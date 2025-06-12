using BytagrammAPI.Dto;
using BytagrammAPI.Dto.Community;
using BytagrammAPI.Dto.User;
using BytagrammAPI.Models;
using BytagrammAPI.Models.Redis;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BytagrammAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ICacheService _cacheService;

        public UserController(IUserService userService, IJwtService jwtService, ICacheService cacheService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _cacheService = cacheService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();

            if (users == null || !users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        [Authorize]
        [HttpGet("get-me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return Unauthorized();

            var user = await _cacheService.GetAsync<UserCache>(userId);

            if (user == null) return NotFound();

            List<CommunityDto> dtoList = user.SubscribedCommunities
                .Select(c => new CommunityDto
                {
                    Title = c.Title,
                    Description = c.Description,
                })
                .ToList();

            var dto = new ProfileDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Communities = dtoList
            };
            return Ok(dto);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.RefreshToken))
            {
                return BadRequest();
            }

            var user = await _jwtService.ValidateRefreshTokenAsync(dto.RefreshToken);

            if (user == null)
            {
                return Unauthorized("Invalid refresh token");
            }

            var principal = _jwtService.GetPrincipalFromExpiredToken(dto.AccessToken);

            var userId = principal.Claims.First().Value;

            if (userId == null)
            {
                return Unauthorized("Access token and refresh token mismatch");
            }

            await CacheUserData(user);

            var newTokens = await _jwtService.GenerateTokens(user);

            return Ok(newTokens);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            var user = new Models.User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = ComputeSha256Hash(dto.Password)
            };

            await _userService.AddAsync(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.Identifier) || string.IsNullOrEmpty(dto.Password))
                return BadRequest();

            User user;
            if (dto.Identifier.Contains("@"))
            {
                user = await _userService.GetByEmailAsync(dto.Identifier);
            }
            else
            {
                user = await _userService.GetByUsernameAsync(dto.Identifier);
            }

            if (user == null)
                return Unauthorized("Invalid Credentials");

            var isPasswordValid = await _userService.VerifyPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
                return Unauthorized("Invalid Credentials");

            await CacheUserData(user);

            var tokens = await _jwtService.GenerateTokens(user);

            return Ok(tokens);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] RegisterDto dto)
        {
            if (dto == null)
                return BadRequest();

            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            user.UserName = dto.UserName ?? user.UserName;
            user.Email = dto.Email ?? user.Email;

            await _userService.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userService.DeleteAsync(id);
            return NoContent();
        }

        private async Task CacheUserData(User user)
        {
            List<CommunityDto> dtoList = user.SubscribedCommunities
               .Select(c => new CommunityDto
               {
                   Title = c.Title,
                   Description = c.Description,
               })
               .ToList();

            var userCache = new Cache<UserCache>
            {
                Key = user.Id,
                Payload = new UserCache 
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    SubscribedCommunities = dtoList
                }
            };

            await _cacheService.SetAsync(userCache);
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

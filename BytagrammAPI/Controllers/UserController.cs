using BytagrammAPI.Dto;
using BytagrammAPI.Models;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
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

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();

            if (users == null || !users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto dto) 
        {
            if(dto == null || string.IsNullOrEmpty(dto.RefreshToken)) 
            {
                return BadRequest();
            }

            var user = await _jwtService.ValidateRefreshTokenAsync(dto.RefreshToken);

            if (user == null)
            {
                return BadRequest();
            }

            var principal = _jwtService.GetPrincipalFromExpiredToken(dto.AccessToken);
            var userId = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if(userId == null) 
            {
                return Unauthorized();
            }

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

            var tokens = await _jwtService.GenerateTokens(user);

            return Ok(tokens);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userService.DeleteAsync(id);
            return NoContent();
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

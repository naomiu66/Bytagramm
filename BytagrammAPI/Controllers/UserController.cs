using BytagrammAPI.Dto;
using BytagrammAPI.Models;
using BytagrammAPI.Services.Abstractions;
using BytagrammAPI.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace BytagrammAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
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

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            var result = await _userService.CreateUserAsync(dto, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var createdUser = await _userService.GetByUsernameAsync(dto.UserName);

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            var userDto = await _authService.LoginAsync(dto);

            if (userDto == null)
                return Unauthorized("Invalid credentials");

            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto dto)
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
    }
}

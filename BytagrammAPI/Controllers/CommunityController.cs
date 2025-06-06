using BytagrammAPI.Dto.Community;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BytagrammAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityService _communityService;

        public CommunityController(ICommunityService postService)
        {
            _communityService = postService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllPosts()
        {
            var communities = await _communityService.GetAllAsync();

            if (communities == null || !communities.Any()) return NotFound();

            return Ok(communities);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var community = await _communityService.GetByIdAsync(id);

            if (community == null) return NotFound();

            return Ok(community);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCommunity([FromBody] CreateCommunityDto dto)
        {
            if (dto == null) return BadRequest();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return Unauthorized();

            var community = new Models.Community
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Description = dto.Description,
                AuthorId = userId,
                Created = DateTime.UtcNow
            };

            await _communityService.AddAsync(community);

            return Ok(community);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCommunity(string id, [FromBody] CommunityDto dto)
        {
            if (dto == null) return BadRequest();

            var community = await _communityService.GetByIdAsync(id);

            if (community == null) return NotFound();

            community.Name = dto.Title ?? community.Name;
            community.Description = dto.Description ?? community.Description;

            await _communityService.UpdateAsync(community);

            return Ok(community);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCommunity(string id)
        {
            var community = _communityService.GetByIdAsync(id);

            if (community == null) return NotFound();

            await _communityService.DeleteAsync(id);

            return NoContent();
        }
    }
}

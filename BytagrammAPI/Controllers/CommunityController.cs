using BytagrammAPI.Dto;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var communities = await _communityService.GetAllAsync();

            if (communities == null || !communities.Any()) return NotFound();

            return Ok(communities);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var community = await _communityService.GetByIdAsync(id);

            if (community == null) return NotFound();

            return Ok(community);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommunity([FromBody] CommunityDto dto)
        {
            if (dto == null) return BadRequest();

            var community = new Models.Community
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Description = dto.Description,
                AuthorId = dto.AuthorId,
                Created = DateTime.UtcNow
            };

            await _communityService.AddAsync(community);

            return Ok(community);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCommunity(string id, [FromBody] CommunityDto dto)
        {
            if (dto == null) return BadRequest();

            var community = await _communityService.GetByIdAsync(id);

            if (community == null) return NotFound();

            community.Name = dto.Name ?? community.Name;
            community.Description = dto.Description ?? community.Description;
            community.AuthorId = dto.AuthorId ?? community.AuthorId;

            await _communityService.UpdateAsync(community);

            return Ok(community);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCommunity(string id)
        {
            var community = _communityService.GetByIdAsync(id);

            if (community == null) return NotFound();

            await _communityService.DeleteAsync(id);

            return NoContent();
        }
    }
}

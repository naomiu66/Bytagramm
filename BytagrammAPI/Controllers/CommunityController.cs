using BytagrammAPI.Dto.Community;
using BytagrammAPI.Dto.Post;
using BytagrammAPI.Models;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetAllCommunities()
        {
            var communities = await _communityService.GetAllAsync();

            if (communities == null || !communities.Any()) return NotFound();

            List<CommunityDto> communityDtoList = communities
               .Select(c => new CommunityDto
               {
                   Id = c.Id,
                   Title = c.Title,
                   Description = c.Description,
                   AuthorId = c.AuthorId,
                   CreatedDate = c.Created,
                   Posts = c.Posts
                       .Select(p => new PostDto
                       {
                           Id = p.Id,
                           Title = p.Title,
                           Content = p.Content,
                           AuthorId = p.AuthorId,
                           AuthorName = p.Author.UserName,
                           CreatedDate = p.CreatedDate,
                           CommunityId = p.CommunityId,
                       })
                       .ToList(),
                   MembersCount = c.Subscribers.Count
               })
               .ToList();

            return Ok(communityDtoList);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var community = await _communityService.GetByIdAsync(id);

            if (community == null) return NotFound();

            List<PostDto> dtoList = community.Posts
               .Select(p => new PostDto
               {
                   Id = p.Id,
                   Title = p.Title,
                   Content = p.Content,
                   AuthorId = p.AuthorId,
                   AuthorName = p.Author.UserName,
                   CreatedDate = p.CreatedDate,
                   CommunityId = p.CommunityId,
               })
               .ToList();

            var dto = new CommunityDto 
            {
                Id = community.Id,
                Title = community.Title,
                Description = community.Description,
                AuthorId = community.AuthorId,
                CreatedDate = community.Created,
                Posts = dtoList,
                MembersCount = community.Subscribers.Count
            };

            return Ok(dto);
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
                Title = dto.Name,
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

            community.Title = dto.Title ?? community.Title;
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

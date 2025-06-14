using BytagrammAPI.Dto.Post;
using BytagrammAPI.Models;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace BytagrammAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllAsync();

            if (posts == null || !posts.Any()) return NotFound();

            List<PostDto> dtoList = posts
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

            return Ok(dtoList);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var post = await _postService.GetByIdAsync(id);

            if (post == null) return NotFound();

            var dto = new PostDto
            {
                Id = id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
                AuthorName = post.Author.UserName,
                CreatedDate = post.CreatedDate,
                CommunityId = post.CommunityId,
            };

            return Ok(dto);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto)
        {
            if (dto == null) return BadRequest();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return Unauthorized();

            var post = new Models.Post
            {
                Id = Guid.NewGuid().ToString(),
                Title = dto.Title,
                Content = dto.Content,
                AuthorId = userId,
                CommunityId = dto.CommunityId,
                CreatedDate = DateTime.UtcNow
            };

            await _postService.AddAsync(post);

            return Ok(post);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] PostDto dto)
        {
            if (dto == null) return BadRequest();

            var post = await _postService.GetByIdAsync(id);

            if (post == null) return NotFound();

            post.Title = dto.Title ?? post.Title;
            post.Content = dto.Content ?? post.Content;
            post.AuthorId = dto.AuthorId ?? post.AuthorId;
            post.CommunityId = dto.CommunityId ?? post.CommunityId;

            await _postService.UpdateAsync(post);
            return Ok(post);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePost(string id)
        {
            var post = _postService.GetByIdAsync(id);

            if (post == null) return NotFound();

            await _postService.DeleteAsync(id);

            return NoContent();
        }
    }
}

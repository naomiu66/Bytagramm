using BytagrammAPI.Dto.Post;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            return Ok(posts);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var post = await _postService.GetByIdAsync(id);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto)
        {
            if (dto == null) return BadRequest();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId == null) return Unauthorized();

            var posts = new Models.Post
            {
                Id = Guid.NewGuid().ToString(),
                Title = dto.Title,
                Content = dto.Content,
                AuthorId = userId,
                CommunityId = dto.CommunityId,
                CreatedDate = DateTime.UtcNow
            };

            await _postService.AddAsync(posts);

            return Ok(posts);
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

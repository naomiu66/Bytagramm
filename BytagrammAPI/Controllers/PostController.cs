using BytagrammAPI.Dto;
using BytagrammAPI.Repositories.Abstractions;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllAsync();

            if (posts == null || !posts.Any()) return NotFound();

            return Ok(posts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(string id) 
        {
            var post = await _postService.GetByIdAsync(id);

            if(post == null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostDto dto) 
        {
            if (dto == null) return BadRequest();

            var posts = new Models.Post
            {
                Id = Guid.NewGuid().ToString(),
                Title = dto.Title,
                Content = dto.Content,
                AuthorId = dto.AuthorId,
                CreatedDate = DateTime.UtcNow
            };

            await _postService.AddAsync(posts);

            return Ok(posts);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] PostDto dto) 
        {
            if (dto == null) return BadRequest();

            var post = await _postService.GetByIdAsync(id);

            if (post == null) return NotFound();

            post.Title = dto.Title ?? post.Title;
            post.Content = dto.Content ?? post.Content;
            post.AuthorId = dto.AuthorId ?? post.AuthorId;
                
            await _postService.UpdateAsync(post);
            return Ok(post);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(string id) 
        {
            var post = _postService.GetByIdAsync(id);

            if (post == null) return NotFound();

            await _postService.DeleteAsync(id);

            return NoContent();
        }
    }
}

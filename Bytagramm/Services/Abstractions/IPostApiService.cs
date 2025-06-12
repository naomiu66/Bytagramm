using Bytagramm.Dto.Post;

namespace Bytagramm.Services.Abstractions
{
    public interface IPostApiService
    {
        public Task<List<PostDto>?> GetAllAsync();
        public Task<PostDto?> GetByIdAsync(string id);
        public Task<bool> CreateAsync(PostDto post);
        public Task<bool> UpdateAsync(string id, PostDto post);
        public Task<bool> DeleteAsync(string id);
    }
}

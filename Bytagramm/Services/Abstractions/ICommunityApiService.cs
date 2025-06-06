using Bytagramm.Models.Community;

namespace Bytagramm.Services.Abstractions
{
    public interface ICommunityApiService
    {
        public Task<List<CreateCommunityDto>?> GetAllAsync();
        public Task<CreateCommunityDto?> GetByIdAsync(string id);
        public Task<bool> CreateAsync(CreateCommunityDto community);
        public Task<bool> UpdateAsync(string id, CreateCommunityDto community);
        public Task<bool> DeleteAsync(string id);
    }
}

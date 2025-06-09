using Bytagramm.Models.Community;

namespace Bytagramm.Services.Abstractions
{
    public interface ICommunityApiService
    {
        public Task<List<CommunityDto>?> GetAllAsync();
        public Task<CommunityDto?> GetByIdAsync(string id);
        public Task<bool> CreateAsync(CreateCommunityDto community);
        public Task<bool> UpdateAsync(string id, CommunityDto community);
        public Task<bool> DeleteAsync(string id);
    }
}

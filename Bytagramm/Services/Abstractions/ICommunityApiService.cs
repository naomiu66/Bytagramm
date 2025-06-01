
using Bytagramm.Dto;

namespace Bytagramm.Services.Abstractions
{
    public interface ICommunityApiService
    {
        public Task<List<CommunityDto>?> GetAllAsync();
        public Task<CommunityDto?> GetByIdAsync(string id);
        public Task<bool> CreateAsync(CommunityDto community);
        public Task<bool> UpdateAsync(string id, CommunityDto community);
        public Task<bool> DeleteAsync(string id);
    }
}

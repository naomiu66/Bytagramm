using Bytagramm.Dto;
using System.Net.Http.Json;

namespace Bytagramm.Services
{
    class CommunityApiService
    {
        private readonly HttpClient _httpClient;

        public CommunityApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CommunityDto>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CommunityDto>>("api/Community");
        }

        public async Task<CommunityDto?> GetByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<CommunityDto>($"api/Community/{id}");
        }

        public async Task<bool> CreateAsync(CommunityDto community)
        {
            var response = await _httpClient.PostAsJsonAsync<CommunityDto>("api/Community", community);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string id, CommunityDto community)
        {
            var response = await _httpClient.PutAsJsonAsync<CommunityDto>($"api/Community/{id}", community);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/Community/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

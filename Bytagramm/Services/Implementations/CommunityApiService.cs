using Bytagramm.Models.Community;
using Bytagramm.Services.Abstractions;
using Bytagramm.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Bytagramm.Services
{
    public class CommunityApiService : ApiService, ICommunityApiService
    {

        public CommunityApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options) : base(httpClientFactory, options) { }

        public async Task<List<CommunityDto>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CommunityDto>>("api/Community/get-all");
        }

        public async Task<CommunityDto?> GetByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<CommunityDto>($"api/Community/get/{id}");
        }

        public async Task<bool> CreateAsync(CreateCommunityDto community)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateCommunityDto>("api/Community/create", community);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string id, CommunityDto community)
        {
            var response = await _httpClient.PutAsJsonAsync<CommunityDto>($"api/Community/update/{id}", community);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/Community/delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

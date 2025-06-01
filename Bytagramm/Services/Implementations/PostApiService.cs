using Bytagramm.Dto;
using Bytagramm.Services.Abstractions;
using Bytagramm.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;


namespace Bytagramm.Services.Implementations
{
    class PostApiService : ApiService, IPostApiService
    {

        public PostApiService(HttpClient httpClient, IOptions<ApiSettings> options) : base(httpClient, options) { }

        public async Task<List<PostDto>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostDto>>("api/Post");
        }

        public async Task<PostDto?> GetByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<PostDto>($"api/Post/{id}");
        }

        public async Task<bool> CreateAsync(PostDto post)
        {
            var response = await _httpClient.PostAsJsonAsync<PostDto>("api/Post", post);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string id, PostDto post)
        {
            var response = await _httpClient.PutAsJsonAsync<PostDto>($"api/Post/{id}", post);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/Post/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

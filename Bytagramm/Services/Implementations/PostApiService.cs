using Bytagramm.Dto.Post;
using Bytagramm.Services.Abstractions;
using Bytagramm.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;


namespace Bytagramm.Services.Implementations
{
    public class PostApiService : ApiService, IPostApiService
    {

        public PostApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options) : base(httpClientFactory, options) { }

        public async Task<List<PostDto>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostDto>>("api/Post/get-all");
        }

        public async Task<PostDto?> GetByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<PostDto>($"api/Post/get/{id}");
        }

        public async Task<bool> CreateAsync(CreatePostDto post)
        {
            var response = await _httpClient.PostAsJsonAsync<CreatePostDto>("api/Post/create", post);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string id, PostDto post)
        {
            var response = await _httpClient.PutAsJsonAsync<PostDto>($"api/Post/update/{id}", post);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/Post/delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

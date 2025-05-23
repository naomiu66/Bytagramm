

using Bytagramm.Dto;
using System.Net.Http.Json;

namespace Bytagramm.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDto>?> GetAllAsync() 
        {
            return await _httpClient.GetFromJsonAsync<List<UserDto>>("api/User");
        }

        public async Task<UserDto?> GetByIdAsync(string id) 
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"api/User/{id}");
        }

        public async Task<bool> CreateAsync(UserDto user) 
        {
            var response = await _httpClient.PostAsJsonAsync<UserDto>("api/User", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string id, UserDto user) 
        {
            var response = await _httpClient.PutAsJsonAsync<UserDto>($"api/User/{id}", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id) 
        {
            var response = await _httpClient.DeleteAsync($"api/User/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

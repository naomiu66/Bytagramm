using Bytagramm.Dto;
using Bytagramm.Models;
using Bytagramm.Services.Abstractions;
using Bytagramm.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Bytagramm.Services.Implementations
{
    public class UserApiService : ApiService, IUserApiService
    {
        public UserApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options) : base(httpClientFactory, options) { }


        public async Task<List<UserDto>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<UserDto>>("api/User/get-all");
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"api/User/get/{id}");
        }

        public async Task<bool> CreateAsync(RegisterDto user)
        {
            var response = await _httpClient.PostAsJsonAsync<RegisterDto>("api/User/register", user);

            if (!response.IsSuccessStatusCode) 
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(string id, UserDto user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/User/update/{id}", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/User/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(LoginDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/login", new LoginDto
            {
                Identifier = dto.Identifier,
                Password = dto.Password,
            });

            if (!response.IsSuccessStatusCode) 
            {
                return false;
            }

            var tokenDto = await response.Content.ReadFromJsonAsync<TokenDto>();

            await SecureStorage.SetAsync("access_token", tokenDto.AccessToken);
            await SecureStorage.SetAsync("refresh_token", tokenDto.RefreshToken);

            return true;
        }

        public async Task<ProfileDto?> GetCurrentUserAsync()
        {
            return await _httpClient.GetFromJsonAsync<ProfileDto>($"api/User/get-me");
        }
    }
}

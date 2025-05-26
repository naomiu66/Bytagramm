using Bytagramm.Dto;
using System.Net.Http.Json;

namespace Bytagramm.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;
        private string? _jwtToken;

        public UserApiService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        private void SetAuthorizationHeader()
        {
            if (!string.IsNullOrEmpty(_jwtToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _jwtToken);
            }
        }

        public async Task<List<UserDto>?> GetAllAsync() 
        {
            SetAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<List<UserDto>>("api/User");
        }

        public async Task<UserDto?> GetByIdAsync(string id) 
        {
            SetAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<UserDto>($"api/User/{id}");
        }

        public async Task<bool> CreateAsync(RegisterDto user) 
        {
            var response = await _httpClient.PostAsJsonAsync<RegisterDto>("api/User/register", user);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            if (result is null || string.IsNullOrEmpty(result.Token))
                return false;

            _jwtToken = result.Token;

            await SecureStorage.SetAsync("jwt_token", _jwtToken);

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _jwtToken);

            return true;
        }

        public async Task<bool> UpdateAsync(string id, UserDto user) 
        {
            SetAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync<UserDto>($"api/User/{id}", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id) 
        {
            SetAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"api/User/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AuthenticateAsync(string usernameOrEmail, string password) 
        {
            var loginData = new LoginDto
            {
                Identifier = usernameOrEmail,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("api/User/login", loginData);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            if (result is null || string.IsNullOrEmpty(result.Token))
                return false;

            _jwtToken = result.Token;

            await SecureStorage.SetAsync("jwt_token", _jwtToken);

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _jwtToken);

            return true;
        }

        public async Task LogoutAsync()
        {
            _jwtToken = null;
            await SecureStorage.SetAsync("jwt_token", string.Empty);
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> InitializeAsync() 
        {
            var token = await SecureStorage.GetAsync("jwt_token");

            if (string.IsNullOrEmpty(token)) 
            {
                return false;
            }

            _jwtToken = token;
            SetAuthorizationHeader();

            return true;
        }

    }
}

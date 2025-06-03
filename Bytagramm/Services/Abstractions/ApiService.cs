

using Bytagramm.Settings;
using Microsoft.Extensions.Options;

namespace Bytagramm.Services.Abstractions
{
    public abstract class ApiService
    {
        protected readonly HttpClient _httpClient;
        protected readonly ApiSettings _apiSettings;

        public ApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _apiSettings = options.Value;

            _httpClient.BaseAddress = new Uri(_apiSettings.BaseUrl);
        }
    }
}

using Bytagramm.Dto.Subscriptions;
using Bytagramm.Services.Abstractions;
using Bytagramm.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bytagramm.Services.Implementations
{
    public class SubscriptionApiService : ApiService, ISubscriptionApiService
    {
        public SubscriptionApiService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options) : base(httpClientFactory, options)
        {
        }

        public async Task<bool> Subscribe(NewCommunitySubscriptionDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync<NewCommunitySubscriptionDto>("api/Subscription/subscribe", dto);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}

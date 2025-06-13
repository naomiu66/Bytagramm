using Bytagramm.Dto.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytagramm.Services.Abstractions
{
    public interface ISubscriptionApiService
    {
        public Task<bool> Subscribe(CommunitySubscriptionDto dto);
        public Task<bool> Unsubscribe(CommunitySubscriptionDto dto);
    }
}

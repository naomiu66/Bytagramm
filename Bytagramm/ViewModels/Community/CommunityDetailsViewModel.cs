using Bytagramm.Models.Community;
using Bytagramm.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytagramm.ViewModels.Community
{
    public partial class CommunityDetailsViewModel : ObservableObject
    {
        private readonly ICommunityApiService _communityApiService;

        public CommunityDetailsViewModel(ICommunityApiService communityApiService) 
        {
            _communityApiService = communityApiService;
        }

        [ObservableProperty]
        public CreateCommunityDto community;

        public async void ApplyQueryAttributes(IDictionary<string, object> query) 
        {
            if (query.TryGetValue("communityId", out var idObj) && idObj is string id)
            {
                var loaded = await _communityApiService.GetByIdAsync(id);
                if (loaded != null)
                    Community = loaded;
            }
        }
    }
}

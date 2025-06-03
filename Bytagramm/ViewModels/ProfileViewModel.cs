using Bytagramm.Dto;
using Bytagramm.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytagramm.ViewModels
{
    public partial class ProfileViewModel : ObservableObject, IQueryAttributable
    {
        IUserApiService _userApiService;

        public ProfileViewModel(IUserApiService userApiService) 
        {
            _userApiService = userApiService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            await InitializeAsync();
        }

            [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string email;

        [ObservableProperty]
        public List<CommunityDto> communities;


        public async Task InitializeAsync() 
        {
            var user = await _userApiService.GetCurrentUserAsync();

            if (user != null)
            {
                Name = user.UserName;
                Email = user.Email;
                Communities = user.communities;
            }
            else 
            {
                Name = "None";
                Email = "None";
                Communities = null;
            }
        }
    }
}

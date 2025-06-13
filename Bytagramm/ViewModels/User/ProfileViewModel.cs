using Bytagramm.Dto.Community;
using Bytagramm.Services.Abstractions;
using Bytagramm.Views.Community;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        public List<CommunityDto> subscribedCommunities;

        public async Task InitializeAsync()
        {
            var user = await _userApiService.GetCurrentUserAsync();

            if (user != null)
            {
                Name = user.UserName;
                Email = user.Email;
                SubscribedCommunities = user.SubscribedCommunities;
            }
            else
            {
                Name = "None";
                Email = "None";
                SubscribedCommunities = null;
            }
        }


    }
}

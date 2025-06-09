using Bytagramm.Models.Community;
using Bytagramm.Services.Abstractions;
using Bytagramm.Views.Community;
using Bytagramm.Views.Post;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Bytagramm.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ICommunityApiService _communityApiService;

        public HomeViewModel(ICommunityApiService communityApiService)
        {
            _communityApiService = communityApiService;
            SubscribedCommunities = new ObservableCollection<CommunityDto>();
            LoadCommunities();
        }

        public ObservableCollection<CommunityDto> SubscribedCommunities { get; }

        [RelayCommand]
        private async Task OpenProfile()
        {
            await Shell.Current.GoToAsync($"///{nameof(ProfilePage)}");
        }

        [RelayCommand]
        private async Task CreatePost()
        {
            await Shell.Current.GoToAsync($"///{nameof(CreatePostPage)}");
        }

        [RelayCommand]
        private async Task OpenCommunity(CommunityDto selectedCommunity)
        {
            if (selectedCommunity is not null)
            {
                await Shell.Current.GoToAsync($"{nameof(CommunityDetailsPage)}?communityId={selectedCommunity.Id}");
            }
        }


        private async void LoadCommunities()
        {
            var communities = await _communityApiService.GetAllAsync();

            if (communities != null)
            {
                SubscribedCommunities.Clear();
                foreach (var community in communities)
                {
                    SubscribedCommunities.Add(community);
                }
            }
        }
    }
}

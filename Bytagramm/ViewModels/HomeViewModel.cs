using Bytagramm.Dto.Community;
using Bytagramm.Dto.Post;
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
        private readonly IPostApiService _postApiService;
        private readonly IUserApiService _userApiService;

        public HomeViewModel(IPostApiService postApiService, IUserApiService userApiService)
        {
            _postApiService = postApiService;
            _userApiService = userApiService;

            SubscribedCommunities = new ObservableCollection<CommunityDto>();
            Posts = new ObservableCollection<PostDto>();
            LoadPageContent();
        }

        public ObservableCollection<CommunityDto> SubscribedCommunities { get; }

        public ObservableCollection<PostDto> Posts { get; }

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
                await Shell.Current.GoToAsync($"///{nameof(CommunityDetailsPage)}?communityId={selectedCommunity.Id}");
            }
        }

        [RelayCommand]
        private async Task CreateCommunity() 
        {
            await Shell.Current.GoToAsync($"///{nameof(CreateCommunityPage)}");
        }

        [RelayCommand]
        private async Task OpenPost(PostDto selectedPost) 
        {
            if (selectedPost is not null)
            {
                await Shell.Current.GoToAsync($"///{nameof(PostDetailsPage)}?postId={selectedPost.Id}");
            }
        }

        private async void LoadPageContent()
        {
            try
            {
                var user = await _userApiService.GetCurrentUserAsync();

                if (user == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Unauthorized user", "OK");
                    await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
                }

                var communities = user.SubscribedCommunities;

                var posts = await _postApiService.GetAllAsync();

                if (communities != null)
                {
                    SubscribedCommunities.Clear();

                    foreach (var community in communities)
                    {
                        SubscribedCommunities.Add(community);
                    }
                }

                if (posts != null)
                {
                    Posts.Clear();

                    foreach (var post in posts)
                    {
                        Posts.Add(post);
                    }
                }
            }
            catch (Exception ex) 
            {
                await Shell.Current.DisplayAlert("Error", "User Unauthorized", "OK");
                await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
            }
        }
    }
}

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
                await Shell.Current.GoToAsync($"{nameof(CommunityDetailsPage)}?communityId={selectedCommunity.Id}");
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
                await Shell.Current.GoToAsync($"{nameof(PostDetailsPage)}?postId={selectedPost.Id}");
            }
        }

        private async void LoadPageContent()
        {
            var user = await _userApiService.GetCurrentUserAsync();
            var communities = user.Communities;

            if (communities != null)
            {
                SubscribedCommunities.Clear();

                if (Posts != null) Posts.Clear();
                
                foreach (var community in communities)
                {
                    SubscribedCommunities.Add(community);
                    // TODO: show random posts from reccomendations
                    if (community.Posts != null)
                    {
                        foreach (var post in community.Posts)
                        {
                            Posts.Add(post);
                        }
                    }
                }
            }
        }
    }
}

using Bytagramm.Dto.Community;
using Bytagramm.Dto.Post;
using Bytagramm.Dto.Subscriptions;
using Bytagramm.Dto.User;
using Bytagramm.Services.Abstractions;
using Bytagramm.Views.Post;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Bytagramm.ViewModels.Community
{
    [QueryProperty(nameof(CommunityId), "communityId")]
    public partial class CommunityDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private string communityId;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private int membersCount;

        [ObservableProperty]
        private DateTime createdDate;

        [ObservableProperty]
        ObservableCollection<PostDto> posts = new();

        [ObservableProperty]
        private bool isSubscribed;

        private readonly ICommunityApiService _communityApiService;
        private readonly IUserApiService _userApiService;
        private readonly ISubscriptionApiService _subscriptionApiService;

        public CommunityDetailsViewModel(ICommunityApiService communityApiService, IUserApiService userApiService, ISubscriptionApiService subscriptionApiService)
        {
            _communityApiService = communityApiService;
            _userApiService = userApiService;
            _subscriptionApiService = subscriptionApiService;
        }

        [RelayCommand]
        private async Task Unsubscribe() 
        {
            var dto = new CommunitySubscriptionDto { CommunityId = CommunityId };

            var response = await _subscriptionApiService.Unsubscribe(dto);

            if (response)
            {
                IsSubscribed = false;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong...", "OK");
            }
        }

        [RelayCommand]
        private async Task Subscribe() 
        {
            var dto = new CommunitySubscriptionDto { CommunityId = CommunityId };

            var response = await _subscriptionApiService.Subscribe(dto);

            if (response) 
            {
                IsSubscribed = true;
            }
            else 
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong...", "OK");
            }
        }

        [RelayCommand]
        private async Task CreatePost() 
        {
            await Shell.Current.GoToAsync($"///{nameof(CreatePostPage)}?communityId={CommunityId}");
        }

        [RelayCommand]
        private async Task OpenPost(PostDto selectedPost)
        {
            if (selectedPost is not null)
            {
                await Shell.Current.GoToAsync($"///{nameof(PostDetailsPage)}?postId={selectedPost.Id}");
            }
        }

        partial void OnCommunityIdChanged(string value)
        {
            _ = LoadPageAsync(value);
        }

        private async Task LoadPageAsync(string id)
        {
            try
            {
                var user = await _userApiService.GetCurrentUserAsync();

                IsSubscribed = user.SubscribedCommunities.Any(c => c.Id == id);

                var community = await _communityApiService.GetByIdAsync(id);
                if (community != null)
                {
                    Title = community.Title;
                    Description = community.Description;
                    MembersCount = community.MembersCount;
                    CreatedDate = community.CreatedDate;
                }
                var posts = community.Posts;

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
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", "Can not initialize community data", "OK");
            }
        }
    }
}

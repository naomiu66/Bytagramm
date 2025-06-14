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
    public partial class CommunityDetailsViewModel : ObservableObject, IQueryAttributable
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
        private ObservableCollection<PostDto> posts = new();

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

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.TryGetValue("communityId", out var communityId)) 
            {
                try
                {
                    var _communityId = communityId as string;

                    CommunityId = _communityId;

                    var user = await _userApiService.GetCurrentUserAsync();

                    IsSubscribed = user.SubscribedCommunities.Any(c => c.Id == _communityId);

                    var community = await _communityApiService.GetByIdAsync(_communityId);
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
            else 
            {
                await Shell.Current.DisplayAlert("Error", "Community not found", "OK");
                await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
            }
        }
    }
}

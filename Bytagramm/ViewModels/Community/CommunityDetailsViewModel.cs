using Bytagramm.Dto.Community;
using Bytagramm.Dto.Post;
using Bytagramm.Dto.Subscriptions;
using Bytagramm.Dto.User;
using Bytagramm.Services.Abstractions;
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

        private readonly ICommunityApiService _communityApiService;
        private readonly ISubscriptionApiService _subscriptionApiService;

        public CommunityDetailsViewModel(ICommunityApiService communityApiService, ISubscriptionApiService subscriptionApiService)
        {
            _communityApiService = communityApiService;
            _subscriptionApiService = subscriptionApiService;
        }

        [RelayCommand]
        private async Task Subscribe() 
        {
            var dto = new NewCommunitySubscriptionDto { CommunityId = CommunityId };

            var response = await _subscriptionApiService.Subscribe(dto);

            if (response) 
            {
                await Shell.Current.DisplayAlert("Success", "You subscribed on this community", "OK");
            }
            else 
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong...", "OK");
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

using Bytagramm.Models.Community;
using Bytagramm.Models.Post;
using Bytagramm.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly IPostApiService _postApiService;

        public CommunityDetailsViewModel(ICommunityApiService communityApiService, IPostApiService postApiService) 
        {
            _communityApiService = communityApiService;
            _postApiService = postApiService;
        }

        [RelayCommand]
        private async Task Subscribe() { }

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

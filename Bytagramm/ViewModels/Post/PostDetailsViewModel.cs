using Bytagramm.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bytagramm.ViewModels.Post
{
    [QueryProperty(nameof(PostId), "postId")]
    public partial class PostDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private string postId;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string content;

        [ObservableProperty]
        private string communityId;

        [ObservableProperty]
        private string authorId;

        [ObservableProperty]
        private string authorName;

        [ObservableProperty]
        private DateTime createdDate;

        IPostApiService _postApiService;

        public PostDetailsViewModel(IPostApiService postApiService) 
        {
            _postApiService = postApiService;
        }

        partial void OnPostIdChanged(string value)
        {
            _ = LoadPageAsync(value);
        }


        private async Task LoadPageAsync(string id)
        {
            try
            {
                var post = await _postApiService.GetByIdAsync(id);

                Title = post.Title;
                Content = post.Content;
                CommunityId = post.CommunityId;
                AuthorId = post.AuthorId;
                AuthorName = post.AuthorName;
                CreatedDate = post.CreatedDate;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", "Can not initialize post data", "OK");
            }
        }
    }
}

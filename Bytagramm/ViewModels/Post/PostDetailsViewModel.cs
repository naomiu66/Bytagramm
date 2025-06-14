using Bytagramm.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace Bytagramm.ViewModels.Post
{
    public partial class PostDetailsViewModel : ObservableObject, IQueryAttributable
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

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("postId", out var postId)) 
            {
                var _postId = postId as string;

                var post = await _postApiService.GetByIdAsync(_postId);

                if (post != null) 
                {
                    Title = post.Title;
                    Content = post.Content;
                    CommunityId = post.CommunityId;
                    AuthorId = post.AuthorId;
                    AuthorName = post.AuthorName;
                    CreatedDate = (DateTime)post.CreatedDate;
                }
            }

            else 
            {
                await Shell.Current.DisplayAlert("Error", "Post not found", "OK");
                await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
            }
        }
    }
}

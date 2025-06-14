using Bytagramm.Dto.Post;
using Bytagramm.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bytagramm.ViewModels.Post
{
    [QueryProperty(nameof(CommunityId), "communityId")]
    public partial class CreatePostViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string content;

        [ObservableProperty]
        private string communityId;

        IPostApiService _postApiService;

        public CreatePostViewModel(IPostApiService postApiService) 
        {
            _postApiService = postApiService;
        }

        [RelayCommand]
        private async Task Create() 
        {
            if (string.IsNullOrWhiteSpace(Title)) Title = "NoTitle";

            if (string.IsNullOrWhiteSpace(Content)) 
            {
                await Shell.Current.DisplayAlert("Error", "Content field must be filled", "OK");
                return;
            }

            var dto = new CreatePostDto {  Title = Title, Content = Content, CommunityId = CommunityId };

            var response = await _postApiService.CreateAsync(dto);

            if (response) 
            {
                await Shell.Current.DisplayAlert("Success", "Post has been created", "OK");
                await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
            }
            else 
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong", "OK");
            }
        }
    }
}

using Bytagramm.ViewModels.Post;

namespace Bytagramm.Views.Post;

public partial class CreatePostPage : ContentPage
{
	public CreatePostPage()
	{
		InitializeComponent();
        var viewModel = MauiProgram.Current.GetService<CreatePostViewModel>();
        BindingContext = viewModel;
    }
}
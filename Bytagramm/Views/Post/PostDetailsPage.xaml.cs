using Bytagramm.ViewModels.Post;

namespace Bytagramm.Views.Post;

public partial class PostDetailsPage : ContentPage
{
	public PostDetailsPage()
	{
		InitializeComponent();
        var viewModel = MauiProgram.Current.GetService<PostDetailsViewModel>();
        BindingContext = viewModel;
    }
}
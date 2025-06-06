using Bytagramm.ViewModels.Community;

namespace Bytagramm.Views.Community;

public partial class CreateCommunityPage : ContentPage
{
	public CreateCommunityPage()
	{
		InitializeComponent();
        var viewModel = MauiProgram.Current.GetService<CreateCommunityViewModel>();
        BindingContext = viewModel;
    }
}
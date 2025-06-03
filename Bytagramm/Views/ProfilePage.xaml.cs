using Bytagramm.ViewModels;

namespace Bytagramm;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
        InitializeComponent();
        var viewModel = MauiProgram.Current.GetService<ProfileViewModel>();
        BindingContext = viewModel;

        viewModel.InitializeAsync();
    }
}
using Bytagramm.ViewModels;

namespace Bytagramm.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
        var viewModel = MauiProgram.Current.GetService<ProfileVewModel>();
        BindingContext = viewModel;
    }
}
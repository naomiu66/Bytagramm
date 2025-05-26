using Bytagramm.ViewModels;

namespace Bytagramm;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        var viewModel = MauiProgram.Current.GetService<HomeViewModel>();
        BindingContext = viewModel;
	}
}
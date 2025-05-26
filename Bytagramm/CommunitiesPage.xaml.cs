using Bytagramm.ViewModels;

namespace Bytagramm;

public partial class CommunitiesPage : ContentPage
{
	public CommunitiesPage()
	{
		InitializeComponent();
        var viewModel = MauiProgram.Current.GetService<CommunitiesViewModel>();
        BindingContext = viewModel;
    }
}
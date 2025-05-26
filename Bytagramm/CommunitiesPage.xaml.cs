using Bytagramm.ViewModels;

namespace Bytagramm;

public partial class CommunitiesPage : ContentPage
{
	public CommunitiesPage(CommunitiesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}
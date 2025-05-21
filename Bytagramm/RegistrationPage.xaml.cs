using Bytagramm.ViewModels;

namespace Bytagramm;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(RegistrationViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
	}
}
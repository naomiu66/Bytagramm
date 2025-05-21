using Bytagramm.ViewModels;

namespace Bytagramm
{

	public partial class LoginPage : ContentPage
	{
		public LoginPage(LoginViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;
		}
	}
}
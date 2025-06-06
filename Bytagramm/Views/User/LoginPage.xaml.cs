using Bytagramm.ViewModels;

namespace Bytagramm
{

    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var viewModel = MauiProgram.Current.GetService<LoginViewModel>();
            BindingContext = viewModel;
        }
    }
}
using Bytagramm.ViewModels;

namespace Bytagramm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var viewModel = MauiProgram.Current.GetService<MainViewModel>();
            BindingContext = viewModel;
        }
    }

}

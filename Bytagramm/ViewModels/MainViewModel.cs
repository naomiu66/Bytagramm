using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bytagramm.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        [RelayCommand]
        private async Task Login() 
        {
            await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
        }

        [RelayCommand]
        private async Task Signin() 
        {
            await Shell.Current.GoToAsync($"///{nameof(RegistrationPage)}");
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace Bytagramm.ViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool showPassword;

        [ObservableProperty]
        private bool isPassword = true;

        partial void OnShowPasswordChanged(bool value) 
        {
            IsPassword = !value;
        }

        [RelayCommand]
        [Obsolete]
        private async Task Register() 
        {
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password)) 
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Fill all fields", "OK");
                return;
            }
        }
    }
}

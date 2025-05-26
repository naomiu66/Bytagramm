using Bytagramm.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel.Communication;

namespace Bytagramm.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly UserApiService _userApiService;

        public LoginViewModel(UserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        [ObservableProperty]
        private string usernameOrEmail;

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
        private async Task Login() 
        {
            if (string.IsNullOrWhiteSpace(UsernameOrEmail) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Fill all fields", "OK");
                return;
            }

            var success = await _userApiService.AuthenticateAsync(UsernameOrEmail, Password);

            if(success) 
            {
#if DEBUG
                await Shell.Current.DisplayAlert("Success", "User logged in", "OK");
#endif
                await SecureStorage.GetAsync("auth_token");
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            else 
            {
                await Shell.Current.DisplayAlert("Erroe", "Something went wrong...", "OK");
            }
        }
    }
}

using Bytagramm.Dto.User;
using Bytagramm.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace Bytagramm.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IUserApiService _userApiService;

        public LoginViewModel(IUserApiService userApiService)
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
        private async Task Signup()
        {
            await Shell.Current.GoToAsync($"///{nameof(RegistrationPage)}");
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(UsernameOrEmail) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Fill all fields", "OK");
                return;
            }
            LoginDto dto = new LoginDto
            {
                Identifier = UsernameOrEmail,
                Password = Password,
            };

            var success = await _userApiService.LoginAsync(dto);

            if (success)
            {
                try
                {
                    await Shell.Current.DisplayAlert("Success", "User logged in", "OK");
                    await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Login] Ошибка: {ex.Message}");
                    await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "ОК");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong...", "OK");
            }
        }
    }
}

using Bytagramm.Dto;
using Bytagramm.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
namespace Bytagramm.ViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        private readonly UserApiService _userApiService;

        public RegistrationViewModel(UserApiService userApiService) 
        {
            _userApiService = userApiService;
        }

        [ObservableProperty]
        private string mail;

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
        private async Task Register() 
        {
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Mail)) 
            {
                await Shell.Current.DisplayAlert("Error", "Fill all fields", "OK");
                return;
            }

            if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$"))
            {
                await Shell.Current.DisplayAlert("Error",
                    "Password must contain at least 1 uppercase letter, 1 lowercase letter, and 1 digit.",
                    "OK");
                return;
            }

            RegisterDto user = new RegisterDto()
            {
                UserName = Username,
                Password = Password,
                Email = Mail
            };

            bool success = await _userApiService.CreateAsync(user);
            if (success)
            {
#if DEBUG
                await Shell.Current.DisplayAlert("Success", "User has been created!", "ОК");
#endif
                string token = Guid.NewGuid().ToString();
                await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong...", "OK");
            }
        }
    }
}

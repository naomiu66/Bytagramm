using Bytagramm.Dto;
using Bytagramm.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

            UserDto user = new UserDto()
            {
                UserName = Username,
                Password = Password,
                Email = "liririlira"
            };

            try
            {
                bool success = await _userApiService.CreateAsync(user);
                if (success)
                    await Application.Current.MainPage.DisplayAlert("OK", "Создан", "ОК");
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Что-то пошло не так", "ОК");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}

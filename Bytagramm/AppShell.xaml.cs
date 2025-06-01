using Bytagramm.Services.Abstractions;
using Bytagramm.Views;

namespace Bytagramm
{
    public partial class AppShell : Shell
    {
        private readonly IUserApiService _userApiService;
        public AppShell(IUserApiService userApiService)
        {
            InitializeComponent();

            _userApiService = userApiService;

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(CommunitiesPage), typeof(CommunitiesPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        }

        private async Task InitAsync()
        {
            string token = await SecureStorage.GetAsync("access_token");

            if (!string.IsNullOrEmpty(token))
                await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
            else
                await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!_initialized)
            {
                _initialized = true;
                await InitAsync();
            }
        }

        private bool _initialized = false;


    }
}

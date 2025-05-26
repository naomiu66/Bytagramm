using Bytagramm.Services;

namespace Bytagramm
{
    public partial class AppShell : Shell
    {
        private readonly UserApiService _userApiService;
        public AppShell(UserApiService userApiService)
        {
            InitializeComponent();

            _userApiService = userApiService;

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(CommunitiesPage), typeof(CommunitiesPage));
        }

        private async Task InitAsync()
        {
            bool loggedIn = await _userApiService.InitializeAsync();

            if (loggedIn)
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

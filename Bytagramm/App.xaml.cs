using Bytagramm.Services;

namespace Bytagramm
{
    public partial class App : Application
    {
        private readonly UserApiService _userApiService;
        public App(UserApiService userApiService)
        {
            InitializeComponent();
            _userApiService = userApiService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell(_userApiService));
        }
    }
}
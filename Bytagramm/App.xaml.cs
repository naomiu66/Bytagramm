using Bytagramm.Services.Abstractions;
using System.Diagnostics;

namespace Bytagramm
{
    public partial class App : Application
    {
        private readonly IUserApiService _userApiService;
        public App(IUserApiService userApiService)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Ошибка в InitializeComponent: " + ex.ToString());
                throw;
            }
            _userApiService = userApiService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell(_userApiService));
        }
    }
}
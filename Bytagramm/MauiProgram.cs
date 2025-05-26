using Bytagramm.ViewModels;
using Microsoft.Extensions.Logging;
using Bytagramm.Services;
using Microsoft.Extensions.Configuration;
using Bytagramm.Settings;
using System.Reflection;
using System.Diagnostics;


namespace Bytagramm
{
    public static class MauiProgram
    {
        public static IServiceProvider Current { get; private set; }
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("Bytagramm.appsettings.json");
            if (stream == null)
                throw new FileNotFoundException("Could not find embedded resource: appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var settings = config.GetRequiredSection("ApiSettings").Get<ApiSettings>();

            //Services
            builder.Services.AddHttpClient<UserApiService>(client =>
            {
                client.BaseAddress = new Uri(settings.Path);
            });

            builder.Services.AddHttpClient<PostApiService>(client =>
            {
                client.BaseAddress = new Uri(settings.Path);
            });

            builder.Services.AddHttpClient<CommunityApiService>(client =>
            {
                client.BaseAddress = new Uri(settings.Path);
            });


            //Pages
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<RegistrationViewModel>();

            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<HomeViewModel>();

            builder.Services.AddTransient<CommunitiesPage>();
            builder.Services.AddTransient<CommunitiesViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            Current = app.Services;

            return app;
        }
    }
}

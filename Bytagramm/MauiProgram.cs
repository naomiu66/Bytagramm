using Bytagramm.Services;
using Bytagramm.Services.Abstractions;
using Bytagramm.Services.Implementations;
using Bytagramm.Settings;
using Bytagramm.ViewModels;
using Bytagramm.ViewModels.Community;
using Bytagramm.ViewModels.Post;
using Bytagramm.Views.Community;
using Bytagramm.Views.Post;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;


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

            builder.Services.Configure<ApiSettings>(config.GetSection("ApiSettings"));

            builder.Services.AddTransient<AuthentificatedHttpClientHandler>();

            builder.Services.AddHttpClient("ApiClient")
                .AddHttpMessageHandler<AuthentificatedHttpClientHandler>();

            //Services
            builder.Services.AddScoped<IUserApiService, UserApiService>();

            builder.Services.AddScoped<IPostApiService, PostApiService>();

            builder.Services.AddScoped<ICommunityApiService, CommunityApiService>();

            builder.Services.AddScoped<ISubscriptionApiService, SubscriptionApiService>();


            //Pages
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            //User
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<RegistrationViewModel>();

            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<HomeViewModel>();

            builder.Services.AddTransient<CommunitiesPage>();
            builder.Services.AddTransient<CommunitiesViewModel>();

            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<ProfileViewModel>();

            //Post
            builder.Services.AddTransient<CreatePostPage>();
            builder.Services.AddTransient<CreatePostViewModel>();

            builder.Services.AddTransient<PostDetailsPage>();
            builder.Services.AddTransient<PostDetailsViewModel>();

            //Communiuty
            builder.Services.AddTransient<CreateCommunityPage>();
            builder.Services.AddTransient<CreateCommunityViewModel>();

            builder.Services.AddTransient<CommunityDetailsPage>();
            builder.Services.AddTransient<CommunityDetailsViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            Current = app.Services;

            return app;
        }
    }
}

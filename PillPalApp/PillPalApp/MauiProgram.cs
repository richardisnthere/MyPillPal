using Microsoft.Extensions.Logging;
using PillPalApp.Services;

namespace PillPalApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            // Add HttpClient with base API address
            builder.Services.AddSingleton(sp => new HttpClient
            {
                // School IP: 10.27.144.216
                BaseAddress = new Uri("http://localhost:5285/")
            });

            // Register Services
            builder.Services.AddSingleton<IMedicationService, MedicationService>();
            builder.Services.AddSingleton<IReminderService, ReminderService>();
            builder.Services.AddSingleton<IUserService, UserService>();

            return builder.Build();
        }
    }
}

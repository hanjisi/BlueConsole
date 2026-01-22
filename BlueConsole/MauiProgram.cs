using BlueConsole.ViewModels;
using BlueConsole.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BlueConsole
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<ScanPageViewModel>();
            builder.Services.AddTransient<ConsolePageViewModel>();
            return builder.Build();
        }
    }
}

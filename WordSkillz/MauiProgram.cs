using CommunityToolkit.Maui;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using UraniumUI;

#if ANDROID
using WordSkillz.Platforms.Android;
#endif

namespace WordSkillz
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .UseSkiaSharp()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
                    fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemiBold");
                    fonts.AddFontAwesomeIconFonts();
                });
#if ANDROID
            builder.ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler<Shell, CustomShellHandler>();
                });
#endif

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<SQLiteDbContext>();
            builder.UseMauiCommunityToolkit();
            return builder.Build();
        }
    }
}
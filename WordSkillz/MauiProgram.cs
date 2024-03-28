﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using UraniumUI;
using WordSkillz.Platforms.Android;

namespace WordSkillz
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
                    fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemiBold");
                });
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<Shell, CustomShellHandler>();
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<SQLiteDbContext>();
            builder.UseMauiCommunityToolkit();
            builder.UseSkiaSharp();

            return builder.Build();
        }
    }
}
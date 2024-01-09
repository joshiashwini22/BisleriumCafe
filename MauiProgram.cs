using BisleriumCafe.Data;
using BisleriumCafe.Data.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using QuestPDF.Infrastructure;





namespace BisleriumCafe
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
            QuestPDF.Settings.License = LicenseType.Community;
            builder.Services.AddMudServices();
            return builder.Build();
        }
    }
}

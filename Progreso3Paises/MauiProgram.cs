using Microsoft.Extensions.Logging;
using Progreso3Paises.Repositories;

namespace Progreso3Paises
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            string dbPath = FileAccessHelper.GetLocalFilePath("Paises.DB");
            builder.Services.AddSingleton<CountryRepo>(s => ActivatorUtilities.CreateInstance<CountryRepo>(s, dbPath));
            return builder.Build();
        }
    }
}

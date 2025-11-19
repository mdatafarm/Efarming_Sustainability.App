using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Models_View;
using Efarming_Sustainability.Core.Interfaces;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Efarming_Sustainability.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                 .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "FASolid");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.UseMicrocharts();
            builder.Services.AddSingleton<UserLoginRepository>();
            builder.Services.AddSingleton<IndicatorsRepository>();
            builder.Services.AddSingleton<CooperativeRepository>();
            builder.Services.AddSingleton<DepartmentsRepository>();
            builder.Services.AddSingleton<MunicipalityRepository>();
            builder.Services.AddSingleton<VillageRepository>();
            builder.Services.AddSingleton<OwnershipTypesRepository>();
            builder.Services.AddSingleton<PlantationStatusesRepository>();
            builder.Services.AddSingleton<PlantationTypesRepository>();
            builder.Services.AddSingleton<PlantationVarietiesRepository>();
            builder.Services.AddSingleton<SupplyChainsRepository>();
            builder.Services.AddSingleton<FarmsRepository>();


            //Registro de los viewmodels

            builder.Services.AddSingleton<FarmsViewModel>();
            builder.Services.AddTransient<LocalFarmsViewModel>();


            //Registros de servicios

            builder.Services.AddSingleton<IAlert, AlertRepository>();

            return builder.Build();
        }
    }
}

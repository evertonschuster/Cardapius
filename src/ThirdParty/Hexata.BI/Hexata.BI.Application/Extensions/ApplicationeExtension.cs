using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.DataBaseSyncs.Customers;
using Hexata.BI.Application.DataBaseSyncs.Sales;
using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Services.Localizations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace Hexata.BI.Application.Extensions
{
    public static class ApplicationeExtension
    {
        public static IHostApplicationBuilder AddWorkflowApp(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSingleton(new ActivitySource("Hexata.BI"));
            builder.Services.AddScoped<Instrument>();


            builder.Services.Configure<LocalizationOption>(builder.Configuration.GetSection("Localization"));

            builder.Services.AddScoped<GoogleLocalizationService>();
            builder.Services.AddScoped<NominatimLocalizationService>();
            builder.Services.AddScoped<SyncManagerService>();

            builder.Services.AddScoped<ILocalizationService, LocalizationService>();
            builder.Services.AddScoped<ISyncService, SyncSaleService>();
            builder.Services.AddScoped<ISyncService, SyncCustomerService>();


            builder.Services.AddHttpClientPolly();

            return builder;
        }
    }
}

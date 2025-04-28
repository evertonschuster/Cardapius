using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.DataBaseSyncs.Sales;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Services.Localizations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace Hexata.BI.Application.Extensions
{
    public static class WorkflowCoreExtension
    {
        public static IHostApplicationBuilder AddWorkflowApp(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSingleton(new ActivitySource("Hexata.BI"));
            builder.Services.AddScoped<Instrument>();


            builder.Services.AddScoped<GoogleLocalizationService>();
            builder.Services.AddScoped<NominatimLocalizationService>();
            builder.Services.AddScoped<ILocalizationService, LocalizationService>();
            builder.Services.AddScoped<ISyncService, SyncSaleService>();
            builder.Services.AddScoped<SyncManagerService>();


            builder.Services.AddHttpClientPolly();

            return builder;
        }
    }
}

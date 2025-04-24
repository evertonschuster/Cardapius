using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.DataBaseSyncs.Sales;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Services.Localizations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using WorkflowCore.Interface;

namespace Hexata.BI.Application.Extensions
{
    public static class WorkflowCoreExtension
    {
        public static IHostApplicationBuilder AddWorkflowApp(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSingleton(new ActivitySource("Hexata.BI"));
            builder.Services.AddSingleton<Instrument>();


            builder.Services.AddSingleton<GoogleLocalizationService>();
            builder.Services.AddSingleton<NominatimLocalizationService>();
            builder.Services.AddSingleton<ILocalizationService, LocalizationService>();
            builder.Services.AddSingleton<ISyncService, SyncSaleService>();


            builder.Services.AddHttpClientPolly();
            builder.Services.AddWorkflow();

            return builder;
        }
    }
}

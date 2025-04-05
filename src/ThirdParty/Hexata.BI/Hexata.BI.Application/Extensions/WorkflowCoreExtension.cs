using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Services.Localizations;
using Hexata.BI.Application.Workflows.SendOrderBI;
using Hexata.BI.Application.Workflows.SendOrderBI.Steps;
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
            builder.Services.AddSingleton<SendOrderBIJob>();

            builder.Services.AddSingleton<InitializeExtractDataStep>();
            builder.Services.AddSingleton<ExtractHexataDataStep>();
            builder.Services.AddSingleton<ParseDataStep>();
            builder.Services.AddSingleton<EnrichDataStep>();
            builder.Services.AddSingleton<EnrichLatLogDataStep>();
            builder.Services.AddSingleton<SaveDataStep>();

            builder.Services.AddSingleton<GoogleLocalizationService>();
            builder.Services.AddSingleton<NominatimLocalizationService>();
            builder.Services.AddSingleton<ILocalizationService, LocalizationService>();


            builder.Services.AddHttpClientPolly();
            builder.Services.AddWorkflow();

            return builder;
        }

        public static IHost UseWorkflow(this IHost host)
        {
            var workHost = host.Services.GetService<IWorkflowHost>()!;
            workHost.RegisterWorkflow<SendOrderBIWorkfow, SendOrderBIData>();

            workHost.Start();

            return host;
        }
    }
}

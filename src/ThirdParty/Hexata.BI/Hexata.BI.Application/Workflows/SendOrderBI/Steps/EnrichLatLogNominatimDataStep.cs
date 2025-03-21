﻿using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Workflows.SendOrderBI.Dtos.Nominatim;
using Hexata.BI.Application.Workflows.SendOrderBI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    class EnrichLatLogNominatimDataStep(HttpClient httpClient, Instrument instrument, ILogger<EnrichDataStep> logger) : StepBodyAsync
    {
        public Order Order { get; internal set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var endereco = new
            {
                Rua = "Rua Minas Gerais",
                Numero = "123",
                Bairro = "Vila Yolanda",
                Cidade = "Foz do Iguaçu",
                Estado = "Paraná",
                Pais = "Brasil",
                CodigoPostal = "85853-000",
            };

            string requestUri = string.Format(
                "https://nominatim.openstreetmap.org/search?" +
                "street={0}, {1}&city={2}&state={3}&country={4}&postalcode&format=json&addressdetails=1&extratags=1&limit=1&dedupe=1&countrycodes=BR&bounded=1&suburb={5}",
                Uri.EscapeDataString(endereco.Rua),
                Uri.EscapeDataString(endereco.Numero),
                Uri.EscapeDataString(endereco.Cidade),
                Uri.EscapeDataString(endereco.Estado),
                Uri.EscapeDataString(endereco.Bairro),
                Uri.EscapeDataString(endereco.CodigoPostal)
            );

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MeuApp/1.0 (seuemail@exemplo.com)");
            var response = await httpClient.GetAsync(requestUri);


            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<LocationResponse[]>(json) ?? [];

            if (resultado.Length == 0)
            {
                logger.LogWarning("No results found for address {Address}", endereco);
                return ExecutionResult.Next();
            }

            double importance = resultado[0].Importance;

            if (importance > 0.2)
            {
                logger.LogInformation("Enriching data with latitude and longitude importance {Importance}", importance);
            }

            return ExecutionResult.Next();
        }
    }
}
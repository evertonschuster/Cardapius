using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Workflows.SendOrderBI.Dtos;
using Hexata.BI.Application.Workflows.SendOrderBI.Dtos.Google;
using Hexata.BI.Application.Workflows.SendOrderBI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    public class EnrichLatLogGoogleDataStep : StepBodyAsync
    {
        private readonly HttpClient _httpClient;
        private readonly Instrument _instrument;
        private readonly ILogger<EnrichLatLogGoogleDataStep> _logger;

        private const string ApiUrl = "https://maps.googleapis.com/maps/api/geocode/json";
        private const string ApiKey = "AIzaSyBvl9WUHD8-h90-wmTF1s2SGJbLnqdg0F8";

        public Order Order { get; internal set; }


        public EnrichLatLogGoogleDataStep(HttpClient httpClient, Instrument instrument, ILogger<EnrichLatLogGoogleDataStep> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _instrument = instrument;
            _logger = logger;
        }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            if (Order?.Customer == null || string.IsNullOrEmpty(Order.Customer.Address) || string.IsNullOrEmpty(Order.Customer.AddressNumber))
            {
                _logger.LogWarning("Order or customer address information is missing for order {OrderId}", Order?.Id);
                return ExecutionResult.Next();
            }

            if (Order.Fee == 0)
            {
                _logger.LogWarning("No fee found for order {OrderId}", Order.Id);
                return ExecutionResult.Next();
            }

            var endereco = new
            {
                Rua = Order.Customer.Address ?? string.Empty,
                Numero = Order.Customer.AddressNumber ?? string.Empty,
                Bairro = Order.Customer.Neighborhood ?? string.Empty,
                Cidade = "Foz do Iguaçu", 
                Estado = "Paraná",        
                Pais = "Brasil",          
                CodigoPostal = Order.Customer.ZipCode ?? string.Empty
            };

            var address = $"{endereco.Rua}, {endereco.Numero}, {endereco.Bairro}, {endereco.Cidade}, {endereco.Estado}, {endereco.Pais}, {endereco.CodigoPostal}";
            var url = $"{ApiUrl}?address={Uri.EscapeDataString(address)}&key={ApiKey}";

            var response = await _httpClient.GetStringAsync(url);
            var geocode = JsonConvert.DeserializeObject<GeocodeResponse>(response);


            if (geocode is null)
            {
                _logger.LogError("Error getting lat/long for address {Address}", address);
                return ExecutionResult.Next();
            }

            if (geocode.Status == "OK")
            {
                var result = geocode.Results[0];

                var placeId = result.PlaceId;
                var lat = result.Geometry.Location.Lat;
                var lng = result.Geometry.Location.Lng;
                var locationType = result.Geometry.LocationType;

                _logger.LogInformation("Address {Address} has latitude {Lat} and longitude {Lng} and location_type {Location_type}",
                    address, lat, lng, locationType);

                if (locationType == "ROOFTOP" || locationType == "RANGE_INTERPOLATED")
                {
                    var localization = new LocalizationDto()
                    {
                        Id = placeId,
                        Latitude = lat,
                        Longitude = lng,
                        Precision = locationType,
                        Provider = "Google"
                    };

                    Order.Localization = localization;
                }
            }
            else
            {
                _logger.LogError("Error getting lat/long for address {Address}: {Status}", address, geocode.Status);
            }


            return ExecutionResult.Next();
        }
    }
}


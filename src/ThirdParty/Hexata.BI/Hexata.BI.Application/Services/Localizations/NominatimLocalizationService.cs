using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Workflows.SendOrderBI.Dtos.Nominatim;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hexata.BI.Application.Services.Localizations
{
    public class NominatimLocalizationService(HttpClient httpClient, Instrument instrument, ILogger<NominatimLocalizationService> logger) : ILocalizationService
    {
        public async Task<Result<LocalizationResultDto, string>> GetLocalizationAsync(AddressDto addressDto)
        {
            string requestUri = string.Format(
                "https://nominatim.openstreetmap.org/search?" +
                "street={0}, {1}&city={2}&state={3}&country={4}&postalcode={5}&format=json&addressdetails=1&extratags=1&limit=1&dedupe=1&countrycodes=BR&bounded=1&suburb={6}",
                Uri.EscapeDataString(addressDto.Street),
                Uri.EscapeDataString(addressDto.Number),
                Uri.EscapeDataString(addressDto.City ?? "Foz do iguaçu"),
                Uri.EscapeDataString(addressDto.State ?? "Paraná"),
                Uri.EscapeDataString(addressDto.Country),
                Uri.EscapeDataString(addressDto.PostalCode),
                Uri.EscapeDataString(addressDto.Neighborhood)
            );

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MeuApp/1.0 (seuemail@exemplo.com)");
            var response = await httpClient.GetAsync(requestUri);
            instrument.RequestNominatimGeocodeCount.Add(1);

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<LocationResponse[]>(json) ?? [];

            if (resultado.Length == 0)
            {
                logger.LogWarning("No results found for address {AddressDto}", addressDto);
                instrument.RequestNominatimGeocodeFailCount.Add(1);
                return "No results found for address";
            }
            double importance = resultado[0].Importance;

            if (importance > 0.2)
            {
                logger.LogInformation("Enriching data with latitude and longitude importance {Importance}", importance);
                instrument.RequestNominatimGeocodeSuccessCount.Add(1);

                return new LocalizationResultDto()
                {
                    Localization = new LocalizationDto()
                    {
                        Id = resultado[0].PlaceId.ToString(),
                        Latitude = resultado[0].Lat,
                        Longitude = resultado[0].Lon,
                        Precision = importance.ToString(),
                        Provider = "Nominatim"
                    },
                    Json = json
                };
            }

            instrument.RequestNominatimGeocodeFailCount.Add(1);
            return "Importance is less than 0.2";
        }
    }
}

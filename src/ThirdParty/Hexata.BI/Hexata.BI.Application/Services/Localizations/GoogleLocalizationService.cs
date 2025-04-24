using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Services.Localizations.Dtos.Google;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Hexata.BI.Application.Services.Localizations
{
    public class GoogleLocalizationService(HttpClient httpClient, Instrument instrument, ILogger<GoogleLocalizationService> logger) : ILocalizationService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly Instrument _instrument = instrument;
        private readonly ILogger<GoogleLocalizationService> _logger = logger;

        private const string ApiUrl = "https://maps.googleapis.com/maps/api/geocode/json";
        private const string ApiKey = "AIzaSyBvl9WUHD8-h90-wmTF1s2SGJbLnqdg0F8";

        public async Task<Result<LocalizationResultDto, string>> GetLocalizationAsync(AddressDto addressDto)
        {
            var address = new StringBuilder();
            address.Append($"{addressDto.Street}, {addressDto.Number} - {addressDto.Neighborhood} - {addressDto.City ?? "Foz do iguaçu"}");
            if (!string.IsNullOrEmpty(addressDto.State))
            {
                address.Append($", {addressDto.State}");
            }
            if (!string.IsNullOrEmpty(addressDto.Country))
            {
                address.Append($", {addressDto.Country}");
            }
            if (!string.IsNullOrEmpty(addressDto.PostalCode))
            {
                address.Append($", {addressDto.PostalCode}");
            }


            var url = $"{ApiUrl}?address={Uri.EscapeDataString(address.ToString())}&key={ApiKey}";

            var response = await _httpClient.GetStringAsync(url);
            var geocode = JsonConvert.DeserializeObject<GeocodeResponse>(response);


            _instrument.RequestGoogleGeocodeCount.Add(1);

            if (geocode is null)
            {
                _logger.LogError("Error getting lat/long for address {Address}", address);
                _instrument.RequestGoogleGeocodeFailCount.Add(1);
                return "Error getting lat/long for address";
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
                _instrument.RequestGoogleGeocodeSuccessCount.Add(1);

                if (locationType == "ROOFTOP" || locationType == "RANGE_INTERPOLATED")
                {
                    return new LocalizationResultDto()
                    {
                        Localization = new LocalizationDto()
                        {
                            Id = placeId,
                            Latitude = lat,
                            Longitude = lng,
                            Precision = locationType,
                            Provider = "Google"
                        },
                        Json = response
                    };

                }
            }

            _logger.LogError("Error getting lat/long for address {Address}: {Status}", address, geocode.Status);
            _instrument.RequestGoogleGeocodeFailCount.Add(1);
            return "Error getting lat/long for address";
        }
    }
}

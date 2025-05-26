using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Repositories;
using Hexata.BI.Application.Services.Localizations.Dtos.Google;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Hexata.BI.Application.Services.Localizations;

public class GoogleLocalizationService(
    HttpClient httpClient,
    Instrument instrument,
    IMonthlyConsumptionRepository consumptionRepository,
    IOptions<LocalizationOption> options,
    ILogger<GoogleLocalizationService> logger)
{
    private readonly LocalizationBoundOption Bounds = options.Value.Bounds;
    private readonly string ApiUrl = options.Value.GoogleApiUrl;
    private readonly string ApiKey = options.Value.GoogleApiKey;
    private readonly List<string> LocationType = options.Value.GoogleLocationType;
    private readonly int MaxRequestMonth = options.Value.GoogleMaxRequestMonth;

    public async Task<Result<LocalizationProviderDto, string>> GetLocalizationAsync(AddressDto addressDto)
    {
        var consume = await consumptionRepository.GetByMonthAsync("GoogleLocalizationService", DateTime.Now);
        if (consume != null && consume.Total >= MaxRequestMonth)
        {
            return Result<LocalizationProviderDto, string>.WithError("Max request month reached");
        }

        var formattedAddress = BuildFormattedAddress(addressDto);
        var bounds = $"{Bounds.Min.Lat},{Bounds.Min.Lng}|{Bounds.Max.Lat},{Bounds.Max.Lng}";
        var requestUrl = $"{ApiUrl}?address={Uri.EscapeDataString(formattedAddress)}&bounds={bounds}&key={ApiKey}";

        string? responseJson = null;
        try
        {
            responseJson = await httpClient.GetStringAsync(requestUrl);
            var geocode = JsonConvert.DeserializeObject<GeocodeResponse>(responseJson);
            await consumptionRepository.AddByMonthAsync("GoogleLocalizationService", DateTime.Now);

            if (geocode == null || geocode.Results == null)
            {
                LogFailure(formattedAddress, "Null or empty response");
                return Result<LocalizationProviderDto, string>.WithError("Error getting lat/long for address");
            }

            var response = new LocalizationProviderDto
            {
                Provider = "Google",
                Json = responseJson
            };

            if (geocode.Status == "OK")
            {
                var result = geocode.Results[0];
                var lat = result.Geometry.Location.Lat;
                var lng = result.Geometry.Location.Lng;
                var locationType = result.Geometry.LocationType;

                logger.LogInformation("Geocoded address: {Address} → lat: {Lat}, lng: {Lng}, type: {Type}",
                    formattedAddress, lat, lng, locationType);
                instrument.RequestGoogleGeocodeSuccessCount.Add(1);

                if (LocationType.Contains(locationType))
                {
                    return Result<LocalizationProviderDto, string>.WithSuccess(response);
                }
            }

            LogFailure(formattedAddress, geocode.Status);

            return Result<LocalizationProviderDto, string>.WithError("Error getting lat/long for address", response);
        }
        catch (Exception ex)
        {
            return HandlerException(ex, formattedAddress, responseJson);
        }
        finally
        {
            instrument.RequestGoogleGeocodeCount.Add(1);
        }
    }

    private Result<LocalizationProviderDto, string> HandlerException(Exception ex, string formattedAddress, string? responseJson)
    {
        logger.LogError(ex, "Exception occurred while requesting geocode for address {Address}", formattedAddress);
        instrument.RequestGoogleGeocodeFailCount.Add(1);
        return Result<LocalizationProviderDto, string>.WithError(responseJson ?? "Exception during Google geocode request");
    }

    private static string BuildFormattedAddress(AddressDto dto)
    {
        var sb = new StringBuilder();
        sb.Append($"{dto.Street}, {dto.Number}");

        if (!string.IsNullOrWhiteSpace(dto.Neighborhood))
            sb.Append($" - {dto.Neighborhood}");

        if (!string.IsNullOrWhiteSpace(dto.City))
            sb.Append($" - {dto.City}");
        else
            sb.Append(" - Foz do Iguaçu");

        if (!string.IsNullOrWhiteSpace(dto.State))
            sb.Append($", {dto.State}");

        if (!string.IsNullOrWhiteSpace(dto.Country))
            sb.Append($", {dto.Country}");

        if (!string.IsNullOrWhiteSpace(dto.PostalCode))
            sb.Append($", {dto.PostalCode}");

        return sb.ToString();
    }

    private void LogFailure(string address, string reason)
    {
        logger.LogInformation("Low precision to geocode address {Address}: {Reason}", address, reason);
        instrument.RequestGoogleGeocodeFailCount.Add(1);
    }
}

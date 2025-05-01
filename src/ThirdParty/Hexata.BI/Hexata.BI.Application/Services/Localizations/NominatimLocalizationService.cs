using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Services.Localizations.Dtos.Nominatim;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Hexata.BI.Application.Services.Localizations;

public class NominatimLocalizationService(
    HttpClient httpClient,
    Instrument instrument,
    IOptions<LocalizationOption> options,
    ILogger<NominatimLocalizationService> logger)
{
    private readonly string NominatimBaseUrl = options.Value.NominatimApiUrl;
    private readonly double MinimiumImportance = options.Value.NominatimMinimiumImportance;
    private const string UserAgent = "MeuApp/1.0 (seuemail@exemplo.com)";

    private static SemaphoreSlim semaphore = new SemaphoreSlim(1);

    public async Task<Result<LocalizationProviderDto, string>> GetLocalizationAsync(AddressDto addressDto)
    {
        string requestUri = BuildRequestUri(addressDto);
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);

        HttpResponseMessage response = null;
        try
        {
            await semaphore.WaitAsync();
            var stopwatch = Stopwatch.StartNew();
            response = await httpClient.GetAsync(requestUri);
            stopwatch.Stop();
            response.EnsureSuccessStatusCode();

            var elapsed = stopwatch.Elapsed;
            if (elapsed < TimeSpan.FromSeconds(1))
            {
                var delay = TimeSpan.FromSeconds(2) - elapsed;
                await Task.Delay(delay);
            }
        }
        catch (Exception ex)
        {
            return await HandlerExceptionAsync(ex, response);
        }
        finally
        {
            semaphore.Release();
        }

        instrument.RequestNominatimGeocodeCount.Add(1);

        var json = await response.Content.ReadAsStringAsync();
        var results = JsonConvert.DeserializeObject<LocationResponse[]>(json) ?? [];

        if (results.Length == 0)
        {
            logger.LogWarning("No results found for address {@AddressDto}", addressDto);
            instrument.RequestNominatimGeocodeFailCount.Add(1);
            return "No results found for address";
        }

        var location = results[0];
        if (location.Importance <= MinimiumImportance)
        {
            logger.LogInformation("Result found but importance {Importance} is too low", location.Importance);
            instrument.RequestNominatimGeocodeFailCount.Add(1);
            return $"Importance is less than or equal to {MinimiumImportance}";
        }

        logger.LogInformation("Enriching data with location: importance {Importance}", location.Importance);
        instrument.RequestNominatimGeocodeSuccessCount.Add(1);

        return new LocalizationProviderDto
        {
            Provider = "Nominatim",
            Json = json
        };
    }

    private async Task<Result<LocalizationProviderDto, string>> HandlerExceptionAsync(Exception ex, HttpResponseMessage? response)
    {
        string? result = null;

        if (response?.Content != null)
        {
            result = await response.Content.ReadAsStringAsync();
        }

        logger.LogError(ex, "Error calling Nominatim API");
        instrument.RequestNominatimGeocodeFailCount.Add(1);

        return result ?? "Failed to reach Nominatim service";
    }

    private string BuildRequestUri(AddressDto address)
    {
        var parameters = new Dictionary<string, string>
        {
            ["street"] = $"{address.Street}, {address.Number}",
            ["city"] = address.City ?? "Foz do iguaçu",
            ["state"] = address.State ?? "Paraná",
            ["country"] = address.Country,
            ["postalcode"] = address.PostalCode,
            ["suburb"] = address.Neighborhood,
            ["format"] = "json",
            ["addressdetails"] = "1",
            ["extratags"] = "1",
            ["limit"] = "1",
            ["dedupe"] = "1",
            ["countrycodes"] = "BR",
            ["bounded"] = "1"
        };

        var query = string.Join("&", parameters
            .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value ?? "")}"));

        return $"{NominatimBaseUrl}?{query}";
    }
}

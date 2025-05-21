using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Services.Localizations.Dtos.Nominatim;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Hexata.BI.Application.Services.Localizations;

public class NominatimLocalizationService(HttpClient httpClient,
                                          Instrument instrument,
                                          IOptions<LocalizationOption> options,
                                          ILogger<NominatimLocalizationService> logger)
{
    private readonly string _baseUrl = options.Value.NominatimApiUrl;
    private readonly double _minimumImportance = options.Value.NominatimMinimiumImportance;
    private const string _userAgent = "MeuApp/1.0 (seuemail@exemplo.com)";
    private static readonly SemaphoreSlim _semaphore = new(1);

    public async Task<Result<LocalizationProviderDto, string>> GetLocalizationAsync(AddressDto addressDto)
    {
        EnsureUserAgent();

        var requestUri = BuildRequestUri(addressDto);
        HttpResponseMessage? response = null;

        try
        {
            await _semaphore.WaitAsync();
            response = await SendRequestWithMinimumDelayAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            return await HandleExceptionAsync(ex, response);
        }
        finally
        {
            _semaphore.Release();
        }

        instrument.RequestNominatimGeocodeCount.Add(1);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var results = JsonConvert.DeserializeObject<LocationResponse[]>(jsonResponse) ?? [];

        if (results.Length == 0)
        {
            LogAndInstrumentNoResults(addressDto);
            return Result<LocalizationProviderDto, string>.WithError("No results found for address");
        }

        var location = results[0];
        if (location.Importance <= _minimumImportance)
        {
            logger.LogInformation("Result found but importance {Importance} is too low", location.Importance);
            instrument.RequestNominatimGeocodeFailCount.Add(1);
            return Result<LocalizationProviderDto, string>.WithError($"Importance is less than or equal to {_minimumImportance}");
        }

        logger.LogInformation("Enriching data with location: importance {Importance}", location.Importance);
        instrument.RequestNominatimGeocodeSuccessCount.Add(1);

        return Result<LocalizationProviderDto, string>.WithSuccess(new LocalizationProviderDto
        {
            Provider = "Nominatim",
            Json = jsonResponse
        });
    }

    private void EnsureUserAgent()
    {
        if (!httpClient.DefaultRequestHeaders.UserAgent.Any())
        {
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
        }
    }

    private async Task<HttpResponseMessage> SendRequestWithMinimumDelayAsync(string requestUri)
    {
        var stopwatch = Stopwatch.StartNew();

        var response = await httpClient.GetAsync(requestUri);

        stopwatch.Stop();
        var elapsed = stopwatch.Elapsed;

        if (elapsed < TimeSpan.FromSeconds(1))
        {
            var remaining = TimeSpan.FromSeconds(1) - elapsed;
            await Task.Delay(remaining);
        }

        return response;
    }

    private async Task<Result<LocalizationProviderDto, string>> HandleExceptionAsync(Exception ex, HttpResponseMessage? response)
    {
        string? responseContent = null;

        if (response?.Content != null)
        {
            responseContent = await response.Content.ReadAsStringAsync();
        }

        logger.LogError(ex, "Error calling Nominatim API");
        instrument.RequestNominatimGeocodeFailCount.Add(1);

        return Result<LocalizationProviderDto, string>.WithError(responseContent ?? "Failed to reach Nominatim service");
    }

    private void LogAndInstrumentNoResults(AddressDto address)
    {
        logger.LogWarning("No results found for address {@AddressDto}", address);
        instrument.RequestNominatimGeocodeFailCount.Add(1);
    }

    private string BuildRequestUri(AddressDto address)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["street"] = $"{address.Street}, {address.Number}",
            ["city"] = address.City ?? "Foz do Iguaçu",
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

        var queryString = string.Join("&", queryParams
            .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value ?? "")}"));

        return $"{_baseUrl}?{queryString}";
    }
}

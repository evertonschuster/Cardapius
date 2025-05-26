using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Repositories;
using Hexata.BI.Application.Services.Localizations.Dtos.Google;
using Hexata.BI.Application.Services.Localizations.Dtos.Nominatim;
using Newtonsoft.Json;

namespace Hexata.BI.Application.Services.Localizations;

public class LocalizationService(
    GoogleLocalizationService googleLocalizationService,
    NominatimLocalizationService nominatimLocalizationService,
    ILocalizationRepository localizationRepository,
    Instrument instrument) : ILocalizationService
{
    private readonly GoogleLocalizationService _googleService = googleLocalizationService;
    private readonly NominatimLocalizationService _nominatimService = nominatimLocalizationService;
    private readonly ILocalizationRepository _repository = localizationRepository;
    private readonly Instrument _instrument = instrument;

    public async Task<Result<LocalizationDto, string>> GetLocalizationAsync(AddressDto addressDto)
    {
        if (addressDto == null ||
            (string.IsNullOrWhiteSpace(addressDto.Street) && string.IsNullOrWhiteSpace(addressDto.Number)))
        {
            return Result<LocalizationDto, string>.WithError("Endereço inválido");
        }

        var cached = _repository.GetAddress(addressDto);
        if (cached?.Value is not null)
        {
            _instrument.LoadLocalizationCacheGeocodeCount.Add(1);
            return ConvertToDto(cached.Value!);
        }


        if (!string.IsNullOrWhiteSpace(addressDto.Number))
        {
            var resultGoogle = await _googleService.GetLocalizationAsync(addressDto);
            _repository.SaveAddress(addressDto, resultGoogle);

            if (resultGoogle?.Value is not null)
                return ConvertToDto(resultGoogle.Value!);
        }

        var resultNominatim = await _nominatimService.GetLocalizationAsync(addressDto);
        _repository.SaveAddress(addressDto, resultNominatim);

        if (resultNominatim.IsSuccess)
            return ConvertToDto(resultNominatim.Value!);

        return Result<LocalizationDto, string>.WithError("Erro ao obter a localização");
    }

    private static Result<LocalizationDto, string> ConvertToDto(LocalizationProviderDto localization)
    {
        switch (localization.Provider)
        {
            case "Google":
                var google = JsonConvert.DeserializeObject<GeocodeResponse>(localization.Json);

                if (google?.Status != "OK" || google.Results?.Count == 0)
                {
                    return Result<LocalizationDto, string>.WithError("Erro ao interpretar resposta do Google");
                }

                var gResult = (google.Results ?? [])[0];

                if (gResult.Types == null || gResult.Types.Count == 0)
                {
                    return Result<LocalizationDto, string>.WithError("Erro ao interpretar resposta do Google");
                }

                return Result<LocalizationDto, string>.WithSuccess(new LocalizationDto
                {
                    Id = gResult.PlaceId,
                    Latitude = gResult.Geometry.Location.Lat,
                    Longitude = gResult.Geometry.Location.Lng,
                    Precision = gResult.Geometry.LocationType,
                    Provider = "Google"
                });

            case "Nominatim":
                var nominatim = JsonConvert.DeserializeObject<LocationResponse[]>(localization.Json);
                if (nominatim == null || nominatim.Length == 0)
                {
                    return Result<LocalizationDto, string>.WithError("Erro ao interpretar resposta do Nominatim");
                }

                var nResult = nominatim[0];
                return Result<LocalizationDto, string>.WithSuccess(new LocalizationDto
                {
                    Id = nResult.PlaceId.ToString(),
                    Latitude = nResult.Lat,
                    Longitude = nResult.Lon,
                    Precision = nResult.Importance.ToString(),
                    Provider = "Nominatim"
                });

            default:
                return Result<LocalizationDto, string>.WithError("Provedor de localização inválido");
        }
    }
}

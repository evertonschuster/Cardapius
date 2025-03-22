using Hexata.BI.Application.Dtos;

namespace Hexata.BI.Application.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalizationService _googleLocalizationService;
        private readonly ILocalizationService _nominatimLocalizationService;

        public LocalizationService(GoogleLocalizationService googleLocalizationService, NominatimLocalizationService nominatimLocalizationService)
        {
            _googleLocalizationService = googleLocalizationService;
            _nominatimLocalizationService = nominatimLocalizationService;
        }

        public async Task<Result<LocalizationDto, string>> GetLocalizationAsync(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return "Address is invalid";
            }

            if (!string.IsNullOrEmpty(addressDto.Number))
            {
                return await _googleLocalizationService.GetLocalizationAsync(addressDto);
            }

            return await _nominatimLocalizationService.GetLocalizationAsync(addressDto);
        }
    }
}

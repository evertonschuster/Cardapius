using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Repositories;

namespace Hexata.BI.Application.Services.Localizations
{
    public class LocalizationService : ILocalizationService
    {
        static int total = 0;
        private readonly ILocalizationService _googleLocalizationService;
        private readonly ILocalizationService _nominatimLocalizationService;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly Instrument _instrument;

        public LocalizationService(
            GoogleLocalizationService googleLocalizationService,
            NominatimLocalizationService nominatimLocalizationService,
            ILocalizationRepository localizationRepository,
            Instrument instrument
            )
        {
            _googleLocalizationService = googleLocalizationService;
            _nominatimLocalizationService = nominatimLocalizationService;
            _localizationRepository = localizationRepository;
            _instrument = instrument;
        }

        public async Task<Result<LocalizationResultDto, string>> GetLocalizationAsync(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return "Address is invalid";
            }

            if (string.IsNullOrWhiteSpace(addressDto.Street) && string.IsNullOrWhiteSpace(addressDto.Number))
            {
                return "Address is invalid";
            }

            var address = _localizationRepository.GetAddress(addressDto);

            if (address != null)
            {
                _instrument.LoadLocalizationCacheGeocodeCount.Add(1);
                return address;
            }

            return "Address is invalid TODO";
            if (total >= 200)
            {
                throw new NotImplementedException();
            }

            total++;
            if (!string.IsNullOrEmpty(addressDto.Number))
            {
                var googleAddress = await _googleLocalizationService.GetLocalizationAsync(addressDto);
                _localizationRepository.SaveAddress(addressDto, googleAddress);
                return googleAddress;
            }

            var nominatimAddress = await _nominatimLocalizationService.GetLocalizationAsync(addressDto);
            _localizationRepository.SaveAddress(addressDto, nominatimAddress);
            return nominatimAddress;
        }

    }
}

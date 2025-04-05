using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Entities;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Repositories;

namespace Hexata.BI.Application.Services.Localizations
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalizationService _googleLocalizationService;
        private readonly ILocalizationService _nominatimLocalizationService;
        private readonly IRepository<Localization> _localizationRepository;
        private readonly Instrument _instrument;

        public LocalizationService(
            GoogleLocalizationService googleLocalizationService,
            NominatimLocalizationService nominatimLocalizationService,
            IRepository<Localization> localizationRepository,
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

            if(string.IsNullOrWhiteSpace(addressDto.Street) && string.IsNullOrWhiteSpace(addressDto.Number))
            {
                return "Address is invalid";
            }

            var address = GetAddress(addressDto);

            if (address != null)
            {
                _instrument.LoadLocalizationCacheGeocodeCount.Add(1);
                return address;
            }


            throw new NotImplementedException();
            if (!string.IsNullOrEmpty(addressDto.Number))
            {
                var googleAddress = await _googleLocalizationService.GetLocalizationAsync(addressDto);
                SaveAddress(addressDto, googleAddress);
                return googleAddress;
            }

            var nominatimAddress = await _nominatimLocalizationService.GetLocalizationAsync(addressDto);
            SaveAddress(addressDto, nominatimAddress);
            return nominatimAddress;
        }

        private void SaveAddress(AddressDto addressDto, Result<LocalizationResultDto, string> localizationResultDto)
        {
            var entity = new Localization(addressDto, localizationResultDto);
            _localizationRepository.InsertOne(entity);
        }

        private Result<LocalizationResultDto, string>? GetAddress(AddressDto addressDto)
        {
            var localization = _localizationRepository.AsQueryable()
                 .Where(e => e.Street == addressDto.Street && e.Number == addressDto.Number && e.Neighborhood == addressDto.Neighborhood && e.City == addressDto.City && e.State == addressDto.State && e.Country == addressDto.Country && e.PostalCode == addressDto.PostalCode)
                 .FirstOrDefault();

            return localization?.Result;
        }
    }
}

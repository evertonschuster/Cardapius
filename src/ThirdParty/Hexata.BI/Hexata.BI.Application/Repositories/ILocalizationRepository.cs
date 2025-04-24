using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Services.Localizations;

namespace Hexata.BI.Application.Repositories
{
    public interface ILocalizationRepository
    {
        Result<LocalizationResultDto, string>? GetAddress(AddressDto addressDto);

        void SaveAddress(AddressDto addressDto, Result<LocalizationResultDto, string> address);

        public Task CleanDataAsync();
    }
}

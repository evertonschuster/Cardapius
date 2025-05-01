using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Services.Localizations;

namespace Hexata.BI.Application.Entities
{
    public class Localization : Entity
    {
        public Localization(AddressDto addressDto, Result<LocalizationProviderDto, string> localizationResultDto)
        {
            Street = addressDto.Street;
            Number = addressDto.Number;
            Neighborhood = addressDto.Neighborhood;
            City = addressDto.City;
            State = addressDto.State;
            Country = addressDto.Country;
            PostalCode = addressDto.PostalCode;
            Result = localizationResultDto;
        }

        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string Country { get; set; }
        public string? PostalCode { get; set; }

        public Result<LocalizationProviderDto, string> Result { get; set; }

    }
}

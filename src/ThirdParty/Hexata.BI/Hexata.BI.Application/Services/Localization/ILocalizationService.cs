using Hexata.BI.Application.Dtos;

namespace Hexata.BI.Application.Services.Localization
{
    public interface ILocalizationService
    {
        Task<Result<LocalizationDto, string>> GetLocalizationAsync(AddressDto addressDto);
    }

    public class AddressDto
    {
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; } 
        public string Country { get; set; } = "Brasil";
        public string? PostalCode { get; set; }
    }
}
using Hexata.BI.Application.Dtos;
using System.ComponentModel;

namespace Hexata.BI.Application.Services.Localizations
{
    public interface ILocalizationService
    {
        Task<Result<LocalizationDto, string>> GetLocalizationAsync(AddressDto addressDto);
    }

    public class LocalizationResultDto
    {
        public required LocalizationDto Localization { get; set; }

        public required string Json { get; set; }
    }

    public class LocalizationProviderDto
    {
        public string Json { get; set; }

        public string Provider { get; set; }
    }

    public class AddressDto
    {
        [DisplayName("Rua")]
        public string? Street { get; set; }

        [DisplayName("Numero")]
        public string? Number { get; set; }

        [DisplayName("Bairro")]
        public string? Neighborhood { get; set; }

        [DisplayName("Cidade")]
        public string? City { get; set; }

        [DisplayName("Estado")]
        public string? State { get; set; } = "Paraná";

        [DisplayName("Pais")]
        public string Country { get; set; } = "Brasil";

        [DisplayName("CEP")]
        public string? PostalCode { get; set; }
    }
}
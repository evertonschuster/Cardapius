﻿using Hexata.BI.Application.Dtos;

namespace Hexata.BI.Application.Services.Localizations
{
    public interface ILocalizationService
    {
        Task<Result<LocalizationResultDto, string>> GetLocalizationAsync(AddressDto addressDto);
    }

    public class LocalizationResultDto
    {
        public required LocalizationDto Localization { get; set; }

        public required string Json { get; set; }
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
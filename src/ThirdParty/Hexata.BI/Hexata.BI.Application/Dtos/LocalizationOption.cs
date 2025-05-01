namespace Hexata.BI.Application.Dtos
{
    public class LocalizationOption
    {
        public required string GoogleApiUrl { get; set; }
        public required string GoogleApiKey { get; set; }
        public required int GoogleMaxRequestMonth { get; set; } = 10_000;
        public required List<string> GoogleLocationType { get; set; } = ["ROOFTOP", "RANGE_INTERPOLATED"];


        public required string NominatimApiUrl { get; set; }
        public required double NominatimMinimiumImportance { get; set; } = 0.2;
    }
}

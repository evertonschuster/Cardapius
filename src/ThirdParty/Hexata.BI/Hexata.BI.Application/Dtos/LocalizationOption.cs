namespace Hexata.BI.Application.Dtos
{
    public record LocalizationOption
    {
        public required LocalizationBoundOption Bounds { get; set; }
        public required string GoogleApiUrl { get; set; }
        public required string GoogleApiKey { get; set; }
        public required int GoogleMaxRequestMonth { get; set; } = 10_000;
        public required List<string> GoogleLocationType { get; set; } = ["ROOFTOP", "RANGE_INTERPOLATED"];


        public required string NominatimApiUrl { get; set; }
        public required double NominatimMinimiumImportance { get; set; } = 0.2;
    }

    public record LocalizationBoundOption
    {
        public required Point Min { get; set; }
        public required Point Max { get; set; }
    }

    public record Point
    {
        public double Lat { get; set; }
        public double Lng { get; set; }

    }
}

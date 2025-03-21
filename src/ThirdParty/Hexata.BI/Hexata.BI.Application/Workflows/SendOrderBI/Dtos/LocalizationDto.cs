namespace Hexata.BI.Application.Workflows.SendOrderBI.Dtos
{
    public class LocalizationDto
    {
        public required string Id { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public required string Precision { get; set; }

        public required string Provider { get; set; }
    }
}

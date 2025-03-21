namespace Hexata.BI.Application.Workflows.SendOrderBI.Dtos
{
    public record CustomerDto
    {
        public long Code { get; set; }
        public string? CompanyName { get; set; }
        public string? FantasyName { get; set; }
        public string? Address { get; set; }
        public string? AddressNumber { get; set; }
        public string? Neighborhood { get; set; }
        public string? AddressComplement { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? CreditStatus { get; set; }
        public string? Type { get; set; }
        public double? DeliveryFee { get; set; }
        public int? SalesQuantity { get; set; }
    }
}

namespace Hexata.BI.Application.Workflows.SendOrderBI.Dtos
{
    public record SaleDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public double ValueWithDiscount { get; set; }
        public double Discount { get; set; }
        public double ValueWithoutDiscount { get; set; }
        public int Term { get; set; }
        public double Change { get; set; }
        public double CustomerValue { get; set; }
        public int? Employee { get; set; }
        public int? CashRegister { get; set; }
        public double CommissionValue { get; set; }
        public string? Status { get; set; }
        public int? IsCardPayment { get; set; }
        public int? TableNumber { get; set; }
        public int? PeopleCount { get; set; }
        public int? IsCreditCardPayment { get; set; }
        public int? CashOperator { get; set; }
        public int? Control { get; set; }
        public string? PC { get; set; }
        public string? HasPrinted { get; set; }
        public string? Plate { get; set; }
        public int? TemporaryCard { get; set; }
        public int? DeliveryPerson { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string? ArrivalTime { get; set; }
        public string? DeliveryTime { get; set; }
        public int? Room { get; set; }
        public double Fee { get; set; }
        public string? Notes { get; set; }
        public string? SaleType { get; set; }
        public string? IsPickup { get; set; }
        public string? CustomerName { get; set; }
        public double? CashValue { get; set; }
        public double? CardValue1 { get; set; }
        public double? CardValue2 { get; set; }
        public double? CheckValue { get; set; }
        public double? TermValue { get; set; }
        public double? DiscountValue { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Terminal { get; set; }
        public string? PrintAccount { get; set; }
        public string? AlphaReference { get; set; }
        public string? PaymentStatus { get; set; }
        public int? CardService { get; set; }
        public int? TemporaryCardCode { get; set; }
        public string? Neighborhood { get; set; }
        public string? ApproximateTime { get; set; }
        public string? ApproximateCounterTime { get; set; }
        public string? Attention { get; set; }
        public string? DistanceKm { get; set; }
        public string? ScheduledTime { get; set; }
        public string? CardHolderName1 { get; set; }
        public string? CardHolderName2 { get; set; }
        public string? AlphaDescription { get; set; }
        public double DonatedChangeValue { get; set; }
        public string? IsPending { get; set; }
        public string? WebOrderGenerator { get; set; }
        public string? WebReferenceGenerator { get; set; }
        public string? WebGeneratorCode { get; set; }
        public string? WebGeneratorRelation { get; set; }
        public double IfoodCardValue { get; set; }
        public string? IsPaidOnline { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string? ClosingTime { get; set; }
        public string? ClosingAttendantName { get; set; }
        public string? CPFNote { get; set; }
        public string? PrintOnProductionControl { get; set; }
        public string? FreeDelivery { get; set; }
        public string? CounterReady { get; set; }
        public string? CheckNet { get; set; }
        public double? PixValue { get; set; }
        public double? AppPlatformDiscount { get; set; }
        public double? AppStoreDiscount { get; set; }
        public double? ConsumptionValue { get; set; }
        public string? CallDeliveryIntegration { get; set; }
        public string? AuxiliaryDeliveryIntegration { get; set; }

        public CustomerDto? Customer { get; set; }
    }

}

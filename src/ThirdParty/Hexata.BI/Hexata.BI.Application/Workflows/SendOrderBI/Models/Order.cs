using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Workflows.SendOrderBI.Dtos;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Models
{
    public record Order
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal ValueWithDiscount { get; set; }
        public decimal Discount { get; set; }
        public decimal ValueWithoutDiscount { get; set; }
        public int Term { get; set; }
        public decimal Change { get; set; }
        public decimal CustomerValue { get; set; }
        public int? Employee { get; set; }
        public decimal? CashRegister { get; set; }
        public decimal? CommissionValue { get; set; }
        public string? Status { get; set; }
        public string PC { get; set; }
        public int? DeliveryPerson { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public decimal Fee { get; set; }
        public string? Notes { get; set; }
        public string? SaleType { get; set; }
        public decimal? CashValue { get; set; }
        public decimal? CardValue1 { get; set; }
        public decimal? CardValue2 { get; set; }
        public decimal? CheckValue { get; set; }
        public decimal? TermValue { get; set; }
        public decimal? DiscountValue { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Terminal { get; set; }
        public string? PaymentStatus { get; set; }
        public int? CardService { get; set; }
        public string? Neighborhood { get; set; }
        public TimeOnly? ApproximateTime { get; set; }
        public TimeOnly? ApproximateCounterTime { get; set; }
        public string? CardHolderName1 { get; set; }
        public string? CardHolderName2 { get; set; }
        public decimal? DonatedChangeValue { get; set; }
        public string? IsPending { get; set; }
        public string? WebOrderGenerator { get; set; }
        public string? WebReferenceGenerator { get; set; }
        public string? WebGeneratorCode { get; set; }
        public string? WebGeneratorRelation { get; set; }
        public decimal? IfoodCardValue { get; set; }
        public string? IsPaidOnline { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string? ClosingAttendantName { get; set; }
        public string? CPFNote { get; set; }
        public string? FreeDelivery { get; set; }
        public string? CounterReady { get; set; }
        public string? CheckNet { get; set; }
        public decimal? PixValue { get; set; }
        public decimal? AppPlatformDiscount { get; set; }
        public decimal? AppStoreDiscount { get; set; }
        public decimal? ConsumptionValue { get; set; }
        public CustomerDto? Customer { get; set; }

        public LocalizationDto? Localization { get; set; }
    }
}

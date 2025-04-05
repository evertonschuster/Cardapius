using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Entities;
using Hexata.BI.Application.Workflows.SendOrderBI.Dtos;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Models
{
    public record Order : IEntity<int>
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime Date { get; set; }
        public double ValueWithDiscount { get; set; }
        public double Discount { get; set; }
        public double ValueWithoutDiscount { get; set; }
        public int Term { get; set; }
        public double Change { get; set; }
        public double CustomerValue { get; set; }
        public int? Employee { get; set; }
        public double? CashRegister { get; set; }
        public double? CommissionValue { get; set; }
        public string? Status { get; set; }
        public string PC { get; set; }
        public int? DeliveryPerson { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public double Fee { get; set; }
        public string? Notes { get; set; }
        public string? SaleType { get; set; }
        public double? CashValue { get; set; }
        public double? CardValue1 { get; set; }
        public double? CardValue2 { get; set; }
        public double? CheckValue { get; set; }
        public double? TermValue { get; set; }
        public double? DiscountValue { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Terminal { get; set; }
        public string? PaymentStatus { get; set; }
        public int? CardService { get; set; }
        public string? Neighborhood { get; set; }
        public TimeOnly? ApproximateTime { get; set; }
        public TimeOnly? ApproximateCounterTime { get; set; }
        public string? CardHolderName1 { get; set; }
        public string? CardHolderName2 { get; set; }
        public double? DonatedChangeValue { get; set; }
        public string? IsPending { get; set; }
        public string? WebOrderGenerator { get; set; }
        public string? WebReferenceGenerator { get; set; }
        public string? WebGeneratorCode { get; set; }
        public string? WebGeneratorRelation { get; set; }
        public double? IfoodCardValue { get; set; }
        public string? IsPaidOnline { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string? ClosingAttendantName { get; set; }
        public string? CPFNote { get; set; }
        public string? FreeDelivery { get; set; }
        public string? CounterReady { get; set; }
        public string? CheckNet { get; set; }
        public double? PixValue { get; set; }
        public double? AppPlatformDiscount { get; set; }
        public double? AppStoreDiscount { get; set; }
        public double? ConsumptionValue { get; set; }
        public CustomerDto? Customer { get; set; }

        public LocalizationDto? Localization { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

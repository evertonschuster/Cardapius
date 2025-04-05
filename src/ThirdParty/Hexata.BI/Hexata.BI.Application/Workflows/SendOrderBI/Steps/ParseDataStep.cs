using Hexata.BI.Application.Workflows.SendOrderBI.Dtos;
using Hexata.BI.Application.Workflows.SendOrderBI.Models;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    class ParseDataStep : StepBodyAsync
    {
        public SaleDto SaleDto { get; internal set; }

        public Order Order { get; internal set; }

        public override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Order = new Order()
            {
                Id = SaleDto.Id,
                CustomerId = SaleDto.CustomerId,
                Date = DateTime.Parse($"{SaleDto.Date.ToShortDateString()} {SaleDto.Time}"),
                ValueWithDiscount = SaleDto.ValueWithDiscount,
                Discount = SaleDto.Discount,
                ValueWithoutDiscount = SaleDto.ValueWithoutDiscount,
                Term = SaleDto.Term,
                Change = SaleDto.Change,
                CustomerValue = SaleDto.CustomerValue,
                Employee = SaleDto.Employee,
                CashRegister = SaleDto.CashRegister,
                CommissionValue = SaleDto.CommissionValue,
                Status = string.IsNullOrWhiteSpace(SaleDto.Status) ? null : SaleDto.Status, ///Criar Enum

                DeliveryPerson = SaleDto.DeliveryPerson,
                DeliveryDate = SaleDto.DeliveryDate is not null ? DateTime.Parse($"{SaleDto.DeliveryDate.Value.ToShortDateString()} {SaleDto.DeliveryTime}") : null,
                ArrivalDate = SaleDto.ArrivalDate is not null ? DateTime.Parse($"{SaleDto.ArrivalDate?.ToShortDateString()} {SaleDto.ArrivalTime}") : null,

                Fee = SaleDto.Fee,
                Notes = string.IsNullOrWhiteSpace(SaleDto.Notes) ? null : SaleDto.Notes,
                SaleType = string.IsNullOrWhiteSpace(SaleDto.SaleType) ? null : SaleDto.SaleType,
                CashValue = SaleDto.CashValue,
                CardValue1 = SaleDto.CardValue1,
                CardValue2 = SaleDto.CardValue2,
                CheckValue = SaleDto.CheckValue,
                TermValue = SaleDto.TermValue,
                DiscountValue = SaleDto.DiscountValue,
                DeliveryAddress = string.IsNullOrWhiteSpace(SaleDto.DeliveryAddress) ? null : SaleDto.DeliveryAddress,
                Terminal = string.IsNullOrWhiteSpace(SaleDto.Terminal) ? null : SaleDto.Terminal,
                PaymentStatus = string.IsNullOrWhiteSpace(SaleDto.PaymentStatus) ? null : SaleDto.PaymentStatus,
                CardService = SaleDto.CardService,
                Neighborhood = string.IsNullOrWhiteSpace(SaleDto.Neighborhood) ? null : SaleDto.Neighborhood,

                //ApproximateTime = SaleDto.ApproximateTime,
                //ApproximateCounterTime = SaleDto.ApproximateCounterTime,

                CardHolderName1 = string.IsNullOrWhiteSpace(SaleDto.CardHolderName1) ? null : SaleDto.CardHolderName1,
                CardHolderName2 = string.IsNullOrWhiteSpace(SaleDto.CardHolderName2) ? null : SaleDto.CardHolderName2,
                DonatedChangeValue = SaleDto.DonatedChangeValue,
                IsPending = string.IsNullOrWhiteSpace(SaleDto.IsPending) ? null : SaleDto.IsPending,//Enum
                WebOrderGenerator = string.IsNullOrWhiteSpace(SaleDto.WebOrderGenerator) ? null : SaleDto.WebOrderGenerator,
                WebReferenceGenerator = string.IsNullOrWhiteSpace(SaleDto.WebReferenceGenerator) ? null : SaleDto.WebReferenceGenerator,
                WebGeneratorCode = string.IsNullOrWhiteSpace(SaleDto.WebGeneratorCode) ? null : SaleDto.WebGeneratorCode,
                WebGeneratorRelation = string.IsNullOrWhiteSpace(SaleDto.WebGeneratorRelation) ? null : SaleDto.WebGeneratorRelation,
                IfoodCardValue = SaleDto.IfoodCardValue,
                IsPaidOnline = string.IsNullOrWhiteSpace(SaleDto.IsPaidOnline) ? null : SaleDto.IsPaidOnline,

                ClosingDate = SaleDto.ClosingDate is not null ? DateTime.Parse($"{SaleDto.ClosingDate?.ToShortDateString()} {SaleDto.ClosingTime}") : null,

                ClosingAttendantName = string.IsNullOrWhiteSpace(SaleDto.ClosingAttendantName) ? null : SaleDto.ClosingAttendantName,
                CPFNote = string.IsNullOrWhiteSpace(SaleDto.CPFNote) ? null : SaleDto.CPFNote,
                FreeDelivery = string.IsNullOrWhiteSpace(SaleDto.FreeDelivery) ? null : SaleDto.FreeDelivery,//Enum
                CounterReady = string.IsNullOrWhiteSpace(SaleDto.CounterReady) ? null : SaleDto.CounterReady,//Enum
                CheckNet = string.IsNullOrWhiteSpace(SaleDto.CheckNet) ? null : SaleDto.CheckNet,//Enum
                PixValue = SaleDto.PixValue,
                AppPlatformDiscount = SaleDto.AppPlatformDiscount,
                AppStoreDiscount = SaleDto.AppStoreDiscount,
                ConsumptionValue = SaleDto.ConsumptionValue,

                Customer = SaleDto.Customer
            };

            return Task.FromResult(ExecutionResult.Next());
        }
    }
}

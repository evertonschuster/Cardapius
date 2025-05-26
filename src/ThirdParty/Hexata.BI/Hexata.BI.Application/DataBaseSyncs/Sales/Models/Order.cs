using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Services.Localizations;
using System.ComponentModel;

namespace Hexata.BI.Application.DataBaseSyncs.Sales.Models
{
    public class Order
    {
        public int Id { get; set; }

        [DisplayName("Codigo")]
        public int Code { get => this.Id; }

        [DisplayName("Codigo cliente")]
        public int? CustomerId { get; set; }

        [DisplayName("Data")]
        public DateTime Date { get; set; }

        [DisplayName("Codigo funcionario")]
        public int? EmployeeId { get; set; }




        [DisplayName("Valor com desconto")]
        public double? ValueWithDiscount { get; set; }

        [DisplayName("Desconto")]
        public double? Discount { get; set; }

        [DisplayName("Valor sem desconto")]
        public double? ValueWithoutDiscount { get; set; }

        [DisplayName("Valor")]
        public double? Value { get => (this.ValueWithDiscount ?? 0) + (this.ValueWithoutDiscount ?? 0); }

        [DisplayName("Prazo")]
        public int? Term { get; set; }

        [DisplayName("Troco")]
        public double? Change { get; set; }

        [DisplayName("Valor do cliente")]
        public double? CustomerValue { get; set; }

        [DisplayName("Caixa")]
        public double? CashRegister { get; set; }

        [DisplayName("Valor de comissão")]
        public double? CommissionValue { get; set; }

        [DisplayName("Status de pagamento")]
        public PaymentStatus? PaymentStatus { get; set; }

        [DisplayName("Valor em dinheiro")]
        public double? CashValue { get; set; }

        [DisplayName("Valor pago em dinheiro")]
        public double? CashPayValue { get => (CashValue ?? 0) - (Change ?? 0); }

        [DisplayName("Valor pix")]
        public double? PixValue { get; set; }

        [DisplayName("Valor cartao 1")]
        public double? CardValue1 { get; set; }

        [DisplayName("Valor cartao 2")]
        public double? CardValue2 { get; set; }

        [DisplayName("Valor cheque")]
        public double? CheckValue { get; set; }

        [DisplayName("Nome cartao 1")]
        public string? CardHolderName1 { get; set; }

        [DisplayName("Nome cartao 2")]
        public string? CardHolderName2 { get; set; }

        [DisplayName("Valor a prazo")]
        public double? TermValue { get; set; }

        [DisplayName("Valor de desconto")]
        public double? DiscountValue { get; set; }

        [DisplayName("Valor do troco doado")]
        public double? DonatedChangeValue { get; set; }






        //TODO: Criar Enum
        /// <summary>
        /// A
        /// C
        /// F
        /// P
        /// X
        /// </summary>
        [DisplayName("Status")]
        public string? Status { get; set; }

        [DisplayName("Pc")]
        public string? PC { get; set; }

        [DisplayName("Data de chegada")]
        public DateTime? ArrivalDate { get; set; }

        [DisplayName("Observacao")]
        public string? Notes { get; set; }

        [DisplayName("Tipo de venda")]
        public string? SaleType { get; set; }



        [DisplayName("Entrega gratuita")]
        public string? FreeDelivery { get; set; }

        [DisplayName("Data de entrega")]
        public DateTime? DeliveryDate { get; set; }

        [DisplayName("Codigo entregador")]
        public int? DeliveryPersonId { get; set; }

        [DisplayName("Endereço da entrega")]
        public string? DeliveryAddress { get; set; }

        [DisplayName("Bairro")]
        public string? Neighborhood { get; set; }

        //[DisplayName("Endereço de entrega")]
        public AddressDto? Address { get; set; }

        [DisplayName("Terminal")]
        public string? Terminal { get; set; }



        [DisplayName("Horario aproximado")]
        public string? ApproximateTime { get; set; }

        [DisplayName("Horario de aproximado balcao")]
        public string? ApproximateCounterTime { get; set; }


        [DisplayName("Em Espera")]
        public string? IsPending { get; set; }

        public string? WebOrderGenerator { get; set; }

        [DisplayName("Data de fechamento")]
        public DateTime? ClosingDate { get; set; }

        [DisplayName("Nome atendente de fechamento")]
        public string? ClosingAttendantName { get; set; }

        [DisplayName("Pronto balcao")]
        public string? CounterReady { get; set; }


        [DisplayName("Check net")]
        public string? CheckNet { get; set; }


        [DisplayName("Geolocalizacao")]
        public LocalizationDto? Localization { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<OrderItem>? Items { get; set; }
    }
}

using System.ComponentModel;

namespace Hexata.BI.Application.DataBaseSyncs.Customers.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [DisplayName("Codigo")]
        public int Code { get => this.Id; }

        [DisplayName("Nome")]
        public string? Name { get; set; }

        [DisplayName("Nome Fantasia Apelido")]
        public string? TradeName { get; set; }

        [DisplayName("Endereco")]
        public string? Address { get; set; }

        [DisplayName("Numero do endereco")]
        public string? AddressNumber { get; set; }

        [DisplayName("Bairro")]
        public string? District { get; set; }

        [DisplayName("Complemento")]
        public string? Complement { get; set; }

        [DisplayName("Telefone")]
        public string? Phone { get; set; }

        [DisplayName("Telefone 2")]
        public string? Phone2 { get; set; }

        [DisplayName("Celular")]
        public string? Mobile { get; set; }

        [DisplayName("CG CPF")]
        public string? CgcCpf { get; set; }

        [DisplayName("Data de cadastro")]
        public DateTime? RegistrationDate { get; set; }

        [DisplayName("Credito")]
        public double? Credit { get; set; }

        [DisplayName("Situacao credito")]
        public string? CreditStatus { get; set; }

        [DisplayName("Tipo")]
        public string? Type { get; set; }

        [DisplayName("Taxa de entrega")]
        public double? DeliveryFee { get; set; }

        [DisplayName("Dia de vencimento")]
        public int? DefaultDueDay { get; set; }

        [DisplayName("Observacoes")]
        public string? Notes { get; set; }

        [DisplayName("Credito temporario")]
        public string? TemporaryCredit { get; set; }

        [DisplayName("Autorizado cortesia")]
        public string? AllowCourtesy { get; set; }

        [DisplayName("Quantidade de vendas")]
        public int? SalesCount { get; set; }
    }
}

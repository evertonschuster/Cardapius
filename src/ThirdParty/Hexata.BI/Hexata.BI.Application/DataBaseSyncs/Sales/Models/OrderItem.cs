using System.ComponentModel;

namespace Hexata.BI.Application.DataBaseSyncs.Sales.Models
{
    public class OrderItem
    {
        #region Basic Information
        [DisplayName("Codigo")]
        public int Id { get; set; }

        [DisplayName("Codigo de saída")]
        public int ExitId { get; set; }

        [DisplayName("Produto")]
        public string Product { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Codigo do cliente")]
        public int ClientId { get; set; }

        [DisplayName("Telefone do cliente")]
        public string ClientPhone { get; set; }

        [DisplayName("Conteúdo")]
        public string Content { get; set; }

        [DisplayName("Unidade")]
        public string Unit { get; set; }
        #endregion

        #region Sale Details
        [DisplayName("Quantidade")]
        public double Quantity { get; set; }

        [DisplayName("Valor Unitário")]
        public double UnitValue { get; set; }

        [DisplayName("Desconto")]
        public double Discount { get; set; }

        [DisplayName("Total de Desconto")]
        public double TotalDiscount { get; set; }

        [DisplayName("Valor Bruto")]
        public double GrossValue { get; set; }

        [DisplayName("Valor da Comissão")]
        public double CommissionValue { get; set; }

        [DisplayName("Especies auxiliar")]
        public List<string>? AuxiliarySpecies { get; set; }

        #endregion

        #region Employee Information
        [DisplayName("Codigo funcionário")]
        public int? EmployeeId { get; set; }

        [DisplayName("Codigo funcionário do desconto")]
        public int? DiscountEmployeeId { get; set; }

        [DisplayName("Nome do Funcionário")]
        public string EmployeeName { get; set; }

        [DisplayName("Nome do Funcionário de Desconto")]
        public string DiscountEmployeeName { get; set; }

        #endregion

        #region Cash Register and Computer Data
        [DisplayName("Caixa")]
        public string CashRegister { get; set; }

        [DisplayName("Computador")]
        public string Computer { get; set; }

        [DisplayName("Operador do Caixa")]
        public string CashRegisterOperator { get; set; }
        #endregion

        #region Other Information
        [DisplayName("Data")]
        public DateTime Date { get; set; }

        [DisplayName("Situação")]
        public string Situation { get; set; }

        [DisplayName("Data de Retirada")]
        public DateTime? WithdrawalDate { get; set; }

        [DisplayName("Grupo A")]
        public string? GroupA { get; set; }

        [DisplayName("Grupo B")]
        public string? GroupB { get; set; }

        [DisplayName("Observação")]
        public string? Note { get; set; }

        [DisplayName("Situação da Comissão")]
        public string? CommissionSituation { get; set; }

        [DisplayName("Data de Saída")]
        public DateTime ExitDate { get; set; }

        [DisplayName("Data de Retorno")]
        public DateTime? ReturnDate { get; set; }

        #endregion

        [DisplayName("CODESP1")]
        public string? CODESP1 { get; set; }

        [DisplayName("CODESP2")]
        public string? CODESP2 { get; set; }

        [DisplayName("CODESP3")]
        public string? CODESP3 { get; set; }

        [DisplayName("CODESP4")]
        public string? CODESP4 { get; set; }

        [DisplayName("CODESP5")]
        public string? CODESP5 { get; set; }

        [DisplayName("CODESP6")]
        public string? CODESP6 { get; set; }

        [DisplayName("CODESP7")]
        public string? CODESP7 { get; set; }

        [DisplayName("CODESP8")]
        public string? CODESP8 { get; set; }

        [DisplayName("CODESP9")]
        public string? CODESP9 { get; set; }

        [DisplayName("CODESP10")]
        public string? CODESP10 { get; set; }
    }
}

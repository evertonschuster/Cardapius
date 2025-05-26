using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexata.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class inicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "PC",
                table: "Orders",
                newName: "Pc");

            migrationBuilder.RenameColumn(
                name: "ValueWithoutDiscount",
                table: "Orders",
                newName: "Valor sem desconto");

            migrationBuilder.RenameColumn(
                name: "ValueWithDiscount",
                table: "Orders",
                newName: "Valor com desconto");

            migrationBuilder.RenameColumn(
                name: "TermValue",
                table: "Orders",
                newName: "Valor a prazo");

            migrationBuilder.RenameColumn(
                name: "Term",
                table: "Orders",
                newName: "Prazo");

            migrationBuilder.RenameColumn(
                name: "SaleType",
                table: "Orders",
                newName: "Tipo de venda");

            migrationBuilder.RenameColumn(
                name: "PixValue",
                table: "Orders",
                newName: "Valor pix");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Orders",
                newName: "Status de pagamento");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Orders",
                newName: "Observacao");

            migrationBuilder.RenameColumn(
                name: "Neighborhood",
                table: "Orders",
                newName: "Bairro");

            migrationBuilder.RenameColumn(
                name: "IsPending",
                table: "Orders",
                newName: "Em Espera");

            migrationBuilder.RenameColumn(
                name: "FreeDelivery",
                table: "Orders",
                newName: "Entrega gratuita");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Orders",
                newName: "Codigo funcionario");

            migrationBuilder.RenameColumn(
                name: "DonatedChangeValue",
                table: "Orders",
                newName: "Valor do troco doado");

            migrationBuilder.RenameColumn(
                name: "DiscountValue",
                table: "Orders",
                newName: "Valor de desconto");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Orders",
                newName: "Desconto");

            migrationBuilder.RenameColumn(
                name: "DeliveryPersonId",
                table: "Orders",
                newName: "Codigo entregador");

            migrationBuilder.RenameColumn(
                name: "DeliveryDate",
                table: "Orders",
                newName: "Data de entrega");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress",
                table: "Orders",
                newName: "Endereço da entrega");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Orders",
                newName: "Data");

            migrationBuilder.RenameColumn(
                name: "CustomerValue",
                table: "Orders",
                newName: "Valor do cliente");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "Codigo cliente");

            migrationBuilder.RenameColumn(
                name: "CounterReady",
                table: "Orders",
                newName: "Pronto balcao");

            migrationBuilder.RenameColumn(
                name: "CommissionValue",
                table: "Orders",
                newName: "Valor de comissão");

            migrationBuilder.RenameColumn(
                name: "ClosingDate",
                table: "Orders",
                newName: "Data de fechamento");

            migrationBuilder.RenameColumn(
                name: "ClosingAttendantName",
                table: "Orders",
                newName: "Nome atendente de fechamento");

            migrationBuilder.RenameColumn(
                name: "CheckValue",
                table: "Orders",
                newName: "Valor cheque");

            migrationBuilder.RenameColumn(
                name: "CheckNet",
                table: "Orders",
                newName: "Check net");

            migrationBuilder.RenameColumn(
                name: "Change",
                table: "Orders",
                newName: "Troco");

            migrationBuilder.RenameColumn(
                name: "CashValue",
                table: "Orders",
                newName: "Valor em dinheiro");

            migrationBuilder.RenameColumn(
                name: "CashRegister",
                table: "Orders",
                newName: "Caixa");

            migrationBuilder.RenameColumn(
                name: "CardValue2",
                table: "Orders",
                newName: "Valor cartao 2");

            migrationBuilder.RenameColumn(
                name: "CardValue1",
                table: "Orders",
                newName: "Valor cartao 1");

            migrationBuilder.RenameColumn(
                name: "CardHolderName2",
                table: "Orders",
                newName: "Nome cartao 2");

            migrationBuilder.RenameColumn(
                name: "CardHolderName1",
                table: "Orders",
                newName: "Nome cartao 1");

            migrationBuilder.RenameColumn(
                name: "ArrivalDate",
                table: "Orders",
                newName: "Data de chegada");

            migrationBuilder.RenameColumn(
                name: "ApproximateTime",
                table: "Orders",
                newName: "Horario aproximado");

            migrationBuilder.RenameColumn(
                name: "ApproximateCounterTime",
                table: "Orders",
                newName: "Horario de aproximado balcao");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Date",
                table: "Orders",
                newName: "IX_Orders_Data");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_Codigo cliente");

            migrationBuilder.RenameColumn(
                name: "WithdrawalDate",
                table: "OrderItems",
                newName: "Data de retirada");

            migrationBuilder.RenameColumn(
                name: "UnitValue",
                table: "OrderItems",
                newName: "Valor unitario");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "OrderItems",
                newName: "Unidade");

            migrationBuilder.RenameColumn(
                name: "TotalDiscount",
                table: "OrderItems",
                newName: "Total de desconto");

            migrationBuilder.RenameColumn(
                name: "Situation",
                table: "OrderItems",
                newName: "Situacao");

            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "OrderItems",
                newName: "Data de retorno");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItems",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "Product",
                table: "OrderItems",
                newName: "Produto");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderItems",
                newName: "Codigo de saida");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "OrderItems",
                newName: "Observação");

            migrationBuilder.RenameColumn(
                name: "GroupB",
                table: "OrderItems",
                newName: "Grupo B");

            migrationBuilder.RenameColumn(
                name: "GroupA",
                table: "OrderItems",
                newName: "Grupo A");

            migrationBuilder.RenameColumn(
                name: "GrossValue",
                table: "OrderItems",
                newName: "Valor bruto");

            migrationBuilder.RenameColumn(
                name: "ExitDate",
                table: "OrderItems",
                newName: "Data de saida");

            migrationBuilder.RenameColumn(
                name: "EmployeeName",
                table: "OrderItems",
                newName: "Nome do funcionario");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "OrderItems",
                newName: "Codigo funcionario");

            migrationBuilder.RenameColumn(
                name: "DiscountEmployeeName",
                table: "OrderItems",
                newName: "Nome do funcionario de desconto");

            migrationBuilder.RenameColumn(
                name: "DiscountEmployeeId",
                table: "OrderItems",
                newName: "Codigo funcionario do desconto");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "OrderItems",
                newName: "Desconto");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "OrderItems",
                newName: "Descrição");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "OrderItems",
                newName: "Data");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "OrderItems",
                newName: "Conteudo");

            migrationBuilder.RenameColumn(
                name: "Computer",
                table: "OrderItems",
                newName: "Computador");

            migrationBuilder.RenameColumn(
                name: "CommissionValue",
                table: "OrderItems",
                newName: "Valor da comissao");

            migrationBuilder.RenameColumn(
                name: "CommissionSituation",
                table: "OrderItems",
                newName: "Situação da comissao");

            migrationBuilder.RenameColumn(
                name: "ClientPhone",
                table: "OrderItems",
                newName: "Telefone do cliente");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "OrderItems",
                newName: "Codigo do cliente");

            migrationBuilder.RenameColumn(
                name: "CashRegisterOperator",
                table: "OrderItems",
                newName: "Operador do caixa");

            migrationBuilder.RenameColumn(
                name: "CashRegister",
                table: "OrderItems",
                newName: "Caixa");

            migrationBuilder.RenameColumn(
                name: "AuxiliarySpecies",
                table: "OrderItems",
                newName: "Especies auxiliar");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId_Product",
                table: "OrderItems",
                newName: "IX_OrderItems_Codigo de saida_Produto");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_Codigo de saida");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Customers",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "TradeName",
                table: "Customers",
                newName: "Nome Fantasia Apelido");

            migrationBuilder.RenameColumn(
                name: "TemporaryCredit",
                table: "Customers",
                newName: "Credito temporario");

            migrationBuilder.RenameColumn(
                name: "SalesCount",
                table: "Customers",
                newName: "Quantidade de vendas");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "Customers",
                newName: "Data de cadastro");

            migrationBuilder.RenameColumn(
                name: "Phone2",
                table: "Customers",
                newName: "Telefone 2");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Customers",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Customers",
                newName: "Observacoes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "Customers",
                newName: "Celular");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Customers",
                newName: "Bairro");

            migrationBuilder.RenameColumn(
                name: "DeliveryFee",
                table: "Customers",
                newName: "Taxa de entrega");

            migrationBuilder.RenameColumn(
                name: "DefaultDueDay",
                table: "Customers",
                newName: "Dia de vencimento");

            migrationBuilder.RenameColumn(
                name: "CreditStatus",
                table: "Customers",
                newName: "Situacao credito");

            migrationBuilder.RenameColumn(
                name: "Credit",
                table: "Customers",
                newName: "Credito");

            migrationBuilder.RenameColumn(
                name: "Complement",
                table: "Customers",
                newName: "Complemento");

            migrationBuilder.RenameColumn(
                name: "CgcCpf",
                table: "Customers",
                newName: "CG CPF");

            migrationBuilder.RenameColumn(
                name: "AllowCourtesy",
                table: "Customers",
                newName: "Autorizado cortesia");

            migrationBuilder.RenameColumn(
                name: "AddressNumber",
                table: "Customers",
                newName: "Numero do endereco");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customers",
                newName: "Endereco");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_Codigo de saida",
                table: "OrderItems",
                column: "Codigo de saida",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_Codigo de saida",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Pc",
                table: "Orders",
                newName: "PC");

            migrationBuilder.RenameColumn(
                name: "Valor sem desconto",
                table: "Orders",
                newName: "ValueWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "Valor pix",
                table: "Orders",
                newName: "PixValue");

            migrationBuilder.RenameColumn(
                name: "Valor em dinheiro",
                table: "Orders",
                newName: "CashValue");

            migrationBuilder.RenameColumn(
                name: "Valor do troco doado",
                table: "Orders",
                newName: "DonatedChangeValue");

            migrationBuilder.RenameColumn(
                name: "Valor do cliente",
                table: "Orders",
                newName: "CustomerValue");

            migrationBuilder.RenameColumn(
                name: "Valor de desconto",
                table: "Orders",
                newName: "DiscountValue");

            migrationBuilder.RenameColumn(
                name: "Valor de comissão",
                table: "Orders",
                newName: "CommissionValue");

            migrationBuilder.RenameColumn(
                name: "Valor com desconto",
                table: "Orders",
                newName: "ValueWithDiscount");

            migrationBuilder.RenameColumn(
                name: "Valor cheque",
                table: "Orders",
                newName: "CheckValue");

            migrationBuilder.RenameColumn(
                name: "Valor cartao 2",
                table: "Orders",
                newName: "CardValue2");

            migrationBuilder.RenameColumn(
                name: "Valor cartao 1",
                table: "Orders",
                newName: "CardValue1");

            migrationBuilder.RenameColumn(
                name: "Valor a prazo",
                table: "Orders",
                newName: "TermValue");

            migrationBuilder.RenameColumn(
                name: "Troco",
                table: "Orders",
                newName: "Change");

            migrationBuilder.RenameColumn(
                name: "Tipo de venda",
                table: "Orders",
                newName: "SaleType");

            migrationBuilder.RenameColumn(
                name: "Status de pagamento",
                table: "Orders",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "Pronto balcao",
                table: "Orders",
                newName: "CounterReady");

            migrationBuilder.RenameColumn(
                name: "Prazo",
                table: "Orders",
                newName: "Term");

            migrationBuilder.RenameColumn(
                name: "Observacao",
                table: "Orders",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Nome cartao 2",
                table: "Orders",
                newName: "CardHolderName2");

            migrationBuilder.RenameColumn(
                name: "Nome cartao 1",
                table: "Orders",
                newName: "CardHolderName1");

            migrationBuilder.RenameColumn(
                name: "Nome atendente de fechamento",
                table: "Orders",
                newName: "ClosingAttendantName");

            migrationBuilder.RenameColumn(
                name: "Horario de aproximado balcao",
                table: "Orders",
                newName: "ApproximateCounterTime");

            migrationBuilder.RenameColumn(
                name: "Horario aproximado",
                table: "Orders",
                newName: "ApproximateTime");

            migrationBuilder.RenameColumn(
                name: "Entrega gratuita",
                table: "Orders",
                newName: "FreeDelivery");

            migrationBuilder.RenameColumn(
                name: "Endereço da entrega",
                table: "Orders",
                newName: "DeliveryAddress");

            migrationBuilder.RenameColumn(
                name: "Em Espera",
                table: "Orders",
                newName: "IsPending");

            migrationBuilder.RenameColumn(
                name: "Desconto",
                table: "Orders",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "Data de fechamento",
                table: "Orders",
                newName: "ClosingDate");

            migrationBuilder.RenameColumn(
                name: "Data de entrega",
                table: "Orders",
                newName: "DeliveryDate");

            migrationBuilder.RenameColumn(
                name: "Data de chegada",
                table: "Orders",
                newName: "ArrivalDate");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Orders",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Codigo funcionario",
                table: "Orders",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "Codigo entregador",
                table: "Orders",
                newName: "DeliveryPersonId");

            migrationBuilder.RenameColumn(
                name: "Codigo cliente",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Check net",
                table: "Orders",
                newName: "CheckNet");

            migrationBuilder.RenameColumn(
                name: "Caixa",
                table: "Orders",
                newName: "CashRegister");

            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Orders",
                newName: "Neighborhood");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Data",
                table: "Orders",
                newName: "IX_Orders_Date");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Codigo cliente",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameColumn(
                name: "Valor unitario",
                table: "OrderItems",
                newName: "UnitValue");

            migrationBuilder.RenameColumn(
                name: "Valor da comissao",
                table: "OrderItems",
                newName: "CommissionValue");

            migrationBuilder.RenameColumn(
                name: "Valor bruto",
                table: "OrderItems",
                newName: "GrossValue");

            migrationBuilder.RenameColumn(
                name: "Unidade",
                table: "OrderItems",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "Total de desconto",
                table: "OrderItems",
                newName: "TotalDiscount");

            migrationBuilder.RenameColumn(
                name: "Telefone do cliente",
                table: "OrderItems",
                newName: "ClientPhone");

            migrationBuilder.RenameColumn(
                name: "Situação da comissao",
                table: "OrderItems",
                newName: "CommissionSituation");

            migrationBuilder.RenameColumn(
                name: "Situacao",
                table: "OrderItems",
                newName: "Situation");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "OrderItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Produto",
                table: "OrderItems",
                newName: "Product");

            migrationBuilder.RenameColumn(
                name: "Operador do caixa",
                table: "OrderItems",
                newName: "CashRegisterOperator");

            migrationBuilder.RenameColumn(
                name: "Observação",
                table: "OrderItems",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "Nome do funcionario de desconto",
                table: "OrderItems",
                newName: "DiscountEmployeeName");

            migrationBuilder.RenameColumn(
                name: "Nome do funcionario",
                table: "OrderItems",
                newName: "EmployeeName");

            migrationBuilder.RenameColumn(
                name: "Grupo B",
                table: "OrderItems",
                newName: "GroupB");

            migrationBuilder.RenameColumn(
                name: "Grupo A",
                table: "OrderItems",
                newName: "GroupA");

            migrationBuilder.RenameColumn(
                name: "Especies auxiliar",
                table: "OrderItems",
                newName: "AuxiliarySpecies");

            migrationBuilder.RenameColumn(
                name: "Descrição",
                table: "OrderItems",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Desconto",
                table: "OrderItems",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "Data de saida",
                table: "OrderItems",
                newName: "ExitDate");

            migrationBuilder.RenameColumn(
                name: "Data de retorno",
                table: "OrderItems",
                newName: "ReturnDate");

            migrationBuilder.RenameColumn(
                name: "Data de retirada",
                table: "OrderItems",
                newName: "WithdrawalDate");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "OrderItems",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Conteudo",
                table: "OrderItems",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Computador",
                table: "OrderItems",
                newName: "Computer");

            migrationBuilder.RenameColumn(
                name: "Codigo funcionario do desconto",
                table: "OrderItems",
                newName: "DiscountEmployeeId");

            migrationBuilder.RenameColumn(
                name: "Codigo funcionario",
                table: "OrderItems",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "Codigo do cliente",
                table: "OrderItems",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "Codigo de saida",
                table: "OrderItems",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "Caixa",
                table: "OrderItems",
                newName: "CashRegister");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_Codigo de saida_Produto",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId_Product");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_Codigo de saida",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Customers",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Telefone 2",
                table: "Customers",
                newName: "Phone2");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Customers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Taxa de entrega",
                table: "Customers",
                newName: "DeliveryFee");

            migrationBuilder.RenameColumn(
                name: "Situacao credito",
                table: "Customers",
                newName: "CreditStatus");

            migrationBuilder.RenameColumn(
                name: "Quantidade de vendas",
                table: "Customers",
                newName: "SalesCount");

            migrationBuilder.RenameColumn(
                name: "Observacoes",
                table: "Customers",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Numero do endereco",
                table: "Customers",
                newName: "AddressNumber");

            migrationBuilder.RenameColumn(
                name: "Nome Fantasia Apelido",
                table: "Customers",
                newName: "TradeName");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Customers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "Customers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Dia de vencimento",
                table: "Customers",
                newName: "DefaultDueDay");

            migrationBuilder.RenameColumn(
                name: "Data de cadastro",
                table: "Customers",
                newName: "RegistrationDate");

            migrationBuilder.RenameColumn(
                name: "Credito temporario",
                table: "Customers",
                newName: "TemporaryCredit");

            migrationBuilder.RenameColumn(
                name: "Credito",
                table: "Customers",
                newName: "Credit");

            migrationBuilder.RenameColumn(
                name: "Complemento",
                table: "Customers",
                newName: "Complement");

            migrationBuilder.RenameColumn(
                name: "Celular",
                table: "Customers",
                newName: "Mobile");

            migrationBuilder.RenameColumn(
                name: "CG CPF",
                table: "Customers",
                newName: "CgcCpf");

            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Customers",
                newName: "District");

            migrationBuilder.RenameColumn(
                name: "Autorizado cortesia",
                table: "Customers",
                newName: "AllowCourtesy");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

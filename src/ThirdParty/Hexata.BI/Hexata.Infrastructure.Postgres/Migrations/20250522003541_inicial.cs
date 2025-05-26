using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hexata.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TradeName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    AddressNumber = table.Column<string>(type: "text", nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    Complement = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Phone2 = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    CgcCpf = table.Column<string>(type: "text", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Credit = table.Column<double>(type: "double precision", nullable: true),
                    CreditStatus = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    DeliveryFee = table.Column<double>(type: "double precision", nullable: true),
                    DefaultDueDay = table.Column<int>(type: "integer", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    TemporaryCredit = table.Column<string>(type: "text", nullable: true),
                    AllowCourtesy = table.Column<string>(type: "text", nullable: true),
                    SalesCount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    ValueWithDiscount = table.Column<double>(type: "double precision", nullable: true),
                    Discount = table.Column<double>(type: "double precision", nullable: true),
                    ValueWithoutDiscount = table.Column<double>(type: "double precision", nullable: true),
                    Term = table.Column<int>(type: "integer", nullable: true),
                    Change = table.Column<double>(type: "double precision", nullable: true),
                    CustomerValue = table.Column<double>(type: "double precision", nullable: true),
                    CashRegister = table.Column<double>(type: "double precision", nullable: true),
                    CommissionValue = table.Column<double>(type: "double precision", nullable: true),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: true),
                    CashValue = table.Column<double>(type: "double precision", nullable: true),
                    PixValue = table.Column<double>(type: "double precision", nullable: true),
                    CardValue1 = table.Column<double>(type: "double precision", nullable: true),
                    CardValue2 = table.Column<double>(type: "double precision", nullable: true),
                    CheckValue = table.Column<double>(type: "double precision", nullable: true),
                    CardHolderName1 = table.Column<string>(type: "text", nullable: true),
                    CardHolderName2 = table.Column<string>(type: "text", nullable: true),
                    TermValue = table.Column<double>(type: "double precision", nullable: true),
                    DiscountValue = table.Column<double>(type: "double precision", nullable: true),
                    DonatedChangeValue = table.Column<double>(type: "double precision", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    PC = table.Column<string>(type: "text", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    SaleType = table.Column<string>(type: "text", nullable: true),
                    FreeDelivery = table.Column<string>(type: "text", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveryPersonId = table.Column<int>(type: "integer", nullable: true),
                    DeliveryAddress = table.Column<string>(type: "text", nullable: true),
                    Neighborhood = table.Column<string>(type: "text", nullable: true),
                    Terminal = table.Column<string>(type: "text", nullable: true),
                    ApproximateTime = table.Column<string>(type: "text", nullable: true),
                    ApproximateCounterTime = table.Column<string>(type: "text", nullable: true),
                    IsPending = table.Column<string>(type: "text", nullable: true),
                    WebOrderGenerator = table.Column<string>(type: "text", nullable: true),
                    ClosingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ClosingAttendantName = table.Column<string>(type: "text", nullable: true),
                    CounterReady = table.Column<string>(type: "text", nullable: true),
                    CheckNet = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Address = table.Column<string>(type: "jsonb", nullable: true),
                    Localization = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Product = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    ClientPhone = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    UnitValue = table.Column<double>(type: "double precision", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false),
                    TotalDiscount = table.Column<double>(type: "double precision", nullable: false),
                    GrossValue = table.Column<double>(type: "double precision", nullable: false),
                    CommissionValue = table.Column<double>(type: "double precision", nullable: false),
                    AuxiliarySpecies = table.Column<List<string>>(type: "text[]", nullable: true),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    DiscountEmployeeId = table.Column<int>(type: "integer", nullable: true),
                    EmployeeName = table.Column<string>(type: "text", nullable: false),
                    DiscountEmployeeName = table.Column<string>(type: "text", nullable: false),
                    CashRegister = table.Column<string>(type: "text", nullable: false),
                    Computer = table.Column<string>(type: "text", nullable: false),
                    CashRegisterOperator = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Situation = table.Column<string>(type: "text", nullable: false),
                    WithdrawalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GroupA = table.Column<string>(type: "text", nullable: true),
                    GroupB = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CommissionSituation = table.Column<string>(type: "text", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CODESP1 = table.Column<string>(type: "text", nullable: true),
                    CODESP2 = table.Column<string>(type: "text", nullable: true),
                    CODESP3 = table.Column<string>(type: "text", nullable: true),
                    CODESP4 = table.Column<string>(type: "text", nullable: true),
                    CODESP5 = table.Column<string>(type: "text", nullable: true),
                    CODESP6 = table.Column<string>(type: "text", nullable: true),
                    CODESP7 = table.Column<string>(type: "text", nullable: true),
                    CODESP8 = table.Column<string>(type: "text", nullable: true),
                    CODESP9 = table.Column<string>(type: "text", nullable: true),
                    CODESP10 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemAuxiliarySpecies",
                columns: table => new
                {
                    Codigodesaida = table.Column<int>(name: "Codigo de saida", type: "integer", nullable: false),
                    Codigosaidaitem = table.Column<int>(name: "Codigo saida item", type: "integer", nullable: false),
                    Especieauxiliar = table.Column<string>(name: "Especie auxiliar", type: "text", nullable: false),
                    Codigosaidaitemespecieauxiliar = table.Column<string>(name: "Codigo saida item especie auxiliar", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemAuxiliarySpecies", x => new { x.Codigodesaida, x.Codigosaidaitem, x.Especieauxiliar });
                    table.ForeignKey(
                        name: "FK_OrderItemAuxiliarySpecies_OrderItems_Codigo saida item",
                        column: x => x.Codigosaidaitem,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemAuxiliarySpecies_Codigo saida item",
                table: "OrderItemAuxiliarySpecies",
                column: "Codigo saida item");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId_Product",
                table: "OrderItems",
                columns: new[] { "OrderId", "Product" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Date",
                table: "Orders",
                column: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OrderItemAuxiliarySpecies");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexata.Infrastructure.SqlLite.Migrations
{
    /// <inheritdoc />
    public partial class ALteraçãoCavePK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_MonthlyConsumptions_Id_Month",
                table: "MonthlyConsumptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonthlyConsumptions",
                table: "MonthlyConsumptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonthlyConsumptions",
                table: "MonthlyConsumptions",
                columns: new[] { "Id", "Month" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MonthlyConsumptions",
                table: "MonthlyConsumptions");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MonthlyConsumptions_Id_Month",
                table: "MonthlyConsumptions",
                columns: new[] { "Id", "Month" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonthlyConsumptions",
                table: "MonthlyConsumptions",
                column: "Id");
        }
    }
}

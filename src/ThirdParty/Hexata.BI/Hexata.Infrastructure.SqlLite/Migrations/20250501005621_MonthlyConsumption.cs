using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexata.Infrastructure.SqlLite.Migrations
{
    /// <inheritdoc />
    public partial class MonthlyConsumption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyConsumptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Month = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyConsumptions", x => x.Id);
                    table.UniqueConstraint("AK_MonthlyConsumptions_Id_Month", x => new { x.Id, x.Month });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyConsumptions");
        }
    }
}

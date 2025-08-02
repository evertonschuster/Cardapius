using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Infra.DataBase.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "Restaurants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "Restaurants",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductSideDishes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductSideDishes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductPreferences",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductPreferences",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductPreferenceItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductPreferenceItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductImages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductImages",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductFlavors",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductFlavors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductFlavorItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductFlavorItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductAdditionals",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductAdditionals",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductAdditionalItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductAdditionalItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                schema: "Administration",
                table: "OutboxMessageEntities",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Administration",
                table: "OutboxMessageEntities",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductSideDishes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductSideDishes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductPreferences");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductPreferences");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductPreferenceItems");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductPreferenceItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductFlavors");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductFlavors");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductFlavorItems");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductFlavorItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductAdditionals");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductAdditionals");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "ProductAdditionalItems");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "ProductAdditionalItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Administration",
                table: "OutboxMessageEntities");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Administration",
                table: "OutboxMessageEntities");
        }
    }
}

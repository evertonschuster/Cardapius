using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Administration.Infra.DataBase.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <summary>
        /// Applies schema changes to introduce the "Administration" schema, move the "Restaurants" table into it, alter the "Complement" column to be nullable, and create new tables for products, product options, images, side dishes, and outbox messages with appropriate relationships and indexes.
        /// </summary>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Administration");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "Restaurants",
                newSchema: "Administration");

            migrationBuilder.AlterColumn<string>(
                name: "Complement",
                schema: "Administration",
                table: "Restaurants",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "OutboxMessageEntities",
                schema: "Administration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    EntityType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Payload = table.Column<string>(type: "text", nullable: false),
                    OccurredOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ProcessedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    SyncSendAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    SynReceivedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    SynReceivedFrom = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessageEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Administration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price_Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Price_MaxDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    Price_ProductionCost = table.Column<decimal>(type: "numeric", nullable: false),
                    PreparationTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ServesManyPeople_Reference = table.Column<int>(type: "integer", nullable: true),
                    ServesManyPeople_Min = table.Column<int>(type: "integer", nullable: true),
                    ServesManyPeople_Max = table.Column<int>(type: "integer", nullable: true),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAdditionals",
                schema: "Administration",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Min = table.Column<int>(type: "integer", nullable: true),
                    Max = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdditionals", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductAdditionals_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Administration",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFlavors",
                schema: "Administration",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Min = table.Column<int>(type: "integer", nullable: true),
                    Max = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFlavors", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductFlavors_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Administration",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                schema: "Administration",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uri = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    AlternativeText = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    ThumbnailUri = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    BlurHash = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => new { x.ProductId, x.Id });
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Administration",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPreferences",
                schema: "Administration",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Min = table.Column<int>(type: "integer", nullable: true),
                    Max = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPreferences", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductPreferences_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Administration",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSideDishes",
                schema: "Administration",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    SideDishId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSideDishes", x => new { x.ProductId, x.SideDishId });
                    table.ForeignKey(
                        name: "FK_ProductSideDishes_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Administration",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSideDishes_Products_SideDishId",
                        column: x => x.SideDishId,
                        principalSchema: "Administration",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAdditionalItems",
                schema: "Administration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Images = table.Column<List<string>>(type: "text[]", nullable: false),
                    Price_Value = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_MaxDiscount = table.Column<decimal>(type: "numeric", nullable: true),
                    ProductAdditionalId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdditionalItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAdditionalItems_ProductAdditionals_ProductAdditional~",
                        column: x => x.ProductAdditionalId,
                        principalSchema: "Administration",
                        principalTable: "ProductAdditionals",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFlavorItems",
                schema: "Administration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Images = table.Column<List<string>>(type: "text[]", nullable: false),
                    Price_Value = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_MaxDiscount = table.Column<decimal>(type: "numeric", nullable: true),
                    ProductFlavorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFlavorItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFlavorItems_ProductFlavors_ProductFlavorId",
                        column: x => x.ProductFlavorId,
                        principalSchema: "Administration",
                        principalTable: "ProductFlavors",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPreferenceItems",
                schema: "Administration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Images = table.Column<List<string>>(type: "text[]", nullable: false),
                    Price_Value = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_MaxDiscount = table.Column<decimal>(type: "numeric", nullable: true),
                    ProductPreferenceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPreferenceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPreferenceItems_ProductPreferences_ProductPreference~",
                        column: x => x.ProductPreferenceId,
                        principalSchema: "Administration",
                        principalTable: "ProductPreferences",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdditionalItems_ProductAdditionalId",
                schema: "Administration",
                table: "ProductAdditionalItems",
                column: "ProductAdditionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFlavorItems_ProductFlavorId",
                schema: "Administration",
                table: "ProductFlavorItems",
                column: "ProductFlavorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPreferenceItems_ProductPreferenceId",
                schema: "Administration",
                table: "ProductPreferenceItems",
                column: "ProductPreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSideDishes_SideDishId",
                schema: "Administration",
                table: "ProductSideDishes",
                column: "SideDishId");
        }

        /// <summary>
        /// Reverts the schema changes applied in the migration by dropping all product-related tables in the "Administration" schema, moving the "Restaurants" table back to its original schema, and restoring the "Complement" column to be non-nullable with a default value.
        /// </summary>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessageEntities",
                schema: "Administration");

            migrationBuilder.DropTable(
                name: "ProductAdditionalItems",
                schema: "Administration");

            migrationBuilder.DropTable(
                name: "ProductFlavorItems",
                schema: "Administration");

            migrationBuilder.DropTable(
                name: "ProductImages",
                schema: "Administration");

            migrationBuilder.DropTable(
                name: "ProductPreferenceItems",
                schema: "Administration");

            migrationBuilder.DropTable(
                name: "ProductSideDishes",
                schema: "Administration");

            migrationBuilder.DropTable(
                name: "ProductAdditionals",
                schema: "Administration");

            migrationBuilder.DropTable(
                name: "ProductFlavors",
                schema: "Administration");

            migrationBuilder.DropTable(
                name: "ProductPreferences",
                schema: "Administration");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Administration");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                schema: "Administration",
                newName: "Restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "Complement",
                table: "Restaurants",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}

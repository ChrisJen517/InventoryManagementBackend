using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class VendorImplementation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21ab8059-8b6b-4059-a3d6-e5d2b2d38ed7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a059b1f9-0b74-45be-a828-e18a3713619a");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Locations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73afdc93-a8c3-463a-aada-09da8c32c842", "2", "Vendor", "Vendor" },
                    { "b92a99cf-fc81-4063-a376-d368d884fabc", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_VendorId",
                table: "Products",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_VendorId",
                table: "Locations",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_VendorId",
                table: "Categories",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VendorId",
                table: "AspNetUsers",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Vendors_VendorId",
                table: "AspNetUsers",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Vendors_VendorId",
                table: "Categories",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Vendors_VendorId",
                table: "Locations",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Vendors_VendorId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Vendors_VendorId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Vendors_VendorId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Products_VendorId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Locations_VendorId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Categories_VendorId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VendorId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73afdc93-a8c3-463a-aada-09da8c32c842");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b92a99cf-fc81-4063-a376-d368d884fabc");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Products",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21ab8059-8b6b-4059-a3d6-e5d2b2d38ed7", "2", "Vendor", "Vendor" },
                    { "a059b1f9-0b74-45be-a828-e18a3713619a", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

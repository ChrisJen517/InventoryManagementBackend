using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProductDefaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Vendors_VendorId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73afdc93-a8c3-463a-aada-09da8c32c842");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b92a99cf-fc81-4063-a376-d368d884fabc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "151af3e0-f0a8-4ac8-9756-b47ec45a4177", "2", "Vendor", "Vendor" },
                    { "528de149-ae22-4a99-b9bd-b0d5afd2e8f6", "1", "Admin", "Admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Vendors_VendorId",
                table: "AspNetUsers",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Vendors_VendorId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "151af3e0-f0a8-4ac8-9756-b47ec45a4177");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528de149-ae22-4a99-b9bd-b0d5afd2e8f6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73afdc93-a8c3-463a-aada-09da8c32c842", "2", "Vendor", "Vendor" },
                    { "b92a99cf-fc81-4063-a376-d368d884fabc", "1", "Admin", "Admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Vendors_VendorId",
                table: "AspNetUsers",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id");
        }
    }
}

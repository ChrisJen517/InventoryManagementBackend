using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class ModelColumnsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Categories_Vendors_VendorId",
            //     table: "Categories");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "22200393-5e3e-46d9-bcf1-a507a758e84d");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "ac9c3166-70e8-4aae-9d3b-d7e19123290c");

            // migrationBuilder.InsertData(
            //     table: "AspNetRoles",
            //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //     values: new object[,]
            //     {
            //         { "77c5515f-f26b-46cf-9d81-5f432d960517", "1", "Admin", "Admin" },
            //         { "b0d26eb7-8a9c-443c-9192-a276176ad3e5", "2", "Vendor", "Vendor" }
            //     });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Vendors_VendorId",
                table: "Categories",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Categories_Vendors_VendorId",
            //     table: "Categories");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "77c5515f-f26b-46cf-9d81-5f432d960517");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "b0d26eb7-8a9c-443c-9192-a276176ad3e5");

            // migrationBuilder.InsertData(
            //     table: "AspNetRoles",
            //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //     values: new object[,]
            //     {
            //         { "22200393-5e3e-46d9-bcf1-a507a758e84d", "1", "Admin", "Admin" },
            //         { "ac9c3166-70e8-4aae-9d3b-d7e19123290c", "2", "Vendor", "Vendor" }
            //     });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Vendors_VendorId",
                table: "Categories",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

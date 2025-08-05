using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Project.theDbcontext.Migrations
{
    /// <inheritdoc />
    public partial class seeding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "test-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6620c1d3-5a0b-413d-b566-f4aaba34350b", "AQAAAAIAAYagAAAAEDSstol0NS8TgW/2IhsXgaxAQh84icFKhm9cETx+ole1jSCqsVPM3rbioIwpILeroA==", "ef541f2c-61a9-438b-aa50-8c9daaa5a02a" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 8, 3, 14, 9, 12, 331, DateTimeKind.Utc).AddTicks(5415), new DateTime(2026, 8, 3, 14, 9, 12, 331, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "Id", "CreatedAt", "Discount", "IsDeleted", "OrderDate", "OrderType", "ShippingAddress", "ShippingCost", "Status", "Subtotal", "Tax", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, false, new DateTime(2025, 8, 3, 14, 9, 12, 331, DateTimeKind.Utc).AddTicks(5520), 1, "123 Test Street", 15m, 1, 200m, 20m, null, "test-user-id" });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "OrderId", "ProductId", "Quantity", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 1, 2, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "test-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33171f36-2499-44f5-b303-b5b4fdf5b753", "AQAAAAIAAYagAAAAEAqmgKkUeNEg5W6Uy6DAkYZ69BVFFA1e+0nOoBF1EHwpllNwPbodzsrNeHsyHHUdpg==", "b644da55-51df-4c4a-991c-3c70063450b0" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 8, 3, 12, 53, 41, 464, DateTimeKind.Utc).AddTicks(2682), new DateTime(2026, 8, 3, 12, 53, 41, 464, DateTimeKind.Utc).AddTicks(2687) });
        }
    }
}

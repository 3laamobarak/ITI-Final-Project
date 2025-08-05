using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Company.Project.theDbcontext.Migrations
{
    /// <inheritdoc />
    public partial class Reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserId",
                table: "reviews",
                newName: "IX_reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_ProductId",
                table: "reviews",
                newName: "IX_reviews_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviews",
                table: "reviews",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "MaritalStatus", "NID", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "test-user-id", 0, "33171f36-2499-44f5-b303-b5b4fdf5b753", "testuser@example.com", true, "Bassel", "Male", "Ahmed", false, null, "Married", "sadsadaf", "TESTUSER@EXAMPLE.COM", "TESTUSER", "AQAAAAIAAYagAAAAEAqmgKkUeNEg5W6Uy6DAkYZ69BVFFA1e+0nOoBF1EHwpllNwPbodzsrNeHsyHHUdpg==", null, false, "b644da55-51df-4c4a-991c-3c70063450b0", false, "testuser" });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "afasf", false, "Default Brand", null });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "afasf", false, "Default Category", null });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedAt", "Description", "ExpiryDate", "IsDeleted", "Name", "Price", "StockQuantity", "UpdatedAt" },
                values: new object[] { 1, 1, 1, new DateTime(2025, 8, 3, 12, 53, 41, 464, DateTimeKind.Utc).AddTicks(2682), "Seeded product", new DateTime(2026, 8, 3, 12, 53, 41, 464, DateTimeKind.Utc).AddTicks(2687), false, "Sample Product", 49.99m, 0, null });

            migrationBuilder.InsertData(
                table: "reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "IsDeleted", "ProductId", "Rating", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Excellent product!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 4.5m, null, "test-user-id" },
                    { 2, "Not bad at all.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 3.8m, null, "test-user-id" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_AspNetUsers_UserId",
                table: "reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_Product_ProductId",
                table: "reviews",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_UserId",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_Product_ProductId",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reviews",
                table: "reviews");

            migrationBuilder.DeleteData(
                table: "reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "test-user-id");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "reviews",
                newName: "Review");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_UserId",
                table: "Review",
                newName: "IX_Review_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_ProductId",
                table: "Review",
                newName: "IX_Review_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

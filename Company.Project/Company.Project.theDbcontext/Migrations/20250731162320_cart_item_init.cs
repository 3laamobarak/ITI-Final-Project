using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Project.theDbcontext.Migrations
{
    /// <inheritdoc />
    public partial class cart_item_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refund_Order_OrderId1",
                table: "Refund");

            migrationBuilder.DropIndex(
                name: "IX_Refund_OrderId1",
                table: "Refund");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Refund");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Refund",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_OrderId",
                table: "Refund",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Order_OrderId",
                table: "Refund",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refund_Order_OrderId",
                table: "Refund");

            migrationBuilder.DropIndex(
                name: "IX_Refund_OrderId",
                table: "Refund");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "Refund",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "Refund",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Refund_OrderId1",
                table: "Refund",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Order_OrderId1",
                table: "Refund",
                column: "OrderId1",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

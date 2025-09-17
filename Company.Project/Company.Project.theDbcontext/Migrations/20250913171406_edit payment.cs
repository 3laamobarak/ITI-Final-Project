using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Project.theDbcontext.Migrations
{
    /// <inheritdoc />
    public partial class editpayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Refund",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RefundedAmount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Refund_PaymentId",
                table: "Refund",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Payments_PaymentId",
                table: "Refund",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refund_Payments_PaymentId",
                table: "Refund");

            migrationBuilder.DropIndex(
                name: "IX_Refund_PaymentId",
                table: "Refund");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Refund");

            migrationBuilder.DropColumn(
                name: "RefundedAmount",
                table: "Payments");
        }
    }
}

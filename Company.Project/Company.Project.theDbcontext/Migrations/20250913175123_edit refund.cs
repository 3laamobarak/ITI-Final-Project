using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Project.theDbcontext.Migrations
{
    /// <inheritdoc />
    public partial class editrefund : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refund_Payments_PaymentId",
                table: "Refund");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Refund",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Payments_PaymentId",
                table: "Refund",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refund_Payments_PaymentId",
                table: "Refund");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Refund",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Payments_PaymentId",
                table: "Refund",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }
    }
}

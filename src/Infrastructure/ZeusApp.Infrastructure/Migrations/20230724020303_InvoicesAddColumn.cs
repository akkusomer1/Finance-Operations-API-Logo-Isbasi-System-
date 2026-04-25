using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoicesAddColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductInvoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "ProductInvoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoices_ServiceId",
                table: "ProductInvoices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoices_Services_ServiceId",
                table: "ProductInvoices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoices_Services_ServiceId",
                table: "ProductInvoices");

            migrationBuilder.DropIndex(
                name: "IX_ProductInvoices_ServiceId",
                table: "ProductInvoices");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ProductInvoices");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

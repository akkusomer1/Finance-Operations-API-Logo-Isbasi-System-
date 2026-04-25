using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDeleteBehavior2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoices_Invoices_InvoiceId",
                table: "ProductInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoices_Products_ProductId",
                table: "ProductInvoices");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoices_Invoices_InvoiceId",
                table: "ProductInvoices",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoices_Products_ProductId",
                table: "ProductInvoices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoices_Invoices_InvoiceId",
                table: "ProductInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoices_Products_ProductId",
                table: "ProductInvoices");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoices_Invoices_InvoiceId",
                table: "ProductInvoices",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoices_Products_ProductId",
                table: "ProductInvoices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_CustomerSuppliers_CustomerSupplierId",
                table: "Banks");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_CustomerSuppliers_CustomerSupplierId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherAddresses_CustomerSuppliers_CustomerSupplierId",
                table: "OtherAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_CustomerSuppliers_CustomerSupplierId",
                table: "RelatedPersons");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_CustomerSuppliers_CustomerSupplierId",
                table: "Banks",
                column: "CustomerSupplierId",
                principalTable: "CustomerSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_CustomerSuppliers_CustomerSupplierId",
                table: "Contacts",
                column: "CustomerSupplierId",
                principalTable: "CustomerSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherAddresses_CustomerSuppliers_CustomerSupplierId",
                table: "OtherAddresses",
                column: "CustomerSupplierId",
                principalTable: "CustomerSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_CustomerSuppliers_CustomerSupplierId",
                table: "RelatedPersons",
                column: "CustomerSupplierId",
                principalTable: "CustomerSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_CustomerSuppliers_CustomerSupplierId",
                table: "Banks");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_CustomerSuppliers_CustomerSupplierId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherAddresses_CustomerSuppliers_CustomerSupplierId",
                table: "OtherAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_CustomerSuppliers_CustomerSupplierId",
                table: "RelatedPersons");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_CustomerSuppliers_CustomerSupplierId",
                table: "Banks",
                column: "CustomerSupplierId",
                principalTable: "CustomerSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_CustomerSuppliers_CustomerSupplierId",
                table: "Contacts",
                column: "CustomerSupplierId",
                principalTable: "CustomerSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherAddresses_CustomerSuppliers_CustomerSupplierId",
                table: "OtherAddresses",
                column: "CustomerSupplierId",
                principalTable: "CustomerSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_CustomerSuppliers_CustomerSupplierId",
                table: "RelatedPersons",
                column: "CustomerSupplierId",
                principalTable: "CustomerSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

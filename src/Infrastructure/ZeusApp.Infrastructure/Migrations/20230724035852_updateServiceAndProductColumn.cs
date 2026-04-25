using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateServiceAndProductColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseServices_Services_ServiceId",
                table: "ExpenseServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoices_Products_ProductId",
                table: "ProductInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoices_Services_ServiceId",
                table: "ProductInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_Products_ProductId",
                table: "ProductStocks");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ServiceCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductInvoices_ProductId",
                table: "ProductInvoices");

            migrationBuilder.DropIndex(
                name: "IX_ProductInvoices_ServiceId",
                table: "ProductInvoices");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductInvoices");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ProductInvoices");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductStocks",
                newName: "ProductServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStocks_ProductId",
                table: "ProductStocks",
                newName: "IX_ProductStocks_ProductServiceId");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "ExpenseServices",
                newName: "ProductServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseServices_ServiceId",
                table: "ExpenseServices",
                newName: "IX_ExpenseServices_ProductServiceId");

            migrationBuilder.AddColumn<int>(
                name: "ProductServiceId",
                table: "ProductInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductServiceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductServiceType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServiceCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VATRate = table.Column<int>(type: "int", nullable: false),
                    TotalStockAmount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    CurrencyType = table.Column<int>(type: "int", nullable: false),
                    SalesPriceIncludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    SalesUnitPriceExcludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PurchasePriceIncludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PurchasePriceExcludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackingType = table.Column<int>(type: "int", nullable: false),
                    ProductServiceCategoryId = table.Column<int>(type: "int", nullable: true),
                    ServiceGroupId = table.Column<int>(type: "int", nullable: true),
                    ProductBrandId = table.Column<int>(type: "int", nullable: true),
                    ProductServiceType = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductServices_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductServices_ProductServiceCategories_ProductServiceCategoryId",
                        column: x => x.ProductServiceCategoryId,
                        principalTable: "ProductServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductServices_ServiceGroup_ServiceGroupId",
                        column: x => x.ServiceGroupId,
                        principalTable: "ServiceGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductServices_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoices_ProductServiceId",
                table: "ProductInvoices",
                column: "ProductServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServices_ProductBrandId",
                table: "ProductServices",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServices_ProductServiceCategoryId",
                table: "ProductServices",
                column: "ProductServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServices_ServiceGroupId",
                table: "ProductServices",
                column: "ServiceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServices_UnitId",
                table: "ProductServices",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseServices_ProductServices_ProductServiceId",
                table: "ExpenseServices",
                column: "ProductServiceId",
                principalTable: "ProductServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoices_ProductServices_ProductServiceId",
                table: "ProductInvoices",
                column: "ProductServiceId",
                principalTable: "ProductServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_ProductServices_ProductServiceId",
                table: "ProductStocks",
                column: "ProductServiceId",
                principalTable: "ProductServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseServices_ProductServices_ProductServiceId",
                table: "ExpenseServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoices_ProductServices_ProductServiceId",
                table: "ProductInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_ProductServices_ProductServiceId",
                table: "ProductStocks");

            migrationBuilder.DropTable(
                name: "ProductServices");

            migrationBuilder.DropTable(
                name: "ProductServiceCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductInvoices_ProductServiceId",
                table: "ProductInvoices");

            migrationBuilder.DropColumn(
                name: "ProductServiceId",
                table: "ProductInvoices");

            migrationBuilder.RenameColumn(
                name: "ProductServiceId",
                table: "ProductStocks",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStocks_ProductServiceId",
                table: "ProductStocks",
                newName: "IX_ProductStocks_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductServiceId",
                table: "ExpenseServices",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseServices_ProductServiceId",
                table: "ExpenseServices",
                newName: "IX_ExpenseServices_ServiceId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductInvoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "ProductInvoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBrandId = table.Column<int>(type: "int", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrencyType = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasePriceExcludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PurchasePriceIncludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    SalesPriceIncludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    SalesUnitPriceExcludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalStockAmount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    TrackingType = table.Column<int>(type: "int", nullable: false),
                    VATRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceCategoryId = table.Column<int>(type: "int", nullable: true),
                    ServiceGroupId = table.Column<int>(type: "int", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrencyType = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasePriceExcludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PurchasePriceIncludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    SalesPriceIncludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    SalesUnitPriceExcludingVAT = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    VATRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_ServiceCategory_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_ServiceGroup_ServiceGroupId",
                        column: x => x.ServiceGroupId,
                        principalTable: "ServiceGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoices_ProductId",
                table: "ProductInvoices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoices_ServiceId",
                table: "ProductInvoices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBrandId",
                table: "Products",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceCategoryId",
                table: "Services",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceGroupId",
                table: "Services",
                column: "ServiceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UnitId",
                table: "Services",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseServices_Services_ServiceId",
                table: "ExpenseServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoices_Products_ProductId",
                table: "ProductInvoices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoices_Services_ServiceId",
                table: "ProductInvoices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_Products_ProductId",
                table: "ProductStocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

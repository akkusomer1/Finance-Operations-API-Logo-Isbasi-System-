using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyType = table.Column<int>(type: "int", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderCategoryId = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    TotalVATAmount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    OrderInvoiceType = table.Column<int>(type: "int", nullable: false),
                    InvoiceType = table.Column<int>(type: "int", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    CarrierCompanyId = table.Column<int>(type: "int", nullable: true),
                    CustomerSupplierId = table.Column<int>(type: "int", nullable: false),
                    HasPaid = table.Column<bool>(type: "bit", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    CustomerOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_CarrierCompanies_CarrierCompanyId",
                        column: x => x.CarrierCompanyId,
                        principalTable: "CarrierCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_CustomerSuppliers_CustomerSupplierId",
                        column: x => x.CustomerSupplierId,
                        principalTable: "CustomerSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderCategories_OrderCategoryId",
                        column: x => x.OrderCategoryId,
                        principalTable: "OrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAmount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    TotalSalesAmountForProduct = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    SupplyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippedQuantity = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PendingQuantity = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductServiceId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOrders_ProductServices_ProductServiceId",
                        column: x => x.ProductServiceId,
                        principalTable: "ProductServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CarrierCompanyId",
                table: "Orders",
                column: "CarrierCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerSupplierId",
                table: "Orders",
                column: "CustomerSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCategoryId",
                table: "Orders",
                column: "OrderCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_OrderId",
                table: "ProductOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductServiceId",
                table: "ProductOrders",
                column: "ProductServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_UnitId",
                table: "ProductOrders",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}

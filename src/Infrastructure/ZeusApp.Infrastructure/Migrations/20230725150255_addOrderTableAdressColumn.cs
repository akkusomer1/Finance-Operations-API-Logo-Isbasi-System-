using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addOrderTableAdressColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAddressDifferent",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OtherAddressId",
                table: "Orders",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAddressDifferent",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OtherAddressId",
                table: "Orders");
        }
    }
}

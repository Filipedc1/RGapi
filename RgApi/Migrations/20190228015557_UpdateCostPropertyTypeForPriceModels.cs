using Microsoft.EntityFrameworkCore.Migrations;

namespace RgApi.Migrations
{
    public partial class UpdateCostPropertyTypeForPriceModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "SalonPrices",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "CustomerPrices",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "SalonPrices",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "CustomerPrices",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}

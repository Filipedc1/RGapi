using Microsoft.EntityFrameworkCore.Migrations;

namespace RgApi.Migrations
{
    public partial class RemoveNamePropertyFromPriceModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SalonPrices");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CustomerPrices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SalonPrices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CustomerPrices",
                nullable: true);
        }
    }
}

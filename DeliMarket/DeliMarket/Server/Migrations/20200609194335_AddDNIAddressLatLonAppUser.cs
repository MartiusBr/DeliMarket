using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class AddDNIAddressLatLonAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DNI",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "1a3255d2-9d40-4fec-821a-e8c533634e1e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DNI",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "e6f69431-7f03-4983-b150-0e00a6ee2e98");
        }
    }
}

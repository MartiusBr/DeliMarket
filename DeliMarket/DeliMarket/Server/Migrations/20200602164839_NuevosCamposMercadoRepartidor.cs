using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class NuevosCamposMercadoRepartidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41");

            migrationBuilder.AddColumn<bool>(
                name: "Autorizado",
                table: "Repartidores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Autorizado",
                table: "Mercados",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Duenio",
                table: "Mercados",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autorizado",
                table: "Repartidores");

            migrationBuilder.DropColumn(
                name: "Autorizado",
                table: "Mercados");

            migrationBuilder.DropColumn(
                name: "Duenio",
                table: "Mercados");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89086180-b978-4f90-9dbd-a7040bc93f41", "53e15b81-8d0a-4f8a-a29b-b80debc621ad", "admin", "admin" });
        }
    }
}

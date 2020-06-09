using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class AddFldRepMerLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Repartidores");

            migrationBuilder.DropColumn(
                name: "Duenio",
                table: "ProductosMercados");

            migrationBuilder.DropColumn(
                name: "Duenio",
                table: "Mercados");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Repartidores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Propietario",
                table: "ProductosMercados",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Mercados",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NroSanidad",
                table: "Mercados",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroCel",
                table: "Mercados",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Propietario",
                table: "Mercados",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RUC",
                table: "Mercados",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Mercados",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Repartidores");

            migrationBuilder.DropColumn(
                name: "Propietario",
                table: "ProductosMercados");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Mercados");

            migrationBuilder.DropColumn(
                name: "NroSanidad",
                table: "Mercados");

            migrationBuilder.DropColumn(
                name: "NumeroCel",
                table: "Mercados");

            migrationBuilder.DropColumn(
                name: "Propietario",
                table: "Mercados");

            migrationBuilder.DropColumn(
                name: "RUC",
                table: "Mercados");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Mercados");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Repartidores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duenio",
                table: "ProductosMercados",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duenio",
                table: "Mercados",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

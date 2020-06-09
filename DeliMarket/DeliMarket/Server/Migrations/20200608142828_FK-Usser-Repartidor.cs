using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class FKUsserRepartidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Repartidores",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "17c2db24-48cb-4bbe-b9e7-d53fb71c50c5");

            migrationBuilder.CreateIndex(
                name: "IX_Repartidores_UserId",
                table: "Repartidores",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repartidores_AspNetUsers_UserId",
                table: "Repartidores",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repartidores_AspNetUsers_UserId",
                table: "Repartidores");

            migrationBuilder.DropIndex(
                name: "IX_Repartidores_UserId",
                table: "Repartidores");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Repartidores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "1937368c-8af9-427a-93c4-808e28ce700d");
        }
    }
}

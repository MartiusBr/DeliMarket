using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class AddFKUsersMercado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Mercados",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "e6f69431-7f03-4983-b150-0e00a6ee2e98");

            migrationBuilder.CreateIndex(
                name: "IX_Mercados_UserId",
                table: "Mercados",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mercados_AspNetUsers_UserId",
                table: "Mercados",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mercados_AspNetUsers_UserId",
                table: "Mercados");

            migrationBuilder.DropIndex(
                name: "IX_Mercados_UserId",
                table: "Mercados");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Mercados",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "17c2db24-48cb-4bbe-b9e7-d53fb71c50c5");
        }
    }
}

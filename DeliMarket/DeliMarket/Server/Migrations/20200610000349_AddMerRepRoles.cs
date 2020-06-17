using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class AddMerRepRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "fad3d1c4-5b9d-4496-92c2-2ab24c2d697a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fa66c0c6-f867-4623-942c-5ae2debbb902", "c27875ce-fb3d-4be5-bac4-d9e0b81968a2", "mercado", "mercado" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb39e7fb-0828-41db-8794-60c9db40171d", "87287dc4-fb9d-4502-b9b3-1db717a16c0e", "repartidor", "repartidor" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "1a3255d2-9d40-4fec-821a-e8c533634e1e");
        }
    }
}

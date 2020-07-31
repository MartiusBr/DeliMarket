using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class fersss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Propietario",
                table: "Mercados");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "3cbfcb6f-9016-47ae-bd24-95d54d029ea0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "9e78a7bc-f34a-41de-973e-6ba702daa6ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "43991878-d7bf-48e2-adf8-4a0e36adb515");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f05a8d3e-4fe9-45e6-a91d-9904118eada3",
                column: "ConcurrencyStamp",
                value: "00539557-53ad-487b-9650-d0c3daa1934f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "af9d4af2-7b2e-4eec-be20-b11190c9e80d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Propietario",
                table: "Mercados",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "57ea5e56-e9a0-4e18-8c83-cb9345cae4b9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "2f757676-c79b-4e2c-b552-dadb762a98be");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "43935eb9-32a2-4b90-8125-c44545e9119e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f05a8d3e-4fe9-45e6-a91d-9904118eada3",
                column: "ConcurrencyStamp",
                value: "40d7c985-8a01-4f40-a56b-cca19708317b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "c836cef3-096d-4ddc-bf45-d0bcf23577e7");
        }
    }
}

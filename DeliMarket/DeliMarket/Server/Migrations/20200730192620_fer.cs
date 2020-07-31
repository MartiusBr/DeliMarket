using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class fer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Ordenes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "bdbceed7-03fc-49dc-a0c7-ef072cb9a837");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "9080d26f-e5c0-4e95-a455-9030a9f3c6b6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "1e682dde-d694-47af-a17a-10ab284dc419");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f05a8d3e-4fe9-45e6-a91d-9904118eada3",
                column: "ConcurrencyStamp",
                value: "b9dcd97a-127b-4eea-a047-f787e549af4e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "d03f887e-78cb-4923-a2e8-507ee3cfa47f");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Ordenes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "5e70f909-14f6-4134-bf02-7fbeb6ac7139");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "e9281ba6-0235-45de-b18d-0b12ae8c62f3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "c9253fd5-d641-4df7-91da-7b3a53e057c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f05a8d3e-4fe9-45e6-a91d-9904118eada3",
                column: "ConcurrencyStamp",
                value: "68b9947f-6bc0-4e12-8a46-e05401f1ea65");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "3b8322e6-656d-4319-becf-32fcd023ac57");
        }
    }
}

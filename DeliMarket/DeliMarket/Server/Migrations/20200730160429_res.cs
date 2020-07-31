using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class res : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "Productos",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estado",
                table: "Productos");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "35f9772c-1593-4383-b6be-3d295c3c882f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "ef3651b4-ae71-43cb-813b-176b8b01e43d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "4258277e-fa93-4b95-8d93-dd664433ab67");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f05a8d3e-4fe9-45e6-a91d-9904118eada3",
                column: "ConcurrencyStamp",
                value: "d6709eb6-297d-44b9-bab7-f5e8211f8d3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "a91580b0-464e-4e00-bdbd-c2845bdacf21");
        }
    }
}

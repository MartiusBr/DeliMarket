using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class ferss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MetodoPago",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetodoPago",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "09f140dc-ebfc-49f7-85b8-960d066dd84a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "36a195b1-917d-429a-bacd-83b1eae03025");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "fdcf31d0-54a7-4928-be5d-ff92971449d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f05a8d3e-4fe9-45e6-a91d-9904118eada3",
                column: "ConcurrencyStamp",
                value: "332538b8-cb97-47a4-a29a-0d2bf6554752");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "456898f2-d604-4568-80b0-211894f9e5d6");
        }
    }
}

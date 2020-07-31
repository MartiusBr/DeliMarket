using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "704cc871-a078-49dd-a38e-a10913c7e89c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "7f16a221-255e-4b8a-9ce6-ee7d506c1a69");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "c6093f08-7ea8-4645-a633-6804d4feb55f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f05a8d3e-4fe9-45e6-a91d-9904118eada3",
                column: "ConcurrencyStamp",
                value: "00a3ab0b-8396-41b3-945a-5bfe78b5dd85");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "67d81312-b9a5-41fe-be5d-d50472d84401");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

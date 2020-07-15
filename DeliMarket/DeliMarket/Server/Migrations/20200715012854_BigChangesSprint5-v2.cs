using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class BigChangesSprint5v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Repartidores_RepartidorID",
                table: "Ordenes");

            migrationBuilder.AlterColumn<int>(
                name: "RepartidorID",
                table: "Ordenes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "b1305f6b-bb42-4742-afdf-eb82d057d395");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "27d65497-3f1b-40ce-8cc2-cba4e67205ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "a296bacf-8429-4c43-b981-e530819aff60");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "809cb2ac-299c-4a37-83ff-1476b5c46c0b");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Repartidores_RepartidorID",
                table: "Ordenes",
                column: "RepartidorID",
                principalTable: "Repartidores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Repartidores_RepartidorID",
                table: "Ordenes");

            migrationBuilder.AlterColumn<int>(
                name: "RepartidorID",
                table: "Ordenes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "35ceb6f6-1a92-4b1d-bf15-513ff790b941");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "ad2baeb7-be7d-4208-8cd3-02340563674b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "b680ad49-3dff-4208-88af-be2c653c8733");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "ed375e76-8e00-4d4a-ab12-28b008b0af22");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Repartidores_RepartidorID",
                table: "Ordenes",
                column: "RepartidorID",
                principalTable: "Repartidores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

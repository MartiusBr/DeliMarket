using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class AgregandoOrdenDetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Propietario",
                table: "ProductosMercados");

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    OrdenRapida = table.Column<bool>(nullable: false),
                    OrdenProgramada = table.Column<bool>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    RepartidorID = table.Column<int>(nullable: false),
                    Montototal = table.Column<double>(nullable: false),
                    Descuento = table.Column<double>(nullable: false),
                    Estado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenes_Repartidores_RepartidorID",
                        column: x => x.RepartidorID,
                        principalTable: "Repartidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordenes_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenID = table.Column<int>(nullable: false),
                    ProductomercadoProductoId = table.Column<int>(nullable: true),
                    ProductomercadoMercadoId = table.Column<int>(nullable: true),
                    Cantidad = table.Column<double>(nullable: false),
                    Precio = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detalles_Ordenes_OrdenID",
                        column: x => x.OrdenID,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detalles_ProductosMercados_ProductomercadoProductoId_ProductomercadoMercadoId",
                        columns: x => new { x.ProductomercadoProductoId, x.ProductomercadoMercadoId },
                        principalTable: "ProductosMercados",
                        principalColumns: new[] { "ProductoId", "MercadoId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "17497078-f838-4468-bd70-61eea42cf586");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "f3dd0c9c-7cba-4189-b0fa-2558dc4037da");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "0636d384-b8d3-4ded-be90-caf8f865a01a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "77e5f569-874e-4264-af60-b8a85de72772");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_OrdenID",
                table: "Detalles",
                column: "OrdenID");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_ProductomercadoProductoId_ProductomercadoMercadoId",
                table: "Detalles",
                columns: new[] { "ProductomercadoProductoId", "ProductomercadoMercadoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_RepartidorID",
                table: "Ordenes",
                column: "RepartidorID");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_UserID",
                table: "Ordenes",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.AddColumn<string>(
                name: "Propietario",
                table: "ProductosMercados",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14200405-23f2-43c9-b0ba-607fcf35e52a",
                column: "ConcurrencyStamp",
                value: "08c13a67-0e78-460a-928d-4389da821703");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "0ac6a596-a920-4317-9ca2-100a44820a5d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb39e7fb-0828-41db-8794-60c9db40171d",
                column: "ConcurrencyStamp",
                value: "383ccf10-d45c-4f18-a60e-9f48cc97ee98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa66c0c6-f867-4623-942c-5ae2debbb902",
                column: "ConcurrencyStamp",
                value: "6bf48c15-a9f7-4116-81c8-a21646445a16");
        }
    }
}

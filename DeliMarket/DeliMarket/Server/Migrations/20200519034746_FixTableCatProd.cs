using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliMarket.Server.Migrations
{
    public partial class FixTableCatProd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriasProducto_Categorias_CategoriaId",
                table: "CategoriasProducto");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriasProducto_Productos_ProductoId",
                table: "CategoriasProducto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriasProducto",
                table: "CategoriasProducto");

            migrationBuilder.RenameTable(
                name: "CategoriasProducto",
                newName: "CategoriasProductos");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriasProducto_ProductoId",
                table: "CategoriasProductos",
                newName: "IX_CategoriasProductos_ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriasProductos",
                table: "CategoriasProductos",
                columns: new[] { "CategoriaId", "ProductoId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "6665a186-d983-44f6-aa25-99c3a0878884");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriasProductos_Categorias_CategoriaId",
                table: "CategoriasProductos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriasProductos_Productos_ProductoId",
                table: "CategoriasProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriasProductos_Categorias_CategoriaId",
                table: "CategoriasProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriasProductos_Productos_ProductoId",
                table: "CategoriasProductos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriasProductos",
                table: "CategoriasProductos");

            migrationBuilder.RenameTable(
                name: "CategoriasProductos",
                newName: "CategoriasProducto");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriasProductos_ProductoId",
                table: "CategoriasProducto",
                newName: "IX_CategoriasProducto_ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriasProducto",
                table: "CategoriasProducto",
                columns: new[] { "CategoriaId", "ProductoId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89086180-b978-4f90-9dbd-a7040bc93f41",
                column: "ConcurrencyStamp",
                value: "8d650817-67c1-40c1-a07d-9ae13be0e363");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriasProducto_Categorias_CategoriaId",
                table: "CategoriasProducto",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriasProducto_Productos_ProductoId",
                table: "CategoriasProducto",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

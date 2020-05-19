using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class CategoriaProducto
    {
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public Producto Producto { get; set; }
    }
}

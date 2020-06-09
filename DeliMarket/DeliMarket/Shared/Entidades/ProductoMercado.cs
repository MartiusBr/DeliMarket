using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class ProductoMercado
    {
        public int MercadoId { get; set; }
        public int ProductoId { get; set; }
        public Mercado Mercado { get; set; }
        public Producto Producto { get; set; }
        public string Propietario { get; set; }
        public int Orden { get; set; }
    }
}

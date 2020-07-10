using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class Detalle
    {
        public int Id { get; set; }
        public int OrdenID { get; set; }
        public Orden Orden { get; set; }
        public ProductoMercado Productomercado { get; set; }
        public double Cantidad { get; set; }
        public double Precio { get; set; }

    }
}

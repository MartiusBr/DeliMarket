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
        public int ProductoId { get; set; }
        public int MercadoId { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double GetTotalPrice()
        {
            Precio = Productomercado.Precio * Cantidad;
            return Precio;     
        }
        public string GetFormattedTotalPrice()
        {
            return GetTotalPrice().ToString("0.00");
        }

    }
}

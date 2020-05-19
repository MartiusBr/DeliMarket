using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class VotoProducto
    {
        public int Id { get; set; }
        public int Voto { get; set; }
        public DateTime FechaVoto { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public string UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
   public class Orden
    {
        public int Id { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool OrdenRapida { get; set; }
        public bool OrdenProgramada { get; set; }
        public ApplicationUser User { get; set; }
        public string UserID { get; set; }
        public Repartidor Repartidor { get; set; }
        public int RepartidorID { get; set; }
        public List<Detalle> Detalles { get; set; }
        public double Montototal { get; set; }
        public double Descuento { get; set; }
        public string Estado { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        public int? RepartidorID { get; set; }
        public List<Detalle> Detalles { get; set; } = new List<Detalle>();
        public double Montototal { get; set; }
        public double Descuento { get; set; }
        public string DireccionEnvio { get; set; }
        public string Estado { get; set; }
        public double GetTotalPrice() 
        {
           Montototal = Detalles.Sum(d => d.GetTotalPrice());
           return Montototal;
        } 
        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
    }
}

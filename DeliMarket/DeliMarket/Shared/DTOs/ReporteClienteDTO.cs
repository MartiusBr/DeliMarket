using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class ReporteClienteDTO
    {
        public string NombreUsuario { get; set; }
        public int OrdenID { get; set; }
        public DateTime Fecha { get; set; }        
        public int FechaDia { get; set; }
        public int FechaMes { get; set; }
        public int FechaAno { get; set; }
        public string nombreproducto { get; set; }
        public string nombremercado { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public double total { get; set; }
    }
}

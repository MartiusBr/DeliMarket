using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class ReporteMercadoDTO
    {
        public string nombre { get; set; }
        public string producto { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public DateTime fecha { get; set; }
        public int fechaDia { get; set; }
        public int fechaMes { get; set; }
        public int fechaAno { get; set; }
        public double gananciatotal { get; set; }
        public int ventatotal { get; set; }
    }
}

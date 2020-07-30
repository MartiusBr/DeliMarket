using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class ProdMercado
    {
        [Required(ErrorMessage ="Ingresa un precio"),Range(0,99999,ErrorMessage = "Ingresa un valor positivo y menor a 99999")]
        public double precio { get; set; }
        [Required, Range(0, 99999, ErrorMessage = "Ingresa un valor positivo y menor a 99999")]
        public int stock { get; set; }
    }
}

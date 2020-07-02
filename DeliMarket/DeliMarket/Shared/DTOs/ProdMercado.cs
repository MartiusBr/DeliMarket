using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class ProdMercado
    {
        [Required]
        public double precio { get; set; }
        [Required]
        public int stock { get; set; }
    }
}

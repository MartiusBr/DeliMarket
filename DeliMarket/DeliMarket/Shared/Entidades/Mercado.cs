using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class Mercado
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        [Required]
        public DateTime? Fecha { get; set; }
        public List<ProductoMercado> ProductosMercado { get; set; }
        [NotMapped]
        public string Duenio { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Mercado p2)
            {
                return Id == p2.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

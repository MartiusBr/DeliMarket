using Microsoft.AspNetCore.Identity;
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
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string RUC { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string NumeroCel { get; set; }
        public string NroSanidad { get; set; }        
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public bool Autorizado { get; set; }
        [Required]
        public DateTime? Fecha { get; set; }
        public List<ProductoMercado> ProductosMercado { get; set; }
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

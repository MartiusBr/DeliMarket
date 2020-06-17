using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class Repartidor 
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string NumeroCel { get; set; }
        public string PlacaVehiculo { get; set; }
        public string Afiliacion { get; set; }
        public string Tipo { get; set; }
        public bool Autorizado { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class Repartidor 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string NumeroCel { get; set; }
        public string PlacaVehiculo { get; set; }
        public string Password { get; set; }
        public string Afiliacion { get; set; }
        public string Tipo { get; set; }

    }
}

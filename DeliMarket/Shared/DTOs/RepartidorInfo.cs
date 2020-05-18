using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class RepartidorInfo
    {

        [Required]
        [DisplayName]
        public string Nombre { get; set; }
        [Required]
        public string DNI { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string NumeroCel { get; set; }
        [Required]
        public string PlacaVehiculo { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

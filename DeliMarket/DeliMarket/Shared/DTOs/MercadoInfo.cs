using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class MercadoInfo
    {
        [Required(ErrorMessage = "Ingrese RUC del mercado")]
        public string RUC { get; set; }
        [Required(ErrorMessage = "Ingrese nombre del mercado")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese una dirección de correo")]
        [EmailAddress(ErrorMessage = "Por favor ingresa una dirección de correo valido")]
        public string Email { get; set; }
        public string NumeroCel { get; set; }
        [Required(ErrorMessage = "Ingrese el número de Sanidad del mercado")]
        public string NroSanidad { get; set; }
        [Required(ErrorMessage = "Ingrese el propietario del mercado")]        
        public string Password { get; set; }
    }
}

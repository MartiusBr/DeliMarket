using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class UserInfo
    {

        [Required]
        [EmailAddress(ErrorMessage ="Por favor ingresa una dirección de correo valido")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Por favor ingresa un numero de celular válido")]
        public string NumeroCel { get; set; }
        [Required(ErrorMessage = "Por favor ingresa tu {0}"), MaxLength(30)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor ingresa una contraseña"), MaxLength(30)]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class UserInfo
    {

        [Required(ErrorMessage = "Por favor ingresa un correo")]
        [EmailAddress(ErrorMessage = "Por favor ingresa una dirección de correo valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor ingresa un DNI Valido"), MaxLength(8,ErrorMessage = "8 dígitos"), MinLength(8, ErrorMessage = "8 dígitos")]
        public string Dni { get; set; }
        [Required(ErrorMessage = "Por favor ingresa un numero de celular válido"), MaxLength(9,ErrorMessage = "9 dígitos"),MinLength(9, ErrorMessage = "9 dígitos")]
        public string NumeroCel { get; set; }
        [Required(ErrorMessage = "Por favor ingresa tu {0}"), MaxLength(30,ErrorMessage ="No exceder los 30 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor ingresa una contraseña"), MaxLength(30)]
        public string Password { get; set; }

        public int MetodoPago { get; set;  }
    }
}

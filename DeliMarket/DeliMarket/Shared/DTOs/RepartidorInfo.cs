using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class RepartidorInfo
    {

        [Required(ErrorMessage = "Por favor ingrese un nombre"),MaxLength(30)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Por favor ingrese su DNI o Carnet de Extranjería"),MaxLength(8)]
        public string DNI { get; set; }
        [Required(ErrorMessage = "Por favor ingrese su Email")]
        [EmailAddress(ErrorMessage = "Por favor ingrese una dirección de correo válida")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor ingresa un numero de celular válido"), MaxLength(9, ErrorMessage = "9 dígitos"), MinLength(9, ErrorMessage = "9 dígitos")]
        public string NumeroCel { get; set; }
        [Required(ErrorMessage = "Por favor ingrese la placa del vehículo")]
        public string PlacaVehiculo { get; set; }
        [Required(ErrorMessage = "Por favor ingrese una contraseña"), MaxLength(30)]
        public string Password { get; set; }
    }
}

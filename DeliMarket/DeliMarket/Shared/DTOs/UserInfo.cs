﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class UserInfo
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Porfavor ingresa tu {0}"), MaxLength(30)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Porfavor ingresa una {0}"), MaxLength(30)]
        public string Password { get; set; }
    }
}

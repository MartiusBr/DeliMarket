using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class UsuarioDTO
    {
        public string UserId { get; set; }
        public string Nombre { get; set; }
        public List<string> Roles { get; set; }
        public string Email { get; set; }
    }
}

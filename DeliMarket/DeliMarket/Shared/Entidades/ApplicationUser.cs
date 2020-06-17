using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class ApplicationUser : IdentityUser
    {
        public string DNI { get; set; }
        public string AddressName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}

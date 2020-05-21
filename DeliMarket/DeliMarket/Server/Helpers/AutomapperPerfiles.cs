using AutoMapper;
using DeliMarket.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliMarket.Server.Helpers
{
    public class AutomapperPerfiles: Profile
    {
        public AutomapperPerfiles()
        {
            CreateMap<Mercado, Mercado>()
                .ForMember(x => x.Foto, option => option.Ignore());

            CreateMap<Producto, Producto>()
                .ForMember(x => x.Poster, option => option.Ignore());
        }
    }
}

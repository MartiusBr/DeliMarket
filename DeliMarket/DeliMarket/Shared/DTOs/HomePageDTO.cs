using DeliMarket.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class HomePageDTO
    {
        public List<Producto> ProductosEnvioRapido { get; set; }
        public List<Producto> ProductosEnvioProg { get; set; }
    }
}

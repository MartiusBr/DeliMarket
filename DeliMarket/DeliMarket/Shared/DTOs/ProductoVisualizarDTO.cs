using DeliMarket.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class ProductoVisualizarDTO
    {
        public Producto Producto { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Mercado> Mercados { get; set; }
        public int VotoUsuario { get; set; }
        public double PromedioVotos { get; set; }
    }
}

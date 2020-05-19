using DeliMarket.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.DTOs
{
    public class ProductoActualizacionDTO
    {
        public Producto Producto { get; set; }
        public List<Persona> Actores { get; set; }
        public List<Categoria> CategoriasSeleccionados { get; set; }
        public List<Categoria> CategoriasNoSeleccionados { get; set; }
    }
}

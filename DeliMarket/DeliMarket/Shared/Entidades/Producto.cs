using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public bool EntregaRapida { get; set; }
        public bool EntregaProgramada { get; set; }
        [Required]
        public DateTime? Lanzamiento { get; set; }
        public string Poster { get; set; }
        public List<CategoriaProducto> CategoriasProducto { get; set; } = new List<CategoriaProducto>();
        //public List<MarcaProducto> MarcasProducto { get; set; } = new List<MarcaProducto>();
        public List<ProductoMercado> ProductosMercado { get; set; }
        public string TituloCortado
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Titulo))
                {
                    return null;
                }

                if (Titulo.Length > 60)
                {
                    return Titulo.Substring(0, 60) + "...";
                }
                else
                {
                    return Titulo;
                }
            }
        }
    }
}

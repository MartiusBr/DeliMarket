using DeliMarket.Shared.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliMarket.Server
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaProducto>().HasKey(x => new { x.CategoriaId, x.ProductoId });
            modelBuilder.Entity<ProductoMercado>().HasKey(x => new { x.ProductoId, x.MercadoId });

            var roleAdmin = new IdentityRole() 
            { Id = "89086180-b978-4f90-9dbd-a7040bc93f41", Name = "admin", NormalizedName = "admin" };

            modelBuilder.Entity<IdentityRole>().HasData(roleAdmin);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CategoriaProducto> CategoriasProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Mercado> Mercados { get; set; }
        public DbSet<ProductoMercado> ProductosMercados { get; set; }
        public DbSet<VotoProducto> VotosProductos { get; set; }
        public DbSet<Repartidor> Repartidores { get; set; }

        //Personalizando Tabla Usuarios(Agregando campos)
        //public class Usuario : IdentityUser
        //{
        //  [Required (ErrorMessage = "El campo {0} es requerido")]
        //  public string DNI { get; set; }    

        //}
    }
}

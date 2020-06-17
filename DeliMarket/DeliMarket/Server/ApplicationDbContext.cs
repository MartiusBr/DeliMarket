﻿using DeliMarket.Shared.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliMarket.Server
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
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
            var roleRepartidor = new IdentityRole()
            { Id = "eb39e7fb-0828-41db-8794-60c9db40171d", Name = "repartidor", NormalizedName = "repartidor" };
            var roleMercado = new IdentityRole()
            { Id = "fa66c0c6-f867-4623-942c-5ae2debbb902", Name = "mercado", NormalizedName = "mercado" };
            modelBuilder.Entity<IdentityRole>().HasData(roleAdmin);
            modelBuilder.Entity<IdentityRole>().HasData(roleRepartidor);
            modelBuilder.Entity<IdentityRole>().HasData(roleMercado);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CategoriaProducto> CategoriasProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Mercado> Mercados { get; set; }
        public DbSet<ProductoMercado> ProductosMercados { get; set; }
        public DbSet<VotoProducto> VotosProductos { get; set; }
        public DbSet<Repartidor> Repartidores { get; set; }


    }
}

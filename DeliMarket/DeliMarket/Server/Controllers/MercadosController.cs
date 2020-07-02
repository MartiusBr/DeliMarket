using AutoMapper;
using DeliMarket.Server.Helpers;
using DeliMarket.Shared.DTOs;
using DeliMarket.Shared.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("Dashboard/api/[controller]")]

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class MercadosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAlmacenadorDeArchivos almacenadorDeArchivos;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public MercadosController(ApplicationDbContext context, 
            IAlmacenadorDeArchivos almacenadorDeArchivos,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.almacenadorDeArchivos = almacenadorDeArchivos;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Mercado>>> Get([FromQuery] Paginacion paginacion)
        {
            var queryable = context.Mercados.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);
            return await queryable.Paginar(paginacion).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mercado>> Get(int id)
        {
            var mercado = await context.Mercados.FirstOrDefaultAsync(x => x.Id == id);

            if (mercado == null) { return NotFound(); }

            return mercado;
        }

        [HttpGet("buscar/{textoBusqueda}")]
        public async Task<ActionResult<List<Mercado>>> Get(string textoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(textoBusqueda)) { return new List<Mercado>(); }
            textoBusqueda = textoBusqueda.ToLower();
            return await context.Mercados
                .Where(x => x.Nombre.ToLower().Contains(textoBusqueda)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Mercado mercado)
        {
            if (!string.IsNullOrWhiteSpace(mercado.Foto))
            {
                var fotoMercado = Convert.FromBase64String(mercado.Foto);
                mercado.Foto = await almacenadorDeArchivos.GuardarArchivo(fotoMercado, "jpg", "mercados");
            }

            context.Add(mercado);
            await context.SaveChangesAsync();
            return mercado.Id;
        }

        [HttpPut("validar")]
        public async Task<ActionResult> ValidarMercado(Mercado mercado)
        {
            var mercadoDB = await context.Mercados.FirstOrDefaultAsync(x => x.Id == mercado.Id);
            
            if (mercadoDB == null) { return NotFound(); }

            mercadoDB = mapper.Map(mercado, mercadoDB);

            mercadoDB.Autorizado = true;

            //var usuario = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var usuario = await userManager.FindByEmailAsync(mercado.Email);

            await userManager.AddToRoleAsync(usuario, "mercado");

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Mercado mercado)
        {
            var mercadoDB = await context.Mercados.FirstOrDefaultAsync(x => x.Id == mercado.Id);

            if (mercadoDB == null) { return NotFound(); }

            mercadoDB = mapper.Map(mercado, mercadoDB);

            if (!string.IsNullOrWhiteSpace(mercado.Foto))
            {
                var fotoImagen = Convert.FromBase64String(mercado.Foto);
                mercadoDB.Foto = await almacenadorDeArchivos.EditarArchivo(fotoImagen,
                    "jpg", "mercados", mercadoDB.Foto);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Mercados.AnyAsync(x => x.Id == id);
            if (!existe) { return NotFound(); }
            var mercado = await context.Mercados.FirstAsync(x => x.Id == id);
            //context.Remove(new Mercado { Id = id });
            context.Remove(mercado);
            var usuarioMercado = await userManager.FindByEmailAsync(mercado.Email);
            var result = await userManager.DeleteAsync(usuarioMercado);
            if (result.Succeeded)
            {
                await context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

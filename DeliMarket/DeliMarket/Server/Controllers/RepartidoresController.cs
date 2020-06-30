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
using System.Net;
using System.Threading.Tasks;

namespace DeliMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("Dashboard/api/[controller]")]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]

    public class RepartidoresController : ControllerBase
    {
        private readonly ApplicationDbContext context; //Declaramos el context (Para acceder a la base de datos)
        private readonly IMapper mapper; //Declaramos mapper para poder Actualizar Un producto
        private readonly IAlmacenadorDeArchivos almacenadorDeArchivos;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RepartidoresController(ApplicationDbContext context,
           IMapper mapper, 
           UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           IAlmacenadorDeArchivos almacenadorDeArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorDeArchivos = almacenadorDeArchivos;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Repartidor>>> Get([FromQuery] Paginacion paginacion)
        {
            var queryable = context.Repartidores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);
            return await queryable.Paginar(paginacion).ToListAsync();
        }

        [HttpPut("validar")]
        public async Task<ActionResult> ValidarMercado(Repartidor repartidor)
        {
            var repartidorDB = await context.Repartidores.FirstOrDefaultAsync(x => x.Id == repartidor.Id);

            if (repartidorDB == null) { return NotFound(); }

            repartidorDB = mapper.Map(repartidor, repartidorDB);

            repartidorDB.Autorizado = true;

            var usuario = await userManager.FindByEmailAsync(repartidor.Email);

            await userManager.AddToRoleAsync(usuario, "repartidor");

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Repartidores.AnyAsync(x => x.Id == id);
            if (!existe) { return NotFound(); }
            context.Remove(new Repartidor { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}

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
using System.Security.Claims;
using System.Threading.Tasks;


namespace DeliMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("Dashboard/api/[controller]")]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsuariosController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> Get([FromQuery] Paginacion paginacion)
        {
            var queryable = context.Users.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);
            return await queryable.Paginar(paginacion)
                .Select(x => new UsuarioDTO { Email = x.Email, UserId = x.Id }).ToListAsync();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RolDTO>>> Get()
        {
            var ListaRoles = await context.Roles
                .Select(x => new RolDTO { Nombre = x.Name, RoleId = x.Id }).ToListAsync();
            return ListaRoles;
        }

        [HttpPost("asignarRol")]
        public async Task<ActionResult> AsignarRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(editarRolDTO.UserId); //Buscamos al usuario por el Id Proporcionado en el model
            if (await userManager.IsInRoleAsync(usuario, editarRolDTO.RoleId)) //Si el usuario ya posee el rol no lo sobreescribiremos y le mostramos el mensaje
            {
                var rolsito = await roleManager.FindByNameAsync(editarRolDTO.RoleId);
                Console.WriteLine($"El usuario ya posee el rol de {rolsito.NormalizedName}");
                return NoContent();
            }
            await userManager.AddToRoleAsync(usuario, editarRolDTO.RoleId);
            return NoContent();
        }

        [HttpPost("removerRol")]
        public async Task<ActionResult> RemoverUsuarioRol(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(editarRolDTO.UserId);
            await userManager.RemoveFromRoleAsync(usuario, editarRolDTO.RoleId);
            return NoContent();
        }

        [HttpPut("AgregarUbicacion")]
        public async Task<ActionResult> ActualizarUbicacionUsuario(LatLong ubicacionInfo)
        {
            //var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var UserClaims = HttpContext.User;

            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            user.AddressName = ubicacionInfo.AddressName;
            user.Latitude = ubicacionInfo.Latitude;
            user.Longitude = ubicacionInfo.Longitude;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }
    }
}

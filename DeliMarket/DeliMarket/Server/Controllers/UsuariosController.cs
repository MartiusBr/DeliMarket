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
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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
            List<UsuarioDTO> usuarios = new List<UsuarioDTO>();

            var usersDB = await queryable.Paginar(paginacion).ToListAsync();

            foreach(var us in usersDB)
            {
                var roles = await userManager.GetRolesAsync(us);
                UsuarioDTO usuarioDTO = new UsuarioDTO { 
                    Email = us.Email,
                    Nombre = us.NormalizedUserName,
                    UserId = us.Id,
                    Roles = roles.ToList()
                };
                usuarios.Add(usuarioDTO);
            }

            return usuarios;

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
                var usuario = userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                var mercado = await context.Mercados.FirstOrDefaultAsync(x => x.UserId == usuario.Result.Id);
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }

        [HttpGet("listaproductospersonalizada/{entregarapida}/{entregaprogramada}")]
        public async Task<ActionResult<HomePageDTO>> Get(bool entregarapida, bool entregaprogramada)
        {
            //usuario
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);

            //coordenadas del usuario
            double latitudusario = user.Latitude;
            double longitudsario = user.Longitude;

            //lista de productos inicial
            List<Producto> productosgenerales = new List<Producto>();

            //valores de la lista
            var queryablemer = context.Mercados.AsQueryable();
            var mercadoDB = await queryablemer.ToListAsync(); //Todos los mercados

            var queryablepromer = context.ProductosMercados.AsQueryable();
            var productomercadoDB = await queryablepromer.ToListAsync(); //Todos los productos de los mercados

            //metodo de seleccion
            foreach (var mer in mercadoDB)
            {
                var usuario = await userManager.FindByIdAsync(mer.UserId);
                double latitudmer = usuario.Latitude;
                double longitudmer = usuario.Longitude;
                //double latitudmer = mer.User.Latitude;
                //double longitudmer = mer.User.Longitude;

                double posicionx = Math.Pow(latitudmer - latitudusario, 2);
                double posiciony = Math.Pow(longitudmer - longitudsario, 2);

                double distancia = Math.Sqrt(posicionx + posiciony);

                if (distancia <= 50) //FIX!!!
                {
                    foreach (var promer in productomercadoDB)
                    {
                        if (mer.Id == promer.MercadoId)
                        {
                            productosgenerales.Add(promer.Producto);
                        }
                    }
                }
            }

            //lista especifica de productos segun la orden
            List<Producto> productosespecificos = new List<Producto>();

            //clacificacion por orden
            foreach (var pro in productosgenerales)
            {
                if (entregarapida == pro.EntregaRapida)
                {
                    productosespecificos.Add(pro);
                }
                else if (entregaprogramada == pro.EntregaProgramada)
                {
                    productosespecificos.Add(pro);
                }
            }

            var response = new HomePageDTO() //Inicializamos el modelo
            {
                ProductosEnvioRapido = productosespecificos, //Asignamos los productos de Envio Rapido al modelo
                ProductosEnvioProg = productosespecificos     //Asignamos los productos de Envio programado al modelo
            };
            return response;
        }
    }
}

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

        [HttpGet("getCliente")]
        public async Task<ActionResult<ApplicationUser>> GetCliente()
        {
            var userID = GetUserId();
            var usuario = await userManager.FindByIdAsync(userID);
            if (usuario == null)
            {
                return BadRequest("El cliente no existe");
            }
            return usuario;
        }

        [HttpGet("getMercado")]
        public async Task<ActionResult<Mercado>> GetMercado()
        {
            var userID = GetUserId();
            var mercado = await context.Mercados
                                .Include(m => m.User)
                                .FirstOrDefaultAsync(m => m.UserId.Equals(userID));
            if (mercado == null)
            {
                return BadRequest("El mercado no existe");
            }
            return mercado;
                                
        }

        [HttpGet("getRepartidor")]
        public async Task<ActionResult<Repartidor>> GetRepartidor()
        {
            var userID = GetUserId();
            var repartidor = await context.Repartidores
                                    .Include(r => r.User)
                                    .FirstOrDefaultAsync(m => m.UserId.Equals(userID));
            if (repartidor == null)
            {
                return BadRequest("El repartidor no existe");
            }
            return repartidor;
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RolDTO>>> Get()
        {
            var ListaRoles = await context.Roles
                .Select(x => new RolDTO { Nombre = x.Name, RoleId = x.Id }).ToListAsync();
            return ListaRoles;
        }

        //[HttpPost("asignarRol")]
        //public async Task<ActionResult> AsignarRolUsuario(EditarRolDTO editarRolDTO)
        //{
        //    var usuario = await userManager.FindByIdAsync(editarRolDTO.UserId); //Buscamos al usuario por el Id Proporcionado en el model
        //    if (await userManager.IsInRoleAsync(usuario, editarRolDTO.RoleId)) //Si el usuario ya posee el rol no lo sobreescribiremos y le mostramos el mensaje
        //    {
        //        var rolsito = await roleManager.FindByNameAsync(editarRolDTO.RoleId);
        //        Console.WriteLine($"El usuario ya posee el rol de {rolsito.NormalizedName}");
        //        return NoContent();
        //    }
        //    await userManager.AddToRoleAsync(usuario, editarRolDTO.RoleId);
        //    return NoContent();
        //}

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
            var roles = await userManager.GetRolesAsync(usuario);   //conseguimos todos los roles del usuario
            await userManager.RemoveFromRolesAsync(usuario, roles.ToArray()); //Removemos todos sus roles
            await userManager.AddToRoleAsync(usuario, editarRolDTO.RoleId); //Para asignarle un nuevo Rol
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

        private string GetUserId()
        {
            var UserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return UserID;
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
            List<ProductoMercado> productosgenerales = new List<ProductoMercado>();


            //valores de la lista
            var queryablemer = context.Mercados.AsQueryable();
            var mercadoDB = await queryablemer.ToListAsync();

            var queryablepromer = context.ProductosMercados.AsQueryable();
            var productomercadoDB = await queryablepromer.ToListAsync();

            var queryablepro = context.Productos.AsQueryable();
            var productoDB = await queryablepromer.ToListAsync();

            //metodo de seleccion
            foreach (var mer in mercadoDB)
            {
                var usuario = await userManager.FindByIdAsync(mer.UserId);
                double latitudmer = usuario.Latitude;
                double longitudmer = usuario.Longitude;
                //double latitudmer = mer.User.Latitude;
                //double longitudmer = mer.User.Longitude;


                double constante = Math.PI / 180;
                double radiotierra = 6371;

                double angulolatitud = (latitudusario - latitudmer) * constante / 2;
                double angulolongitud = (longitudsario - longitudmer) * constante / 2;
                //prueba de seno cuadrado
                //double sin2x = 0.5 - 0.5 * Math.Cos(angulolatitud*2);
                //double sin2y = 0.5 + 0.5 * Math.Cos(angulolatitud * 2);
                double cosu = Math.Cos(latitudusario * constante);
                double cosm = Math.Cos(latitudmer * constante);

                double sin2x = Math.Pow(angulolatitud, 2);
                double sin2y = Math.Pow(angulolongitud, 2);

                double distancia = Math.Sqrt(sin2x + cosu * cosm * sin2y);
                distancia = Math.Asin(distancia) * 2;
                distancia = distancia * radiotierra * 1000;

                if (distancia <= 700) //FIX!!!
                {
                    foreach (var promer in productomercadoDB)
                    {
                        if (mer.Id == promer.MercadoId)
                        {
                            var producto = await context.Productos.FirstOrDefaultAsync(x => x.Id == promer.ProductoId);
                            if (producto.estado == true)
                            {
                                if (promer.Stock > 0)
                                {
                                    /*int idpro = promer.ProductoId;
                                    var prod = await context.Productos.FirstAsync(x => x.Id == idpro);*/
                                    productosgenerales.Add(promer);

                                }
                            }
                        }
                    }
                }


            }

            //lista especifica de productos segun la orden
            List<Producto> productosespecificos = new List<Producto>();
            List<ProductoMercado> productosmercadosespecificos = new List<ProductoMercado>();
            //clacificacion por orden
            foreach (var pro in productosgenerales)
            {
                var prod = await context.Productos.FirstOrDefaultAsync(x => x.Id == pro.ProductoId);
                pro.Producto = prod;                
                if (Boolean.Equals(entregarapida, prod.EntregaRapida))
                {
                    productosespecificos.Add(prod);
                    productosmercadosespecificos.Add(pro);
                }
                else if (Boolean.Equals(entregaprogramada, prod.EntregaProgramada))
                {
                    productosespecificos.Add(prod);
                    productosmercadosespecificos.Add(pro);
                }
            }

            var response = new HomePageDTO() //Inicializamos el modelo
            {
                ProductosEnvioRapido = productosespecificos, //Asignamos los productos de Envio Rapido al modelo
                ProductosMercadoEnvioRapido = productosmercadosespecificos,
                ProductosEnvioProg = productosespecificos,     //Asignamos los productos de Envio programado al modelo
                ProductosMercadoEnvioProg = productosmercadosespecificos
            };
            return response;
        }
    }
}

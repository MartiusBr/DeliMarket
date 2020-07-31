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
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("Dashboard/api/[controller]")]

    public class OrdenesController : ControllerBase
    {
        private readonly ApplicationDbContext context; //Declaramos el context (Para acceder a la base de datos)
        private readonly IMapper mapper; //Declaramos mapper para poder Actualizar Un producto
        private readonly UserManager<ApplicationUser> userManager; //Declaramos el User Manager de tipo ApplicationUser(tabla ASPNETUsers)

        public OrdenesController(ApplicationDbContext context,    //Inicializamos los Servicios para poder usarlos en el controlador
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Orden>>> Get()
        {
            var ordenes = await context.Ordenes
                .Where(o => o.UserID == GetUserId())
                .Include(o => o.Repartidor)
                .Include(o => o.Detalles)
                //.Include(o => o.DeliveryLocation)
                //.Include(o => o.Pizzas).ThenInclude(p => p.Special)
                //.Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
                //.OrderByDescending(o => o.CreatedTime)
                .ToListAsync();

            //return orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
            return NoContent();
        }

        [HttpPost("realizarOrden")]
        public async Task<ActionResult<int>> RealizarOrden(Orden orden)
        {
            var idUsuario = GetUserId();
            var usuario = await userManager.FindByIdAsync(idUsuario);

            orden.OrdenRapida = true; //temporal
            orden.FechaCreacion = DateTime.Now;
            orden.UserID = idUsuario;
            orden.DireccionEnvio = usuario.AddressName;
            orden.Estado = 1;
            //orden.User = usuario

            foreach (var detalle in orden.Detalles)
            {
                detalle.ProductoId = detalle.Productomercado.ProductoId;
                detalle.MercadoId = detalle.Productomercado.MercadoId;
                var promer = await context.ProductosMercados.FirstOrDefaultAsync(x => x.ProductoId == detalle.ProductoId && x.MercadoId == detalle.MercadoId);
                promer.Stock = promer.Stock - detalle.Cantidad;
                detalle.Productomercado = null;
                orden.CantidadTotal += detalle.Cantidad;
            }

            context.Add(orden);

            try
            {
                await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw e;
            }

            return orden.Id;
        }

        private string GetUserId()
        {
            var UserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return UserID;
        }

        [AllowAnonymous]
        [HttpGet("ListaOrdenPersonalizada")]
        public async Task<ActionResult<List<Orden>>> ListaOrdenPersonalizada()// Devuelve las ordenes que tenga un usuario a su nombre
        {            
            List<Orden> ordenes = new List<Orden>();
            string usuarioID = GetUserId();
            var repartidor = await context.Repartidores.FirstOrDefaultAsync(x => x.UserId == usuarioID);
            var qordenes = context.Ordenes.AsQueryable();
            var ordenDB = await qordenes.ToListAsync();
            foreach (var ord in ordenDB)
            {
                if (repartidor == null)
                {
                    if (ord.UserID == usuarioID)
                    {
                        ordenes.Add(ord);
                    }
                }
                else if (ord.RepartidorID == repartidor.Id)
                {
                    ordenes.Add(ord);
                }     
            }        
            return ordenes;
        }


        [AllowAnonymous]
        [HttpGet("ListaOrdenDisponibles")]
        public async Task<ActionResult<List<Orden>>> ListaOrdenesDisponibles() //ordenes que esten disponibles para el repartidor
        {
            
            List<Orden> ordenes = new List<Orden>();
            
            var qordenes = context.Ordenes.AsQueryable();
            var ordenDB = await qordenes.ToListAsync();
            foreach (var ord in ordenDB)
            {
                if (ord.Estado==1)
                {
                    ordenes.Add(ord);
                }
            }
            
            return ordenes;
        }

        [AllowAnonymous]
        [HttpGet("ListaOrdenAceptadas")]
        public async Task<ActionResult<List<Orden>>> ListaOrdenesAcepatadas() //ordenes que esten disponibles para el repartidor
        {

            List<Orden> ordenes = new List<Orden>();

            var qordenes = context.Ordenes.AsQueryable();
            var ordenDB = await qordenes.ToListAsync();
            foreach (var ord in ordenDB)
            {
                if (ord.Estado != 1 )
                {
                    ordenes.Add(ord);
                }
            }

            return ordenes;
        }

        [AllowAnonymous]
        [HttpGet("ActualizarEstadoOrden/{estado}/{idorden}")]
        public async Task<int> ActualizarEstadoOrden(int estado, int idorden) { //ordenes que esten disponibles para el repartidor
            
            var usuarioid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var repartidor = await context.Repartidores.FirstOrDefaultAsync(x => x.UserId == usuarioid);

            var orden = await context.Ordenes.FirstOrDefaultAsync(x => x.Id == idorden);
            var detalles = await context.Detalles.Where(x => x.OrdenID == idorden).ToListAsync();
            int ars= 1;
            switch (estado)
            {
                case 2:
                    //Procesando
                    orden.Estado = 2;
                    orden.RepartidorID = repartidor.Id;
                    break;
                case 3:
                    //Enviando
                    orden.Estado = 3;
                    break;
                case 4:
                    //Completado
                    orden.Estado = 4;
                    break;
                case 5:
                    //Cacelado
                    if (orden.Estado < 3)
                    {
                        foreach (var det in detalles)
                        {
                            context.Detalles.Remove(det);
                        }
                        context.Ordenes.Remove(orden);
                    }
                    else
                    {
                        ars = 0;
                    }
                    break;


            }
            context.SaveChanges();
            return ars;
        }
            

    }
}
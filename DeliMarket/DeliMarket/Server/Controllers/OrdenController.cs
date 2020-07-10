using AutoMapper;
using DeliMarket.Server.Helpers;
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

    public class OrdenController : ControllerBase
    {
        private readonly ApplicationDbContext context; //Declaramos el context (Para acceder a la base de datos)
        private readonly IMapper mapper; //Declaramos mapper para poder Actualizar Un producto
        private readonly UserManager<ApplicationUser> userManager; //Declaramos el User Manager de tipo ApplicationUser(tabla ASPNETUsers)

        public OrdenController(ApplicationDbContext context,    //Inicializamos los Servicios para poder usarlos en el controlador
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

        //[HttpPost]
        //public async Task<ActionResult<int>> RealizarOrden(Orden orden) 
        //{
        //    orden.FechaCreacion = DateTime.Now;
        //    orden. = ;
        //    orden.UserID = GetUserId();

        //    // Enforce existence of Pizza.SpecialId and Topping.ToppingId
        //    // in the database - prevent the submitter from making up
        //    // new specials and toppings
        //    foreach (var pizza in orden.Pizzas)
        //    {
        //        pizza.SpecialId = pizza.Special.Id;
        //        pizza.Special = null;

        //        foreach (var topping in pizza.Toppings)
        //        {
        //            topping.ToppingId = topping.Topping.Id;
        //            topping.Topping = null;
        //        }
        //    }

        //    _db.Orders.Attach(orden);
        //    await _db.SaveChangesAsync();

        //    // In the background, send push notifications if possible
        //    var subscription = await _db.NotificationSubscriptions.Where(e => e.UserId == GetUserId()).SingleOrDefaultAsync();
        //    if (subscription != null)
        //    {
        //        _ = TrackAndSendNotificationsAsync(orden, subscription);
        //    }

        //    return orden.OrderId;
        //}

        private string GetUserId()
        {
            var UserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return UserID;
        }


    }
}
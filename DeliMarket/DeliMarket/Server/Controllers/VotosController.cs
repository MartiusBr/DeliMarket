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
using static DeliMarket.Server.ApplicationDbContext;

namespace DeliMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public VotosController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Votar(VotoProducto votoProducto)
        {
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;
            var votoActual = await context.VotosProductos
                .FirstOrDefaultAsync(x => x.ProductoId == votoProducto.ProductoId && x.UserId == userId);

            if (votoActual == null)
            {
                votoProducto.UserId = userId;
                votoProducto.FechaVoto = DateTime.Today;
                context.Add(votoProducto);
                await context.SaveChangesAsync();
            }
            else
            {
                votoActual.Voto = votoProducto.Voto;
                await context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}

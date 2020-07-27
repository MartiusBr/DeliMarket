using AutoMapper;
using DeliMarket.Shared.DTOs;
using DeliMarket.Shared.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class ReportesController : ControllerBase
    {
        private readonly ApplicationDbContext context; //Declaramos el context (Para acceder a la base de datos)
        private readonly IMapper mapper; //Declaramos mapper para poder Actualizar Un producto
        private readonly UserManager<ApplicationUser> userManager; //Declaramos el User Manager de tipo ApplicationUser(tabla ASPNETUsers)

        public ReportesController(ApplicationDbContext context,    //Inicializamos los Servicios para poder usarlos en el controlador
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet("ReporteCliente")]
        public async Task<ActionResult<List<ReporteClienteDTO>>> ClientexProductoxTiempo()
        {
            var usuarioid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


            //join
            //Lista para cliente
            var consulta = await context.Users.Where(x => x.Id == usuarioid)
                .Join(
                context.Ordenes.Where(x => x.Estado == "completado"),
                user => user.Id,
                order => order.UserID,
                (user, order) => new
                {
                    name = user.UserName,
                    orderid = order.Id,
                    date = order.FechaCreacion,
                    total = order.Montototal,
                }
                )//obetenemos todos los pedidos hechos x cada dia
                .Join(
                context.Detalles,
                user => user.orderid,
                detail => detail.OrdenID,
                (user, detail) => new
                {
                    user.name,
                    user.orderid,
                    user.date,
                    user.total,
                    productid = detail.ProductoId,
                    marketid = detail.MercadoId,
                    amount = detail.Cantidad,
                    price = detail.Precio,
                }
                )//otenemos todos los detalles con su precio y producto
                .Join(
                context.Mercados,
                user => user.marketid,
                market => market.Id,
                (user, market) => new
                {
                    user.name,
                    user.orderid,
                    user.date,
                    user.total,
                    user.productid,
                    marketname = market.Nombre,
                    user.amount,
                    user.price,
                }
                )//obtenemos el nombre del mercado 
                .Join(
                context.Productos,
                user => user.productid,
                product => product.Id,
                (user, product) => new
                {
                    user.name,
                    user.orderid,
                    user.date,
                    user.total,
                    productname = product.Titulo,
                    user.marketname,
                    user.amount,
                    user.price,
                }
                )//obtenemos el nombre del producto
                .ToListAsync();
            List<ReporteClienteDTO> listareporte = new List<ReporteClienteDTO>();

            //transformacion al DTO
            foreach (var report in consulta)
            {
                ReporteClienteDTO clientreport = new ReporteClienteDTO();
                clientreport.NombreUsuario = report.name;
                clientreport.OrdenID = report.orderid;
                clientreport.Fecha = (DateTime)report.date;
                clientreport.FechaDia = report.date.Value.Day;
                clientreport.FechaMes = report.date.Value.Month;
                clientreport.FechaAno = report.date.Value.Year;
                clientreport.nombremercado = report.marketname;
                clientreport.nombreproducto = report.productname;
                clientreport.cantidad = report.amount;
                clientreport.precio = report.price;
                clientreport.total = report.total;

                listareporte.Add(clientreport);
            }
            return listareporte;

        }

        [AllowAnonymous]
        [HttpGet("FiltroCliente/{ano}/{mes}")]
        public async Task<List<ReporteClienteDTO>> FiltroCliente(int ano, int mes)
        {
            DateTime actual = DateTime.Now;
            if (mes == 0)
            {
                mes = actual.Month;
            }
            if (ano == 0)
            {
                ano = actual.Year;
            }
            var repor = await ClientexProductoxTiempo();
            var listareporte = repor.Value;


            List<ReporteClienteDTO> reporte = new List<ReporteClienteDTO>();

            foreach (var rep in listareporte)
            {
                if (rep.FechaAno == ano)
                {
                    if (rep.FechaMes == mes)
                    {
                        reporte.Add(rep);
                    }
                }

            }


            return reporte;
        }




        [AllowAnonymous]
        [HttpGet("ReporteMercado")]
        public async Task<ActionResult<List<ReporteMercadoDTO>>> MercadoxProductoxTiempo()
        {
            var usuarioid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mercado= await context.Mercados.FirstOrDefaultAsync(x=> x.UserId==usuarioid);                        

            //join
            //Lista para mercado
            var consulta = await context.Ordenes.Where(x => x.Estado == "completado")
                .Join(
                context.Detalles,
                ord => ord.Id,
                det => det.OrdenID,
                (ord, det) => new
                {
                    orderid = ord.Id,
                    merid = det.MercadoId,
                    proid = det.ProductoId,
                    amount= det.Cantidad,
                    price= det.Precio,
                    date= ord.FechaCreacion

                }
                )//obetenemos todos los pedidos hechos x cada dia
                .Join(
                context.Mercados.Where(x=> x.Id== mercado.Id),
                ord => ord.merid,
                mer => mer.Id,
                (ord, mer) => new
                {
                    ord.orderid,
                    ord.proid,                    
                    ord.amount,
                    ord.price,                    
                    ord.date,
                    mername = mer.Nombre,                    
                }
                )//otenemos todos los detalles con su precio y producto                
                .Join(
                context.Productos,
                ord => ord.proid,
                product => product.Id,
                (ord, product) => new
                {
                    ord.orderid,
                    ord.mername,
                    ord.amount,
                    ord.price,
                    ord.date,
                    productname = product.Titulo,

                }
                )//obtenemos el nombre del producto
                .ToListAsync();
            List<ReporteMercadoDTO> listareporte = new List<ReporteMercadoDTO>();

            //transformacion al DTO
            foreach (var report in consulta)
            {
                ReporteMercadoDTO merchreport = new ReporteMercadoDTO();
                merchreport.nombre = report.mername;
                merchreport.producto = report.productname;
                merchreport.cantidad = report.amount;
                merchreport.precio = report.price;
                merchreport.fecha = (DateTime)report.date;
                merchreport.fechaDia = report.date.Value.Day;
                merchreport.fechaMes = report.date.Value.Month;
                merchreport.fechaAno = report.date.Value.Year;

                listareporte.Add(merchreport);
            }
            var bakaa = "bakka";
            return listareporte;

        }

        [AllowAnonymous]
        [HttpGet("FiltroMercado/{ano}/{mes}")]
        public async Task<List<ReporteMercadoDTO>> FiltroMercado(int ano, int mes)
        {
            DateTime actual = DateTime.Now;
            if (mes == 0)
            {
                mes = actual.Month;
            }
            if (ano == 0)
            {
                ano = actual.Year;
            }
            var repor = await MercadoxProductoxTiempo();
            var listareporte = repor.Value;

            
            List<ReporteMercadoDTO> reporte = new List<ReporteMercadoDTO>();
            List<string> producto = new List<string>();
            foreach (var rep in listareporte)
            {
                if (rep.fechaAno == ano)
                {
                    if (rep.fechaMes == mes)
                    {
                        if (reporte.Count != 0)
                        {
                            if (producto.Contains(rep.producto))
                            {
                                int id = producto.IndexOf(rep.producto);
                                reporte.ElementAt(id).ventatotal = reporte.ElementAt(id).ventatotal + rep.cantidad;
                                reporte.ElementAt(id).gananciatotal = reporte.ElementAt(id).gananciatotal + rep.precio;
                            }
                            else
                            {
                                ReporteMercadoDTO parcial = new ReporteMercadoDTO();
                                parcial.nombre = rep.nombre;
                                parcial.producto = rep.producto;
                                parcial.ventatotal = rep.cantidad;
                                parcial.gananciatotal = rep.precio;
                                producto.Add(rep.producto);
                                reporte.Add(parcial);
                            }

                        }
                        else
                        {
                            ReporteMercadoDTO parcial = new ReporteMercadoDTO();
                            parcial.nombre = rep.nombre;
                            parcial.producto = rep.producto;
                            parcial.ventatotal = rep.cantidad;
                            parcial.gananciatotal = rep.precio;
                            producto.Add(rep.producto);
                            reporte.Add(parcial);
                        }


                    }
                }

            }

            var bakaa = "bakka";
            return reporte;
        }


        [AllowAnonymous]
        [HttpGet("ReporteAdmin")]
        public async Task<ActionResult<List<ReporteMercadoDTO>>> AdminxProductoxTiempo()
        {
            var usuarioid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            //join
            //Lista para mercado
            var consulta = await context.Ordenes.Where(x => x.Estado == "completado")
                .Join(
                context.Detalles,
                ord => ord.Id,
                det => det.OrdenID,
                (ord, det) => new
                {
                    orderid = ord.Id,
                    merid = det.MercadoId,
                    proid = det.ProductoId,
                    amount = det.Cantidad,
                    price = det.Precio,
                    date = ord.FechaCreacion

                }
                )//obetenemos todos los pedidos hechos x cada dia
                .Join(
                context.Mercados,
                ord => ord.merid,
                mer => mer.Id,
                (ord, mer) => new
                {
                    ord.orderid,
                    ord.proid,
                    ord.amount,
                    ord.price,
                    ord.date,
                    mername = mer.Nombre,
                }
                )//otenemos todos los detalles con su precio y producto                
                .Join(
                context.Productos,
                ord => ord.proid,
                product => product.Id,
                (ord, product) => new
                {
                    ord.orderid,
                    ord.mername,
                    ord.amount,
                    ord.price,
                    ord.date,
                    productname = product.Titulo,

                }
                )//obtenemos el nombre del producto
                .ToListAsync();
            List<ReporteMercadoDTO> listareporte = new List<ReporteMercadoDTO>();

            //transformacion al DTO
            foreach (var report in consulta)
            {
                ReporteMercadoDTO merchreport = new ReporteMercadoDTO();
                merchreport.nombre = report.mername;
                merchreport.producto = report.productname;
                merchreport.cantidad = report.amount;
                merchreport.precio = report.price;
                merchreport.fecha = (DateTime)report.date;
                merchreport.fechaDia = report.date.Value.Day;
                merchreport.fechaMes = report.date.Value.Month;
                merchreport.fechaAno = report.date.Value.Year;

                listareporte.Add(merchreport);
            }
            var bakaa = "bakka";
            return listareporte;

        }

        [AllowAnonymous]
        [HttpGet("FiltroAdmin/{ano}/{mes}")]
        public async Task<List<ReporteMercadoDTO>> FiltroAdmin(int ano, int mes)
        {
            DateTime actual = DateTime.Now;
            if (mes == 0)
            {
                mes = actual.Month;
            }
            if (ano == 0)
            {
                ano = actual.Year;
            }
            var repor = await AdminxProductoxTiempo();
            var listareporte = repor.Value;

            
            List<ReporteMercadoDTO> reporte = new List<ReporteMercadoDTO>();
            
            var queryablemer = context.Mercados.AsQueryable();
            var mercadoDB = await queryablemer.ToListAsync();

            foreach (var mer in mercadoDB)
            {
                //rompe por cada mercado
                List<ReporteMercadoDTO> reportemercado = new List<ReporteMercadoDTO>();
                List<string> producto = new List<string>();
                foreach (var rep in listareporte)
                    {

                        if (rep.nombre.Equals(mer.Nombre))
                        {
                            if (rep.fechaAno == ano)
                            {
                                if (rep.fechaMes == mes)
                                {
                                    if (reportemercado.Count != 0)
                                    {

                                        if (producto.Contains(rep.producto))
                                        {
                                            int id = producto.IndexOf(rep.producto);
                                            reportemercado.ElementAt(id).ventatotal = reportemercado.ElementAt(id).ventatotal + rep.cantidad;
                                            reportemercado.ElementAt(id).gananciatotal = reportemercado.ElementAt(id).gananciatotal + rep.precio;
                                        }
                                        else
                                        {
                                            ReporteMercadoDTO parcial = new ReporteMercadoDTO();
                                            parcial.nombre = rep.nombre;
                                            parcial.producto = rep.producto;
                                            parcial.ventatotal = rep.cantidad;
                                            parcial.gananciatotal =  rep.precio;
                                            producto.Add(rep.producto);
                                            reportemercado.Add(parcial);
                                        }

                                    }
                                    else
                                    {
                                        ReporteMercadoDTO parcial = new ReporteMercadoDTO();
                                        parcial.nombre = rep.nombre;
                                        parcial.producto = rep.producto;
                                        parcial.ventatotal = rep.cantidad;
                                        parcial.gananciatotal =  rep.precio;
                                        producto.Add(rep.producto);
                                        reportemercado.Add(parcial);
                                    }


                                }
                            }
                        }
                    }

                string nommer = "";
                double gantotmer = 0;
                foreach (var y in reportemercado)
                {                    
                    nommer = y.nombre;
                    gantotmer = gantotmer + y.gananciatotal;
                }
                if(nommer!="" && gantotmer != 0)
                {
                    ReporteMercadoDTO parcial = new ReporteMercadoDTO();
                    parcial.nombre = nommer;
                    parcial.gananciatotal = gantotmer;
                    reporte.Add(parcial);
                }
            }
            var bakaa = "bakka";
            return reporte;
        }

     
        





    }   
}

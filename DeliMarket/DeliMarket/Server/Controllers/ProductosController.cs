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

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext context; //Declaramos el context (Para acceder a la base de datos)
        private readonly IAlmacenadorDeArchivos almacenadorDeArchivos; //Declaramos el Servicio de Almacenador de Archivos
        private readonly IMapper mapper; //Declaramos mapper para poder Actualizar Un producto
        private readonly UserManager<ApplicationUser> userManager; //Declaramos el User Manager de tipo ApplicationUser(tabla ASPNETUsers)

        public ProductosController(ApplicationDbContext context,    //Inicializamos los Servicios para poder usarlos en el controlador
            IAlmacenadorDeArchivos almacenadorDeArchivos,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.almacenadorDeArchivos = almacenadorDeArchivos;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<HomePageDTO>> Get() //Retornamos un DTO para el inicio(modelo)
        {
            var limite = 6; //Limite de productos a mostrar en productos de envío rapido y Programado

            var productosEnvioRapido = await context.Productos  //Fitramos los productos que tengan Entrega rápida y tomamos los 6 primeros productos
                .Where(x => x.EntregaRapida).Take(limite)
                .OrderByDescending(x => x.Lanzamiento)      //Y los ordenamos por su Lanzamiento
                .ToListAsync();         //Lo convertimos en Lista 

            var fechaActual = DateTime.Today;   //Conseguimos la fecha de hoy

            var productosEnvioProg = await context.Productos //Filtramos los productos que se lanzarán en el futuro
                .Where(x => x.Lanzamiento > fechaActual)
                .OrderBy(x => x.Lanzamiento).Take(limite) //Los ordenamos por Lamzamiento y tomamos los 6 primeros productos
                .ToListAsync();         //Lo convertimos en Lista

            var response = new HomePageDTO() //Inicializamos el modelo
            {
                ProductosEnvioRapido = productosEnvioRapido, //Asignamos los productos de Envio Rapido al modelo
                ProductosEnvioProg = productosEnvioProg     //Asignamos los productos de Envio programado al modelo
            };

            return response; //retornamos el modelo(DTO de Inicio)

        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductoVisualizarDTO>> Get(int id) //Devuelvo DTO del Producto(a mostrar) dando como parámetro el id del producto
        {
            var producto = await context.Productos.Where(x => x.Id == id)  //Busco el producto cuyo id sea igual al proporcionado en el parámetro
                .Include(x => x.CategoriasProducto).ThenInclude(x => x.Categoria)  //Incluimos la lista de Categorias que tiene el producto y tomamos las Categorias
                .Include(x => x.ProductosMercado).ThenInclude(x => x.Mercado) //Incluimos las lista de Ac que tiene el producto y tomamos los mercados
                .FirstOrDefaultAsync(); //Seleccionamos el primer registro que se filtro 

            if (producto == null) { return NotFound(); } //Si no encuentra el producto retornamos No Encontrado

            // todo: sistema de votacion

            var promedioVotos = 0.0; //Iniciamos el promedio de votos
            var votoUsuario = 0; //Iniciamos el voto del usuario

            if (await context.VotosProductos.AnyAsync(x => x.ProductoId == id)) //Si existen votos del producto
            {
                promedioVotos = await context.VotosProductos.Where(x => x.ProductoId == id)
                    .AverageAsync(x => x.Voto);  //Busca en Tabla VotosProductos todos los votos que tengan el id del producto y los promedia

                if (HttpContext.User.Identity.IsAuthenticated) //Si el usuario se encuentra Autenticado
                {
                    var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name); //Buscamos al usuario por su email, pasamos como parámetro el nombre del usuario(configurado como correo)
                    var userId = user.Id; // Conseguimos el Id del usuario tipo -> es2sda0321dsad232321

                    var votoUsuarioDB = await context.VotosProductos //Buscamos el voto del usuario pasando como parámetro el id del producto y el Id del usuario
                        .FirstOrDefaultAsync(x => x.ProductoId == id && x.UserId == userId); //Tomamos el primero que encontramos

                    if (votoUsuarioDB != null) //Si el usuario relizo un voto en el producto
                    {
                        votoUsuario = votoUsuarioDB.Voto; //asignamos el voto que realizó en el votoUsuario
                    }
                }
            }

            producto.ProductosMercado = producto.ProductosMercado.OrderBy(x => x.Orden).ToList();  //ordenamos los AC por orden 

            var model = new ProductoVisualizarDTO(); //inicializamos el DTO que retornaremos(modelo)
            model.Producto = producto; //Asignamos el producto al modelo
            model.Categorias = producto.CategoriasProducto.Select(x => x.Categoria).ToList(); //Filtramos en la tabla de categorias productos (del Producto) y tomamos la Categorias que tiene el producto
            model.Mercados = producto.ProductosMercado.Select(x => //Hacemos un Select a la Lista de ProductoMercado del Producto y 
            new Mercado                         //tomamos los campos que necesitamos y las asignamos a Mercado
            {
                Nombre = x.Mercado.Nombre,
                Foto = x.Mercado.Foto,
                Propietario = x.Propietario,
                Id = x.MercadoId
            }).ToList(); //La convertimos en Lista

            model.PromedioVotos = promedioVotos; //Asignamos el Promedio de Votos del Producto
            model.VotoUsuario = votoUsuario;    //Asignamos el voto del Usuario (del Producto)
            return model; //retornamos el modelo
        }

        [AllowAnonymous]
        [HttpGet("filtrar")]
        public async Task<ActionResult<List<Producto>>> Get([FromQuery] ParametrosBusquedaProductos parametrosBusqueda)
        {
            var productosQueryable = context.Productos.AsQueryable(); //Convierto Los Productos Como IQueryable, parecido a(IEnumerable). Nos permite hacer Querys

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Titulo)) //Si es ingreso un Titulo en el filtro
            {
                productosQueryable = productosQueryable
                    .Where(x => x.Titulo.ToLower().Contains(parametrosBusqueda.Titulo.ToLower())); //Filtro los productos que contengan el titulo que se ingreso en el filtro
            }

            if (parametrosBusqueda.EntregaRapida) //Si se hizo check en la entrega rapida
            {
                productosQueryable = productosQueryable.Where(x => x.EntregaRapida); //Filtro 
            }

            if (parametrosBusqueda.EntregaProgramada) //Si se hizo check en la entrega programada
            {
                var hoy = DateTime.Today; //Fecha Hoy
                productosQueryable = productosQueryable.Where(x => x.Lanzamiento >= hoy); //Filtro los productos donde su fecha lanzamiento sea mayor al día de hoy
            }

            if (parametrosBusqueda.CategoriaId != 0) //Para todos los productos que empiezen con el id 1,2,3...
            {
                productosQueryable = productosQueryable
                    .Where(x => x.CategoriasProducto.Select(y => y.CategoriaId) //Filtro los productos cuya categoria coinciden con las que se seleccionaron en el filtro
                    .Contains(parametrosBusqueda.CategoriaId));
            }

            // TODO: Implementar votacion

            await HttpContext.InsertarParametrosPaginacionEnRespuesta(productosQueryable, //Se inserta información de paginación en las respuestas http
                parametrosBusqueda.CantidadRegistros);

            var productos = await productosQueryable.Paginar(parametrosBusqueda.Paginacion).ToListAsync(); //Productos que se mostrarán en una página dada

            return productos;
        }

        public class ParametrosBusquedaProductos //Clase que contiene los parametros provenientes del filtro
        {
            public int Pagina { get; set; } = 1;  //Al inicio nos encontramos en la primera pagina por defecto
            public int CantidadRegistros { get; set; } = 10; //La cantidad de registros a mostrar (10) por pagina
            public Paginacion Paginacion
            {
                get { return new Paginacion() { Pagina = Pagina, CantidadRegistros = CantidadRegistros }; }
            }
            public string Titulo { get; set; } //Se busco por título
            public int CategoriaId { get; set; } //
            public bool EntregaRapida { get; set; } //Se dio check a la entrega rápida
            public bool EntregaProgramada { get; set; } //Se dio check a la entrega programada
            public bool MasVotadas { get; set; } //Se dio check a Mas Votadas(Productos)
        }

        [HttpGet("actualizar/{id}")] //Actualizar el producto dando su Id como parámetro
        public async Task<ActionResult<ProductoActualizacionDTO>> PutGet(int id) //Retorno su DTO
        {
            var productoActionResult = await Get(id); //consigo el producto dando como parámetro su id(Action Result)
            if (productoActionResult.Result is NotFoundResult) { return NotFound(); } // Si no se encuentra el producto retorno NoEncontrado

            var productoVisualizarDTO = productoActionResult.Value; //Consigo el vallor del ActionResult dando como resultado el producto
            var categoriasSeleccionadosIds = productoVisualizarDTO.Categorias.Select(x => x.Id).ToList(); //lista de Ids de las categorias Seleccionadas
            var categoriasNoSeleccionados = await context.Categorias
                .Where(x => !categoriasSeleccionadosIds.Contains(x.Id))
                .ToListAsync();

            var model = new ProductoActualizacionDTO();
            model.Producto = productoVisualizarDTO.Producto;
            model.CategoriasNoSeleccionados = categoriasNoSeleccionados;
            model.CategoriasSeleccionados = productoVisualizarDTO.Categorias; //Lista de las categorias seleccionadas
            model.Mercados = productoVisualizarDTO.Mercados;
            return model;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Producto producto) //Agregar un producto(POST)
        {
            if (!string.IsNullOrWhiteSpace(producto.Poster)) //Hay una imagen del producto(que viene del modelo)
            {

                var fotoProducto = Convert.FromBase64String(producto.Poster); //Lo convierto en array de Bits
                producto.Poster = await almacenadorDeArchivos.GuardarArchivo(fotoProducto, "jpg", "productos");//Y lo guadro en wwwroot del proyecto del servidor
            }

            if (producto.ProductosMercado != null) //Si hay PA
            {
                for (int i = 0; i < producto.ProductosMercado.Count; i++) //Para cada A en el P
                {
                    producto.ProductosMercado[i].Orden = i + 1; //Los ordeno desde 1,2,3...
                }
            }

            context.Add(producto); //Agrego el producto
            await context.SaveChangesAsync(); //Guardo cambios
            return producto.Id; //retorno el id del prod para redireccinar a la visualización del producto
        }

        [HttpPut]
        public async Task<ActionResult> Put(Producto producto) //Actualizar un producto
        {
            var productoDB = await context.Productos.FirstOrDefaultAsync(x => x.Id == producto.Id); //Consigo el producto por su Id(del producto que se paso como parámetro)

            if (productoDB == null) { return NotFound(); } //Si el producto no existe retorno NotFound()--400

            productoDB = mapper.Map(producto, productoDB); //mapeo el producto(que viene como parametro) al destino (productoDB)

            if (!string.IsNullOrWhiteSpace(producto.Poster)) //Si se agrego una imagen al producto(cambio imagen)
            {
                var posterImagen = Convert.FromBase64String(producto.Poster); //convierto la imagen en un Array de Bytes 
                productoDB.Poster = await almacenadorDeArchivos.EditarArchivo(posterImagen,
                    "jpg", "productos", productoDB.Poster);//Guardo la nueva imagen y retorno el enlace donde se encuentra(Poster)
            }

            await context.Database.ExecuteSqlInterpolatedAsync($"delete from CategoriasProductos WHERE ProductoId = {producto.Id}; delete from ProductosMercados where ProductoId = {producto.Id}"); //Elimino de la tabla CategoriasProductos y de PAct para poder guardar

            /////////// Primero ordenamos los ACT en el producto////////////////
            if (producto.ProductosMercado != null) //si el producto posee mercados que lo ofrecen/venden
            {
                for (int i = 0; i < producto.ProductosMercado.Count; i++) //Para cada Act en el Prod
                {
                    producto.ProductosMercado[i].Orden = i + 1; // Los ordeno desde 1,2,3..
                }
            }

            productoDB.ProductosMercado = producto.ProductosMercado; //Asignamos los Act ordenados
            productoDB.CategoriasProducto = producto.CategoriasProducto; //Asignamos las categorias al producto

            await context.SaveChangesAsync(); //Guardamos cambios
            return NoContent(); //No retorno ningun contenido

        }

        [AllowAnonymous]
        [HttpPost("seleccionar/{id}")] //Como mercado selecciono un producto y le agrego precio y stock
        public async Task<ActionResult> SeleccionarProductoMercado(int id, ProdMercado prodmercado)
        {
            var productoActionResult = await Get(id); //consigo el producto dando como parámetro su id(Action Result)
            if (productoActionResult.Result is NotFoundResult) { return NotFound(); } // Si no se encuentra el producto retorno NoEncontrado
            var producto = productoActionResult.Value; //Consigo el vallor del ActionResult dando como resultado el producto
            var mercado = await context.Mercados.FirstAsync(x => x.Email == HttpContext.User.Identity.Name);

            var existeProdMer = context.ProductosMercados.Any(x => x.MercadoId == mercado.Id && x.ProductoId == id);
            if (existeProdMer)  //Solo actualizo
            {
                var BDprodMer = await context.ProductosMercados.FirstAsync(x => x.MercadoId == mercado.Id);
                context.ProductosMercados.Attach(BDprodMer);
                BDprodMer.Precio = prodmercado.precio;
                BDprodMer.Stock = prodmercado.stock;

                await context.SaveChangesAsync();
                return NoContent();
            }
            else
            {   //Creo un nuevo prodMercado
                var productoMercado = new ProductoMercado
                {
                    Mercado = mercado,
                    MercadoId = mercado.Id,
                    Precio = prodmercado.precio,
                    Stock = prodmercado.stock,
                    ProductoId = producto.Producto.Id,
                    Producto = producto.Producto
                };
                context.Add(productoMercado);
                await context.SaveChangesAsync();
                return NoContent();
            }

        }

        [HttpDelete("{id}")] //Eliminar un producto por su id , que viene como parámetro
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Productos.AnyAsync(x => x.Id == id); // Verificamos si existe dicho producto
            if (!existe) { return NotFound(); }              //Si no existe retorno NotFound()
            context.Remove(new Producto { Id = id });       //Remuevo el producto cuyo id es el pasado en el parámetro
            await context.SaveChangesAsync();               //Guardo cambios asincronamente
            return NoContent();                             //No retorno ningun contenido
        }
    }
}

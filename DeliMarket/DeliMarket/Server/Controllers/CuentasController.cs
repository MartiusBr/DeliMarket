using DeliMarket.Shared.DTOs;
using DeliMarket.Shared.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeliMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public CuentasController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            this.context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("CrearCliente")] //Crear un usuario (Cliente por defecto)
        public async Task<ActionResult<UserToken>> CrearClient([FromBody] UserInfo model) //Crea un usuario y retorna un Token
        {
            var user = new IdentityUser { UserName = model.Nombre, Email = model.Email, PhoneNumber = model.NumeroCel }; //Almacenamos al Usuario(modelo que viene de parámetri) en una variable
            var result = await _userManager.CreateAsync(user, model.Password); //Creamos al usuario  
            var rolCliente = new List<string>(); //Creo una Lista vacia
            rolCliente.Add("cliente"); //Le asigno el rol de cliente por defecto
            if (result.Succeeded)   //Si el resultado de crearlo es satisfactorio
            {
                return BuildToken(model, rolCliente); //Le asigno un Token con la lista de Rol que tiene(Cliente por defecto)
            }
            else
            {
                var errores = result.Errors;
                return BadRequest(errores); //Si no se creo exitosamente al usuario retorno BadRequest
            }
        }

        [HttpPost("CrearRepartidor")] //Crear un repartidor 
        public async Task<ActionResult<UserToken>> CrearRepartidor([FromBody] RepartidorInfo model) //Crea un repartidor y retorna un Token
        {
            var user = new IdentityUser { UserName = model.Nombre, Email = model.Email, PhoneNumber = model.NumeroCel };
            var result = await _userManager.CreateAsync(user, model.Password);
            var rolRepartidor = new List<string>();
            rolRepartidor.Add("noauth"); //Por defecto al registrarse el repartidor se le asigna un rol anonimo hasta que se le asigne el rol de repartidor
            if (result.Succeeded)
            {
                var usuario = await _userManager.GetUserAsync(HttpContext.User);
                var repartidor = new Repartidor
                {
                    Nombre = model.Nombre,
                    User = usuario, //Se le asigna el usuario
                    DNI = model.DNI,
                    Email = model.Email,
                    NumeroCel = model.NumeroCel,
                    PlacaVehiculo = model.PlacaVehiculo,
                    Autorizado = false //Por defecto al registrarse el repartidor este tiene que ser validador por el 
                };
                var userInfo = new UserInfo { Email = model.Email, Nombre = model.Nombre, NumeroCel = model.NumeroCel };
                context.Add(repartidor);
                await context.SaveChangesAsync();
                return BuildToken(userInfo, rolRepartidor);
            }
            else
            {
                var errores = result.Errors.ToList();

                return BadRequest(errores);
                //return BadRequest("Username or password invalid"); 
            }

        }

        [HttpPost("CrearMercado")] //Crear un mercado 
        public async Task<ActionResult<UserToken>> CrearMercado([FromBody] MercadoInfo model) //Crea un repartidor y retorna un Token
        {
            var user = new IdentityUser { UserName = model.Nombre, Email = model.Email, PhoneNumber = model.NumeroCel };
            var result = await _userManager.CreateAsync(user, model.Password);
            var rolMercado = new List<string>();
            rolMercado.Add("noauth"); //Por defecto al registrarse el mercado se le asigna un rol anonimo hasta que se le asigne el rol de repartidor
            if (result.Succeeded)
            {
                var usuario = await _userManager.GetUserAsync(HttpContext.User);

                var mercado = new Mercado
                {
                    Email = model.Email,
                    User = usuario,
                    Nombre = model.Nombre,
                    RUC = model.RUC,
                    NumeroCel = model.NumeroCel,
                    NroSanidad = model.NroSanidad,
                    Fecha = DateTime.Now,
                    Propietario = model.Propietario,
                    Autorizado = false //Por defecto al registrarse el repartidor este tiene que ser validador por el 
                };
                var userInfo = new UserInfo { Email = model.Email, Nombre = model.Nombre, NumeroCel = model.NumeroCel };
                context.Add(mercado);
                await context.SaveChangesAsync();
                return BuildToken(userInfo, rolMercado);
            }
            else
            {
                var errores = result.Errors.ToList();
                return BadRequest(errores);
                //return BadRequest("Username or password invalid"); //Si no se creo exitosamente al usuario retorno BadRequest
            }

        }

        [HttpGet("RenovarToken")] //Action para Renovar el Token
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Para Usar este endpoint el Usuario debe esta Autenticado
        public async Task<ActionResult<UserToken>> Renovar()  //Retorno un User Token
        {
            var userInfo = new UserInfo()
            {
                Email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value
            };

            var usuario = await _userManager.FindByEmailAsync(userInfo.Email);
            var roles = await _userManager.GetRolesAsync(usuario);

            return BuildToken(userInfo, roles);
        }

        [HttpPost("Login")] //Accion para Login
        public async Task<ActionResult<UserToken>> Login([FromBody] UserLogin userLogin) 
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName,
                userLogin.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {

                var usuario = await _userManager.FindByEmailAsync(userLogin.Email); //Obtengo el cliente por su Email
                var roles = await _userManager.GetRolesAsync(usuario); //Obtengo sus roles

                UserInfo userInfo = new UserInfo { Email = userLogin.Email, Nombre = usuario.UserName};

                return BuildToken(userInfo, roles); //Construyo el token pasando como parametros el usuario(correo,nombre) y sus roles
            }
            else
            {
                return BadRequest("Invalid login attempt");
            }
        }

        private string GetUserId()
        {
            return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private UserToken BuildToken(UserInfo userInfo, IList<string> roles) //Metodo para Construir un Token teniendo como parámetro al usuario y la Lista de Roles que posee
        {
            var claims = new List<Claim>() //Creo una Lista de Claims (Informacion del usuario)
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email), //
                new Claim(ClaimTypes.Name, userInfo.Nombre), //Su Nombre
                new Claim("mivalor", "Lo que yo quiera"), //Cualquier llave - Valor
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //Id Unico del Token
            };

            foreach (var rol in roles)  //Para cada rol que tenga el usuario
            {
                claims.Add(new Claim(ClaimTypes.Role, rol)); //Asigno el Rol en la informacion de usuario(Claims)
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"])); //Codigo para Crear el Token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30); //Agrego una fecha de Expiración en este caso 30 minutos

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims, //Agrego la lista de Claims al token
               expires: expiration, //Agrego la fecha de expiracion al Token
               signingCredentials: creds);

            return new UserToken() //Retorno el Token y su fecha de expiracion
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}

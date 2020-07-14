using Blazor.FileReader;
using DeliMarket.Client.Auth;
using DeliMarket.Client.Helpers;
using DeliMarket.Client.Repositorios;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeliMarket.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddSingleton<ServicioSingleton>();
            services.AddSingleton<OrdenEstado>();
            services.AddTransient<ServicioTransient>();
            services.AddScoped<IRepositorio, Repositorio>(); //Cuando uso el Servicio IRepositorio, me retorna una instancia de Repositorio
            services.AddScoped<IMostrarMensajes, MostrarMensajes>();  //Cuando uso el Servicio IMostrarMensajes, me retorna una instancia de MostrarMensajes

            services.AddFileReaderService(options => options.InitializeOnFirstCall = true); //Configurando el servicio de lectura de archivos
            services.AddAuthorizationCore();    //Agregar Autorizacion

            services.AddScoped<ProveedorAutenticacionJWT>(); 

            services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacionJWT>(
                provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());
            services.AddScoped<ILoginService, ProveedorAutenticacionJWT>(
                provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());

            services.AddScoped<RenovadorToken>();
        }
    }
}

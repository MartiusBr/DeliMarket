using AutoMapper;
using DeliMarket.Server.Helpers;
using DeliMarket.Shared.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Text;

namespace DeliMarket.Server
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>() //Configuracion de Identity
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //Configuracion Autenticacion
                    .AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["jwt:key"])),
                     ClockSkew = TimeSpan.Zero
                 });

            services.AddAutoMapper(typeof(Startup));    //Configuracion AutoMapper

            services.AddScoped<IAlmacenadorDeArchivos, AlmacenadorArchivosLocal>(); //Cuando pido el servicio IAlmacenadorDeArchivos retorno una instancia de Almacenador de archivos Local
            //services.AddScoped<IAlmacenadorDeArchivos, AlmacenadorArchivosAzStorage>();
            services.AddHttpContextAccessor();//Para poder configurar el servicio de IHttpContextAccesor

            services.AddMvc().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/Dashboard"), first =>
            {
                first.UseBlazorFrameworkFiles("/Dashboard");
                first.UseStaticFiles();

                first.UseRouting();
                first.UseAuthentication();
                first.UseAuthorization();

                first.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    //endpoints.MapFallbackToFile("/Dashboard/{*path:nonfile}", "Dashboard/index.html");
                    endpoints.MapFallbackToFile("Dashboard/index.html");
                });
            });

            app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/Tienda"), sec =>
            {
                sec.UseBlazorFrameworkFiles("/Tienda");
                sec.UseStaticFiles();

                sec.UseRouting();
                sec.UseAuthentication();
                sec.UseAuthorization();

                sec.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    //endpoints.MapFallbackToFile("/Dashboard/{*path:nonfile}", "Dashboard/index.html");
                    endpoints.MapFallbackToFile("Tienda/index.html");
                });
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapDefaultControllerRoute
            });


            //app.UseStaticFiles();
            //app.UseBlazorFrameworkFiles();

            //app.UseRouting();
            //app.UseAuthentication(); //Agregando Autenticación en el pipeline
            //app.UseAuthorization();  //Agregando Autorización " "

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //    endpoints.MapFallbackToFile("index.html");
            //});
        }
    }
}

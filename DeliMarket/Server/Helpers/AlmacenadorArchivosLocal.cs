using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeliMarket.Server.Helpers
{
    public class AlmacenadorArchivosLocal : IAlmacenadorDeArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, //Para poder obtener el url donde se encuentra nuestra carpeta wwwroot
            IHttpContextAccessor httpContextAccessor
            ) 
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string nombreContenedor, string rutaArchivoActual)
        {
            if (!string.IsNullOrEmpty(rutaArchivoActual)) //Si existe el la imagen
            {
                await EliminarArchivo(rutaArchivoActual, nombreContenedor); //Lo elimino
            }

            return await GuardarArchivo(contenido, extension, nombreContenedor); //Y guardo la nueva imagen
        }

        public Task EliminarArchivo(string ruta, string nombreContenedor)
        {
            var filename = Path.GetFileName(ruta);
            string directorioArchivo = Path.Combine(env.WebRootPath, nombreContenedor, filename);
            if (File.Exists(directorioArchivo))
            {
                File.Delete(directorioArchivo);
            }
            return Task.FromResult(0);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string nombreContenedor)
        {
            var filename = $"{Guid.NewGuid()}.{extension}";
            string folder = Path.Combine(env.WebRootPath, nombreContenedor);

            if (!Directory.Exists(folder)) //Si no existe el directorio lo creamos
            {
                Directory.CreateDirectory(folder);
            }

            string rutaGuardado = Path.Combine(folder, filename);
            await File.WriteAllBytesAsync(rutaGuardado, contenido);

            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var rutaParaBD = Path.Combine(urlActual, nombreContenedor, filename);
            return rutaParaBD;
        }
    }
}

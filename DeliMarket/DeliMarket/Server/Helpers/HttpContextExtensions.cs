using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliMarket.Server.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosPaginacionEnRespuesta<T>(this HttpContext context,
            IQueryable<T> queryable, int cantidadRegistrosAMostrar)
        {
            if (context == null) { throw new ArgumentNullException(nameof(context)); } 

            double conteo = await queryable.CountAsync();       //cantidad de registros
            double totalPaginas = Math.Ceiling(conteo / cantidadRegistrosAMostrar);  //Total de páginas
            context.Response.Headers.Add("conteo", conteo.ToString()); //Se agrega conteo en la cabecera de la respuesta
            context.Response.Headers.Add("totalPaginas", totalPaginas.ToString()); //Se agrega totalDePaginas en la cabecera de la respuesta
        }
    }
}

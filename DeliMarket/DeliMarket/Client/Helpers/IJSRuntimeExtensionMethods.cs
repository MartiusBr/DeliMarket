using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DeliMarket.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string mensaje)
        {
            await js.InvokeVoidAsync("console.log", "prueba de console.log");
            return await js.InvokeAsync<bool>("confirm", mensaje);
        }

        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
           => js.InvokeAsync<object>(
               "localStorage.setItem",
               key, content
               );

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );

        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
            => js.InvokeAsync<object>(
                "localStorage.removeItem",
                key);

        public static async ValueTask InitMap(this IJSRuntime js, double lat, double lng)
        {
            await js.InvokeVoidAsync("initMap",lat,lng);
        }

        public static async ValueTask InitCosticWidgets(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("initCostic");
        }

        //public static async ValueTask<Object> GetLatLong(this IJSRuntime js)
        //{
        //    return await js.InvokeAsync<object>("GetLatLong");
        //}


    }
}

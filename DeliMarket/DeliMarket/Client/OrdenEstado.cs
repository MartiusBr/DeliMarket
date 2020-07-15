using DeliMarket.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliMarket.Client
{
    public class OrdenEstado
    {
        public bool ShowingConfigureDialog { get; private set; }

        public Detalle DetalleProdMercado { get; private set; }

        public Orden Orden { get; private set; } = new Orden();

        public void MostrarDetalleProdMerDialog(ProductoMercado productoMercado)
        {
            DetalleProdMercado = new Detalle()
            {
                Productomercado = productoMercado,
                Cantidad = 1
            };

            ShowingConfigureDialog = true;
        }

        public void CancelarDetalleProdMercado()
        {
            DetalleProdMercado = null;
            ShowingConfigureDialog = false;
        }

        public void ConfirmarDetalleProdMercado()
        {
            Orden.Detalles.Add(DetalleProdMercado);
            DetalleProdMercado = null;
            ShowingConfigureDialog = false;
        }

        public void RemoverDetalle(Detalle detalle)
        {
            Orden.Detalles.Remove(detalle);
        }

        public void ResetOrder()
        {
            Orden = new Orden();
        }

        public void ReplaceOrder(Orden order)
        {
            Orden = order;
        }
    }
}

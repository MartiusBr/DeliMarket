﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class PeliculaActor
    {
        public int PersonaId { get; set; }
        public int ProductoId { get; set; }
        public Persona Persona { get; set; }
        public Producto Producto { get; set; }
        public string Personaje { get; set; }
        public int Orden { get; set; }
    }
}

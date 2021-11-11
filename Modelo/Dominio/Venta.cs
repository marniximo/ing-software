using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public class Venta
    {
        public int Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public float Total { get; set; }
        public int Vendedor { get; set; }
    }
}

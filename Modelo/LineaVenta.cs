using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public class LineaVenta
    {
        public int IdProducto { get; set; }
        public LineaStock Producto { get; set; }
        public float PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public float Subtotal { get; set; }
        public int Venta { get; set; }
    }
}

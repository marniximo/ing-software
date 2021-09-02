using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public class Producto
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public float Costo { get; set; }
        public float NetoGravado { get; set; }
        public float Precio { get; set; }
        public float MargenGanancia { get; set; }
    }
}

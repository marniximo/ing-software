using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Utils
{
    public class ProductosUtils
    {
        public static Producto GetProducto() {
            return new Producto
            {
                Codigo = 1,
                Costo = 200,
                Descripcion = "Producto de prueba",
                Marca = 1,
                MargenGanancia = 20,
            };
        }
    }
}

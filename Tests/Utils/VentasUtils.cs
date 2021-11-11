using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Utils
{
    public class VentasUtils
    {
        public static Venta GetVenta()
        {
            return new Venta {
                Codigo = 1,
                Fecha = DateTime.Now,
                Total = 1000,
                Vendedor = 1
            };
        }
    }
}

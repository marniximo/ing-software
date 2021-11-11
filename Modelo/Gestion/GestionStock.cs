using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Gestion
{
    public class GestionStock
    {
        public static void AgregarStock(int codigoProducto, Color color, Talle talle, int stock, Sucursal sucursal)
        {
            var ultimoStock = Servidor.listaStock.LastOrDefault();
            int codigo = ultimoStock == null ? 1 : ultimoStock.Codigo + 1;
            var lineaStock = new LineaStock()
            {
                Codigo = codigo,
                Color = color.Codigo,
                Producto = codigoProducto,
                Stock = stock,
                Sucursal = sucursal.Codigo,
                Talle = talle.Codigo,
            };
            Servidor.listaStock.Add(lineaStock);
        }

        public static void ModificarStock(int codigo, int stock)
        {
            var lineaStock = BuscarStock(codigo);
            lineaStock.Stock += stock;
        }

        public static LineaStock BuscarStock(int codigoProducto, Color color, Talle talle, Sucursal sucursal)
        {
            var stock = Servidor.listaStock.FirstOrDefault(p => p.Producto == codigoProducto
                                                                    && p.Sucursal == sucursal.Codigo
                                                                    && p.Color == color.Codigo
                                                                    && p.Talle == talle.Codigo);
            return stock;
        }

        public static LineaStock BuscarStock(int codigo)
        {
            var stock = Servidor.listaStock.FirstOrDefault(p => p.Codigo == codigo);
            return stock;
        }
    }
}

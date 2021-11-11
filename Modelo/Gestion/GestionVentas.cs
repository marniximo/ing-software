using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Gestion
{
    public class GestionVentas
    {
        public static int AgregarVenta(float total, Usuario vendedor)
        {
            var ultimaVenta = Servidor.listaVentas.LastOrDefault();
            var codigo = ultimaVenta == null ? 1 : ultimaVenta.Codigo + 1;
            var venta = new Venta()
            {
                Codigo = codigo,
                Fecha = DateTime.Now,
                Total = total,
                Vendedor = vendedor.Legajo
            };
            Servidor.listaVentas.Add(venta);
            return venta.Codigo;
        }

        public static Venta BuscarVenta(int codigo)
        {
            var venta = Servidor.listaVentas.FirstOrDefault(v => v.Codigo == codigo);
            return venta;
        }

        public static object RealizarVenta(ref List<LineaVenta> carrito, Usuario usuario, CondicionTributaria condicionTributaria, string CUIT = "") {
            if (carrito.Count > 0)
            {
                float total = 0;
                foreach (var linea in carrito)
                {
                    total += linea.Subtotal;
                    linea.Producto.Stock -= linea.Cantidad;
                    //para una base de datos, tendriamos que actualizar el stock en este paso.
                }
                if (total > 10000 || condicionTributaria != CondicionTributaria.CF)
                {
                    if (!string.IsNullOrEmpty(CUIT))
                    {
                        var cliente = GestionClientes.BuscarCliente(CUIT);
                        if (cliente == null)
                        {
                            return new { Codigo = 1, Mensaje = "Cliente no encontrado!" };
                        }
                    }
                    else
                    {
                        return new { Codigo = 2, Mensaje = "Ingrese CUIT!" };
                    }
                }
                AgregarVenta(total, usuario);
                carrito = new List<LineaVenta>();
                return new { Codigo = 0, Mensaje = "" };
            }
            else
                return new { Codigo = 3, Mensaje = "El carrito esta vacio!" };
        }

        public static object AgregarAlCarrito(ref List<LineaVenta> carrito, int codigo, Color color, Talle talle, Sucursal sucursal, int cantidad) {
            var stock = GestionStock.BuscarStock(codigo, color, talle, sucursal);
            if (stock != null)
            {
                var producto = GestionProductos.BuscarProducto(stock.Producto);
                if (producto == null)
                    return new { Codigo = 7, Mensaje = "Producto no encontrado!" };
                if (stock.Stock >= cantidad)
                {
                    if (!carrito.Any(c => c.IdProducto == producto.Codigo))
                    {
                        var linea = new LineaVenta()
                        {
                            Cantidad = cantidad,
                            PrecioUnitario = producto.Precio,
                            IdProducto = producto.Codigo,
                            Producto = stock,
                            Subtotal = producto.Precio * cantidad
                        };
                        carrito.Add(linea);
                    }
                    else
                        return new { Codigo = 4, Mensaje = "Ya se ha agregado ese producto!" };
                }
                else
                    return new { Codigo = 5, Mensaje = "Stock insuficiente!" };
            }
            else
            {
                return new { Codigo = 6, Mensaje = "Stock inexistente!" };
            }
            return new { Codigo = 0, Mensaje = "" };
        }
    }
}

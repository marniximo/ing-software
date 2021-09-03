using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiendaIngSoft
{
    public static class GestorBaseDeDatos
    {
        public static void AgregarProducto(string descripcion, int marca, float costo, float margenGanancia) 
        {
            var neto = (1 + margenGanancia / 100) * costo;
            var precio = neto * (1 + Servidor.IVA_PORC);
            var ultimoProducto = Servidor.listaProductos.LastOrDefault();
            int codigo = ultimoProducto == null ? 1 : ultimoProducto.Codigo + 1;
            var prdocuto = new Producto()
            {
                Codigo = codigo,
                Costo = costo,
                Descripcion = descripcion,
                Marca = marca,
                NetoGravado = neto,
                Precio = precio,
                MargenGanancia = margenGanancia
            };
            Servidor.listaProductos.Add(prdocuto);
        }

        public static void ActualizarProducto(int codigo, string descripcion, int marca, float costo, float margenGanancia) {
            var producto = BuscarProducto(codigo);
            var neto = (1 + margenGanancia / 100) * costo;
            var precio = neto * (1 + Servidor.IVA_PORC);
            producto.Codigo = codigo;
            producto.Costo = costo;
            producto.Descripcion = descripcion;
            producto.Marca = marca;
            producto.NetoGravado = neto;
            producto.Precio = precio;
            producto.MargenGanancia = margenGanancia;
        }

        public static void EliminarProducto(int codigo) {
            var producto = BuscarProducto(codigo);
            Servidor.listaProductos.Remove(producto);
            var lineasStock = Servidor.listaStock.Where(l => l.Producto == producto.Codigo);
            foreach (var linea in lineasStock) {
                Servidor.listaStock.Remove(linea);
            }
        }

        public static Producto BuscarProducto(int codigo) {
            var producto = Servidor.listaProductos.FirstOrDefault( p => p.Codigo == codigo);
            return producto;
        }

        public static void AgregarStock(int codigoProducto, Color color, Talle talle, int stock, Sucursal sucursal) {
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

        public static void ModificarStock(int codigo, int stock) {
            var lineaStock = BuscarStock(codigo);
            lineaStock.Stock += stock;
        }

        public static LineaStock BuscarStock(int codigoProducto, Color color, Talle talle, Sucursal sucursal) {
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

        public static void AgregarVenta(float total, Usuario vendedor) {
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
        }

        public static Venta BuscarVenta(int codigo) {
            var venta = Servidor.listaVentas.FirstOrDefault( v => v.Codigo == codigo);
            return venta;
        }

        public static Cliente BuscarCliente(int cuit) {
            var cliente = Servidor.listaClientes.FirstOrDefault( c => c.CUIT == cuit);
            return cliente;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Gestion
{
    public class GestionProductos
    {
        public static int AgregarProducto(string descripcion, int marca, float costo, float margenGanancia)
        {
            var neto = (1 + margenGanancia / 100) * costo;
            var precio = neto * (1 + Servidor.IVA_PORC);
            var ultimoProducto = Servidor.listaProductos.LastOrDefault();
            int codigo = ultimoProducto == null ? 1 : ultimoProducto.Codigo + 1;
            var producto = new Producto()
            {
                Codigo = codigo,
                Costo = costo,
                Descripcion = descripcion,
                Marca = marca,
                NetoGravado = neto,
                Precio = precio,
                MargenGanancia = margenGanancia
            };
            Servidor.listaProductos.Add(producto);
            return producto.Codigo;
        }

        public static void ActualizarProducto(int codigo, string descripcion, int marca, float costo, float margenGanancia)
        {
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

        public static void EliminarProducto(int codigo)
        {
            var producto = BuscarProducto(codigo);
            Servidor.listaProductos.Remove(producto);
            var lineasStock = Servidor.listaStock.Where(l => l.Producto == producto.Codigo);
            foreach (var linea in lineasStock)
            {
                Servidor.listaStock.Remove(linea);
            }
        }

        public static Producto BuscarProducto(int codigo)
        {
            var producto = Servidor.listaProductos.FirstOrDefault(p => p.Codigo == codigo);
            return producto;
        }
    }
}

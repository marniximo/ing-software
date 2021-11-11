using System;
using System.Collections.Generic;
using System.Text;
using Modelo;
using Modelo.Gestion;
using Tests.Utils;
using TiendaIngSoft;
using Xunit;

namespace Tests
{
    public class ProductosTest
    {
        [Fact]
        public void TestAgregarProducto() {
            var producto = ProductosUtils.GetProducto();
            var codigo = GestionProductos.AgregarProducto(
                producto.Descripcion, 
                producto.Marca, 
                producto.Costo, 
                producto.MargenGanancia
            );
            var nuevoProducto = GestionProductos.BuscarProducto(codigo);
            Assert.NotEmpty(Servidor.listaProductos);
            Assert.Equal("Producto de prueba", nuevoProducto.Descripcion);
            Assert.Equal(1, nuevoProducto.Marca);
            Assert.Equal(200, nuevoProducto.Costo);
            Assert.Equal(240f, Math.Round(nuevoProducto.NetoGravado, 2));
            Assert.Equal(240 * 1.21f, nuevoProducto.Precio);
        }

        [Fact]
        public void TestActualizarProducto()
        {
            var producto = ProductosUtils.GetProducto();
            int codigo = GestionProductos.AgregarProducto(
                producto.Descripcion,
                producto.Marca,
                producto.Costo,
                producto.MargenGanancia
            );

            GestionProductos.ActualizarProducto(codigo, "Nueva descripcion", 2, 100, 30);

            var nuevoProducto = GestionProductos.BuscarProducto(codigo);

            Assert.Equal("Nueva descripcion", nuevoProducto.Descripcion);
            Assert.Equal(2, nuevoProducto.Marca);
            Assert.Equal(100, nuevoProducto.Costo);
            Assert.Equal(130, nuevoProducto.NetoGravado);
            Assert.Equal(130 * 1.21f, nuevoProducto.Precio);
        }

        [Fact]
        public void TestAgregarAlCarritoExitoso()
        {
            var carrito = new List<LineaVenta>();
            var respuesta = GestionVentas.AgregarAlCarrito(
                ref carrito,
                Servidor.listaProductos[0].Codigo, 
                Servidor.listaColores[0],
                Servidor.listaTalles[0],
                Servidor.listaSucursales[0],
                1
            ).ToType(typeof(Respuesta)) as Respuesta;
            Assert.True(respuesta.Codigo == 0);
            Assert.NotEmpty(carrito);
        }

        [Fact]
        public void TestAgregarAlCarritoExistente()
        {
            var carrito = new List<LineaVenta>();
            GestionVentas.AgregarAlCarrito(
                ref carrito,
                1,
                Servidor.listaColores[0],
                Servidor.listaTalles[0],
                Servidor.listaSucursales[0],
                1
            );
            var respuesta = GestionVentas.AgregarAlCarrito(
                ref carrito,
                1,
                Servidor.listaColores[0],
                Servidor.listaTalles[0],
                Servidor.listaSucursales[0],
                1
            ).ToType(typeof(Respuesta)) as Respuesta;
            Assert.True(respuesta.Codigo == 4);
            Assert.True(carrito.Count == 1);
        }

        [Fact]
        public void TestRealizarVentaExitosa()
        {
            var carrito = new List<LineaVenta> {
                new LineaVenta(){
                    Cantidad = 5,
                    IdProducto = 1,
                    PrecioUnitario = 550*1.21f,
                    Producto = Servidor.listaStock[0],
                    Subtotal = 550*1.21f*5,
                }
            };
            var respuesta = GestionVentas.RealizarVenta(
                ref carrito,
                Servidor.listaUsuarios[0],
                CondicionTributaria.CF
            ).ToType(typeof(Respuesta)) as Respuesta;
            Assert.True(respuesta.Codigo == 0);
        }
    }
}

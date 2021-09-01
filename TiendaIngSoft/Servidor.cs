using System;
using System.Collections.Generic;
using System.Text;
using Modelo;

namespace TiendaIngSoft
{
    public static class Servidor
    {
        public static List<Usuario> listaUsuarios = new List<Usuario>() {
            new Usuario(){
                Legajo = 1234,
                Password = "password"
            },
            new Usuario(){
                Legajo = 5678,
                Password = "password2"
            }
        };

        public static List<EspecificacionProducto> listaEspecificacionProductos = new List<EspecificacionProducto>() {
            new EspecificacionProducto(){
                Codigo = 1,
                Descripcion = "Cinturon Gucci",
                Costo = 500,
                Marca = "Gucci",
                NetoGravado = 600,
                Precio = 700
            },
            new EspecificacionProducto(){
                Codigo = 2,
                Descripcion = "Camisa Taverniti",
                Costo = 5000,
                Marca = "Taverniti",
                NetoGravado = 6000,
                Precio = 7000
            }
        };

        public static List<Producto> listaProductos = new List<Producto>() {
            new Producto(){
                Codigo = 3,
                EspecificacionProducto = 1,
                Color = 1,
                Stock = 500,
                Sucursal = 1,
                Talle = 1
            },
            new Producto(){
                Codigo = 4,
                EspecificacionProducto = 1,
                Color = 2,
                Stock = 200,
                Sucursal = 1,
                Talle = 1
            },
            new Producto(){
                Codigo = 5,
                EspecificacionProducto = 1,
                Color = 1,
                Stock = 100,
                Sucursal = 1,
                Talle = 2
            },
            new Producto(){
                Codigo = 6,
                EspecificacionProducto = 2,
                Color = 1,
                Stock = 600,
                Sucursal = 1,
                Talle = 1
            }
        };

        public static List<Venta> listaVentas = new List<Venta>();

        public static List<Color> listaColores = new List<Color>() { 
            new Color(){
                Codigo = 1,
                Descripcion = "Rojo",
            },
            new Color(){
                Codigo = 2,
                Descripcion = "Azul",
            }
        };

        public static List<Sucursal> listaSucursales = new List<Sucursal>(){
            new Sucursal(){
                Codigo = 1,
                Descripcion = "Centro",
            },
            new Sucursal(){
                Codigo = 2,
                Descripcion = "Norte",
            }
        };

        public static List<Talle> listaTalles = new List<Talle>(){
            new Talle(){
                Codigo = 1,
                Descripcion = "X",
            },
            new Talle(){
                Codigo = 2,
                Descripcion = "M",
            },
            new Talle(){
                Codigo = 3,
                Descripcion = "L",
            }
        };
    }
}

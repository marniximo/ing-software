using System;
using System.Collections.Generic;
using System.Text;
using Modelo;

namespace TiendaIngSoft
{
    public static class Servidor
    {
        public const float IVA_PORC = 0.21f;

        public static List<Usuario> listaUsuarios = new List<Usuario>() {
            new Usuario(){
                Legajo = 1234,
                Password = "password",
                Sucursal = 1
            },
            new Usuario(){
                Legajo = 5678,
                Password = "password2",
                Sucursal = 1
            }
        };

        public static List<Producto> listaProductos = new List<Producto>() {
            new Producto(){
                Codigo = 1,
                Descripcion = "Cinturon Gucci",
                Costo = 500,
                MargenGanancia = 10,
                Marca = "Gucci",
                NetoGravado = 550,
                Precio = 550*1.21f,
            },
            new Producto(){
                Codigo = 2,
                Descripcion = "Camisa Taverniti",
                Costo = 5000,
                MargenGanancia = 10,
                Marca = "Taverniti",
                NetoGravado = 5500,
                Precio = 5500*1.21f
            }
        };

        public static List<LineaStock> listaStock = new List<LineaStock>() {
            new LineaStock(){
                Codigo = 3,
                Producto = 1,
                Color = 1,
                Stock = 500,
                Sucursal = 1,
                Talle = 1
            },
            new LineaStock(){
                Codigo = 4,
                Producto = 1,
                Color = 2,
                Stock = 200,
                Sucursal = 1,
                Talle = 1
            },
            new LineaStock(){
                Codigo = 5,
                Producto = 1,
                Color = 1,
                Stock = 100,
                Sucursal = 1,
                Talle = 2
            },
            new LineaStock(){
                Codigo = 6,
                Producto = 2,
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

        public static List<Cliente> listaClientes = new List<Cliente>()
        {
            new Cliente(){
                CUIT = 12345,
                Domicilio = "mi casa",
                RazonSocial = "marianoSA"
            }
        };
    }
}

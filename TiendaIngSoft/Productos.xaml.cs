using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TiendaIngSoft
{
    /// <summary>
    /// Interaction logic for Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
        public Productos()
        {
            InitializeComponent();
            cmb_color.ItemsSource = Servidor.listaColores;
            cmb_color.DisplayMemberPath = "Descripcion";
            cmb_sucursal.ItemsSource = Servidor.listaSucursales;
            cmb_sucursal.DisplayMemberPath = "Descripcion";
            cmb_talle.ItemsSource = Servidor.listaTalles;
            cmb_talle.DisplayMemberPath = "Descripcion";
            refrescarEspecificaciones();
            refrescarProductos();
        }

        private void refrescarEspecificaciones() {
            dg_especificacion.ItemsSource = null;
            dg_especificacion.ItemsSource = Servidor.listaEspecificacionProductos;
        }

        private void refrescarProductos()
        {
            dg_producto.ItemsSource = null;
            dg_producto.ItemsSource = Servidor.listaProductos;
        }

        private void btn_okEspecificacion_Click(object sender, RoutedEventArgs e)
        {
            var ultimaEspecificacion = Servidor.listaEspecificacionProductos.LastOrDefault();
            int codigo = ultimaEspecificacion == null ? 1 : ultimaEspecificacion.Codigo + 1;
            var especificacion = new EspecificacionProducto() {
                Codigo = codigo,
                Costo = float.Parse(txt_costo.Text),
                Descripcion = txt_descripcion.Text,
                Marca = txt_marca.Text,
                NetoGravado = float.Parse(txt_netoGravado.Text),
                Precio = float.Parse(txt_precio.Text)
            };
            Servidor.listaEspecificacionProductos.Add(especificacion);
            refrescarEspecificaciones();
        }

        private void btn_okProductos_Click(object sender, RoutedEventArgs e)
        {
            var ultimoProducto = Servidor.listaEspecificacionProductos.LastOrDefault();
            int codigo = ultimoProducto == null ? 1 : ultimoProducto.Codigo + 1;
            var color = cmb_color.SelectedItem as Modelo.Color;
            var talle = cmb_talle.SelectedItem as Talle;
            var sucursal = cmb_sucursal.SelectedItem as Sucursal;
            var producto = new Producto()
            {
                Codigo = codigo,
                Color = color.Codigo,
                EspecificacionProducto = int.Parse(txt_especificacion.Text),
                Stock = int.Parse(txt_cantidad.Text),
                Sucursal = sucursal.Codigo,
                Talle = talle.Codigo,
            };
            Servidor.listaProductos.Add(producto);
            refrescarProductos();
        }
    }
}

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
        EspecificacionProducto especificacionSeleccionada;

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

        private void vaciarCamposEspecificacion() {
            txt_marca.Text = "";
            txt_descripcion.Text = "";
            txt_costo.Text = "";
            txt_margenGanancia.Text = "";
        }

        private void llenarCamposEspecificacion()
        {
            txt_marca.Text = especificacionSeleccionada.Marca;
            txt_descripcion.Text = especificacionSeleccionada.Descripcion;
            txt_costo.Text = especificacionSeleccionada.Costo.ToString();
            txt_margenGanancia.Text = (especificacionSeleccionada.NetoGravado/especificacionSeleccionada.Costo).ToString();
        }

        private void btn_okEspecificacion_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btn_okEspecificacion.Content == "Crear")
            {
                var ultimaEspecificacion = Servidor.listaEspecificacionProductos.LastOrDefault();
                int codigo = ultimaEspecificacion == null ? 1 : ultimaEspecificacion.Codigo + 1;
                var porcentaje = float.Parse(txt_margenGanancia.Text);
                var costo = float.Parse(txt_costo.Text);
                var neto = (1 + porcentaje / 100) * costo;
                var precio = neto * 1 + Servidor.IVA_PORC;
                var especificacion = new EspecificacionProducto()
                {
                    Codigo = codigo,
                    Costo = float.Parse(txt_costo.Text),
                    Descripcion = txt_descripcion.Text,
                    Marca = txt_marca.Text,
                    NetoGravado = neto,
                    Precio = precio
                };
                Servidor.listaEspecificacionProductos.Add(especificacion);
                refrescarEspecificaciones();
                vaciarCamposEspecificacion();
                MessageBox.Show("Producto Creado!");
            }
            else {
                
                btn_okEspecificacion.Content = "Crear";
            }
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

        private void dg_especificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && dg_especificacion.SelectedIndex >= 0) {
                btn_okEspecificacion.Content = "Actualizar";
                llenarCamposEspecificacion();
            }
        }
    }
}

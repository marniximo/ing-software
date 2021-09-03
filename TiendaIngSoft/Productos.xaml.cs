using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Producto productoSeleccionado;
        public Productos()
        {
            InitializeComponent();
            cmb_color.ItemsSource = Servidor.listaColores;
            cmb_color.DisplayMemberPath = "Descripcion";
            cmb_sucursal.ItemsSource = Servidor.listaSucursales;
            cmb_sucursal.DisplayMemberPath = "Descripcion";
            cmb_talle.ItemsSource = Servidor.listaTalles;
            cmb_talle.DisplayMemberPath = "Descripcion";
            cmb_marca.ItemsSource = Servidor.listaMarcas;
            cmb_marca.DisplayMemberPath = "Descripcion";
            refrescarStocks();
            refrescarProductos();
        }

        private void refrescarProductos() {
            dg_productos.ItemsSource = null;
            dg_productos.ItemsSource = Servidor.listaProductos;
        }

        private void refrescarStocks()
        {
            dg_stock.ItemsSource = null;
            dg_stock.ItemsSource = Servidor.listaStock;
        }

        private void vaciarCamposProductos() {
            txt_descripcion.Text = "";
            txt_costo.Text = "";
            txt_margenGanancia.Text = "";
        }

        private void llenarCamposProductos()
        {
            cmb_marca.SelectedItem = productoSeleccionado.Marca;
            txt_descripcion.Text = productoSeleccionado.Descripcion;
            txt_costo.Text = productoSeleccionado.Costo.ToString();
            txt_margenGanancia.Text = productoSeleccionado.MargenGanancia.ToString();
        }

        private void btn_okProductos_Click(object sender, RoutedEventArgs e)
        {
            var porcentaje = float.Parse(txt_margenGanancia.Text);
            var costo = float.Parse(txt_costo.Text);
            var descripcion = txt_descripcion.Text;
            var marca = cmb_marca.SelectedItem as Marca;
            if ((string)btn_okProductos.Content == "Crear")
            {
                GestorBaseDeDatos.AgregarProducto(descripcion, marca.Codigo, costo, porcentaje);
                MessageBox.Show("Producto Creado!");
            }
            else
            {
                GestorBaseDeDatos.ActualizarProducto(productoSeleccionado.Codigo, descripcion, marca.Codigo, costo, porcentaje);
                btn_okProductos.Content = "Crear";
                MessageBox.Show("Producto Actualizado!");
            }
            refrescarProductos();
            vaciarCamposProductos();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btn_okStock_Click(object sender, RoutedEventArgs e)
        {
            var color = cmb_color.SelectedItem as Modelo.Color;
            var talle = cmb_talle.SelectedItem as Talle;
            var sucursal = cmb_sucursal.SelectedItem as Sucursal;
            var producto = int.Parse(txt_producto.Text);
            var stock = GestorBaseDeDatos.BuscarStock(producto, color, talle, sucursal);
            var cantidad = int.Parse(txt_cantidad.Text);
            if (stock == null)
            {
                GestorBaseDeDatos.AgregarStock(producto, color, talle, cantidad, sucursal);
                MessageBox.Show("Stock Creado!");
            }
            else
            {
                GestorBaseDeDatos.ModificarStock(cantidad, cantidad);
                MessageBox.Show("Stock Actualizado!");
            }
            refrescarStocks();
        }

        private void dg_productos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && dg_productos.SelectedIndex >= 0)
            {
                btn_okProductos.Content = "Actualizar";
                productoSeleccionado = dg_productos.SelectedItem as Producto;
                llenarCamposProductos();
            }
            else if(e.Key == Key.Delete && dg_productos.SelectedIndex >= 0)
            {
                productoSeleccionado = dg_productos.SelectedItem as Producto;
                GestorBaseDeDatos.EliminarProducto(productoSeleccionado.Codigo);
                productoSeleccionado = null;
                MessageBox.Show("Producto Eliminado!");
                refrescarProductos();
            }
        }

        
    }
}

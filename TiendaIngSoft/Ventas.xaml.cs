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
    /// Interaction logic for Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {
        List<LineaVenta> carrito = new List<LineaVenta>();
        Cliente cliente;
        public Ventas()
        {
            InitializeComponent();
            refrescarVentas();
            refrescarCarrito();
            cliente = null;
        }

        private void refrescarVentas() {
            dg_ventas.ItemsSource = null;
            dg_ventas.ItemsSource = Servidor.listaVentas;
        }

        private void refrescarCarrito()
        {
            dg_carrito.ItemsSource = null;
            dg_carrito.ItemsSource = carrito;
            try
            {
                dg_carrito.Columns[1].Visibility = Visibility.Hidden;
                dg_carrito.Columns[5].Visibility = Visibility.Hidden;
            }
            catch { }
        }

        private void btn_registrar_Click(object sender, RoutedEventArgs e)
        {
            if (carrito.Count > 0)
            {
                float total = 0;
                var ultimaVenta = Servidor.listaVentas.LastOrDefault();
                var codigo = ultimaVenta == null ? 1 : ultimaVenta.Codigo + 1;
                foreach (var linea in carrito)
                {
                    total += linea.Subtotal;
                    linea.Venta = codigo;
                    linea.Producto.Stock -= linea.Cantidad;
                }
                var venta = new Venta()
                {
                    Codigo = codigo,
                    Fecha = DateTime.Now,
                    Total = total,
                    Vendedor = Configuracion.UsuarioActual.Legajo
                };
                Servidor.listaVentas.Add(venta);
                MessageBox.Show("Venta Registrada!");
                carrito = new List<LineaVenta>();
                refrescarVentas();
                refrescarCarrito();
            }
            else {
                MessageBox.Show("El carrito esta vacio!");
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            dg_ventas.MinColumnWidth = (dg_ventas.ActualWidth - SystemParameters.VerticalScrollBarWidth) / 4 - 2;
            dg_carrito.MinColumnWidth = (dg_carrito.ActualWidth - SystemParameters.VerticalScrollBarWidth) / 4 - 2;
            dg_carrito.Columns[1].Visibility = Visibility.Hidden;
            dg_carrito.Columns[5].Visibility = Visibility.Hidden;
        }

        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            var producto = Servidor.listaProductos.FirstOrDefault(p => p.EspecificacionProducto == int.Parse(txt_codigo.Text) 
                                                                        && p.Sucursal           == Configuracion.Sucursal 
                                                                        && p.Color              == int.Parse(txt_color.Text) 
                                                                        && p.Talle              == int.Parse(txt_talle.Text));
            if (producto != null)
            {
                var especificacion = Servidor.listaEspecificacionProductos.FirstOrDefault(e => e.Codigo == producto.EspecificacionProducto);
                int cantidad = int.Parse(txt_cantidad.Text);
                if (producto.Stock >= cantidad)
                {
                    if (!carrito.Any(c => c.IdProducto == producto.Codigo))
                    {
                        var linea = new LineaVenta()
                        {
                            Cantidad = cantidad,
                            PrecioUnitario = especificacion.Precio,
                            IdProducto = producto.Codigo,
                            Producto = producto,
                            Subtotal = especificacion.Precio * cantidad
                        };
                        carrito.Add(linea);
                    }
                    else {
                        MessageBox.Show("Ya se ha agregado ese producto!");
                    }
                }
                else {
                    MessageBox.Show("Stock insuficiente!");
                }
            }
            else {
                MessageBox.Show("Producto no encontrado!");
            }
            
            refrescarCarrito();
        }

        private void dg_carrito_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) {
                if (dg_carrito.SelectedItem != null) {
                    var item = dg_carrito.SelectedItem as LineaVenta;
                    carrito.Remove(item);
                    refrescarCarrito();
                }
            }
        }

        private void volverCliente(object sender, EventArgs e) {
            this.Show();
            cliente = Servidor.listaClientes.FirstOrDefault(c => c.CUIT == int.Parse(txt_cuit.Text));
            if (cliente == null) {
                MessageBox.Show("Cliente no encontrado!");
                txt_cuit.Text = "";
            }
        }

        private void txt_cuit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_cuit.Text.Length > 0)
            {
                cliente = Servidor.listaClientes.FirstOrDefault(c => c.CUIT == int.Parse(txt_cuit.Text));
                if (cliente == null)
                {
                    var clientes = new Clientes();
                    clientes.Show();
                    this.Hide();
                    clientes.Closed += volverCliente;
                }
            }
        }

        private void cb_condicionTributaria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cb_condicionTributaria.SelectedIndex != 0)
                {
                    txt_cuit.IsEnabled = true;
                }
                else
                {
                    txt_cuit.IsEnabled = false;
                }
            }
            catch { }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

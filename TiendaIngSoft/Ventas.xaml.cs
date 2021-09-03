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
            cmb_color.ItemsSource = Servidor.listaColores;
            cmb_color.DisplayMemberPath = "Descripcion";
            cmb_talle.ItemsSource = Servidor.listaTalles;
            cmb_talle.DisplayMemberPath = "Descripcion";
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
        }

        private void btn_registrar_Click(object sender, RoutedEventArgs e)
        {

            if (carrito.Count > 0)
            {
                float total = 0;
                foreach (var linea in carrito)
                {
                    total += linea.Subtotal;
                    //linea.Venta = codigo;
                    linea.Producto.Stock -= linea.Cantidad;
                }
                if ( total > 10000 || (string)cb_condicionTributaria.SelectedItem != "CF") {
                    if (txt_cuit.Text.Length > 0)
                    {
                        cliente = GestorBaseDeDatos.BuscarCliente(int.Parse(txt_cuit.Text));
                        if (cliente == null)
                        {
                            var clientes = new Clientes();
                            clientes.Show();
                            this.Hide();
                            clientes.Closed += volverCliente;
                            return;
                        }
                    }
                    else {
                        MessageBox.Show("Ingrese CUIT!");
                        return;
                    }
                }
                GestorBaseDeDatos.AgregarVenta(total, Configuracion.UsuarioActual);
                MessageBox.Show("Aqui iria una factura");
                MessageBox.Show("Venta Registrada!");
                carrito = new List<LineaVenta>();
                refrescarVentas();
                refrescarCarrito();
            }
            else {
                MessageBox.Show("El carrito esta vacio!");
            }
        }

        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            var stock = GestorBaseDeDatos.BuscarStock(int.Parse(txt_codigo.Text), (Modelo.Color)cmb_color.SelectedItem, (Talle)cmb_talle.SelectedItem, Configuracion.Sucursal);
            if (stock != null)
            {
                var producto = GestorBaseDeDatos.BuscarProducto(stock.Producto);
                int cantidad = int.Parse(txt_cantidad.Text);
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
            cliente = GestorBaseDeDatos.BuscarCliente(int.Parse(txt_cuit.Text));
            if (cliente == null)
            {
                MessageBox.Show("Cliente no encontrado!");
                txt_cuit.Text = "";
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

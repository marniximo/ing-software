using Modelo;
using Modelo.Gestion;
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
            cmb_condicionTributaria.ItemsSource = Enum.GetValues(typeof(CondicionTributaria)).Cast<CondicionTributaria>();
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
            
            var result = GestionVentas.RealizarVenta(
                ref carrito,
                Configuracion.UsuarioActual,
                (CondicionTributaria)cmb_condicionTributaria.SelectedItem
            ).ToType(typeof(Respuesta)) as Respuesta;
            switch (result.Codigo)
            {
                case 0:
                    MessageBox.Show("Aqui iria una factura");
                    MessageBox.Show("Venta Realizada!");
                    break;
                case 1:
                    //Hacer nuevo cliente
                    break;
                default:
                    MessageBox.Show(result.Mensaje);
                    break;
            }
            refrescarVentas();
            refrescarCarrito();
        }

        private void btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            var result = GestionVentas.AgregarAlCarrito(
                ref carrito,
                int.Parse(txt_codigo.Text),
                (Modelo.Color)cmb_color.SelectedItem,
                (Talle)cmb_talle.SelectedItem,
                Configuracion.Sucursal,
                int.Parse(txt_cantidad.Text)
            ).ToType(typeof(Respuesta)) as Respuesta;

            if (result.Codigo != 0) {
                MessageBox.Show(result.Mensaje);
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
            cliente = GestionClientes.BuscarCliente(txt_cuit.Text);
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

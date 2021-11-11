using System;
using System.Collections.Generic;
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
    /// Interaction logic for Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
        }

        public void AbrirProductos()
        {
            var productos = new Productos();
            productos.Show();
            this.Hide();
            productos.Closed += Mostrar;
        }

        public void AbrirVentas()
        {
            var ventas = new Ventas();
            ventas.Show();
            this.Hide();
            ventas.Closed += Mostrar;
        }

        public void Mostrar(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btn_productos_Click(object sender, RoutedEventArgs e)
        {
            AbrirProductos();
        }

        private void btn_ventas_Click(object sender, RoutedEventArgs e)
        {
            AbrirVentas();
        }
    }
}

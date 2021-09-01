using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TiendaIngSoft
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public bool ConfirmarLogin(int legajo, string password) {
            foreach (var usuario in Servidor.listaUsuarios) {
                if (usuario.Legajo == legajo && usuario.Password == password && usuario.Sucursal == Configuracion.Sucursal) {
                    Configuracion.UsuarioActual = usuario;
                    return true;
                }
            }
            return false;
        }

        public void AbrirPrincipal() {
            var principal = new Principal();
            principal.Show();
            this.Hide();
            principal.Closed += Mostrar;
        }

        public void Mostrar(object sender, EventArgs e) {
            this.Show();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            int legajo = int.Parse(txt_legajo.Text);
            string password = txt_password.Password;
            if (ConfirmarLogin(legajo, password))
            {
                AbrirPrincipal();
            }
            else {
                MessageBox.Show("Combinacion incorrecta!");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Gestion
{
    public class GestionClientes
    {
        public static Cliente BuscarCliente(string cuit)
        {
            var cliente = Servidor.listaClientes.FirstOrDefault(c => c.CUIT == cuit);
            return cliente;
        }
    }
}

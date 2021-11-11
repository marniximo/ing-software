using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Utils
{
    public class UsuariosUtils
    {
        public static Usuario GetUsuario() {
            return new Usuario
            {
                Legajo = 1234,
                Password = "password",
                Sucursal = 1,
            };
        }
    }
}

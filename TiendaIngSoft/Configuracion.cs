using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiendaIngSoft
{
    public class Configuracion
    {
        public static Sucursal Sucursal = new Sucursal() {
            Codigo = 1,
            Descripcion = "Centro"
        };
        public static Usuario UsuarioActual = null;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Dto
{
    /// <summary>
    /// clase con parametros para mostrar usuario y cuenta
    /// </summary>
    public class Usuarios
    {
        /// <summary>
        /// identificador de usuario
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// nombre del usuario
        /// </summary>
        public String Nombre { get; set; }
        /// <summary>
        /// apellido del usuario
        /// </summary>
        public String Apellido { get; set; }
        /// <summary>
        /// numero de cuenta del usuario
        /// </summary>
        public int NCuenta { get; set; }

    }
}
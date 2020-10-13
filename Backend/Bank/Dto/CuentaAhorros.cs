using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Dto
{
    /// <summary>
    /// clase con parametros para crear cuenta de ahorros
    /// </summary>
    public class CuentaAhorros
    {
        /// <summary>
        /// nombre del usuario
        /// </summary>
        public String Nombre { get; set; }
        /// <summary>
        /// apellido del usuario
        /// </summary>
        public String Apellido { get; set; }
        /// <summary>
        /// valor inicial de la cuenta de ahorros
        /// </summary>
        public decimal Valor { get; set; }

    }
}
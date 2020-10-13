using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Dto

{
    /// <summary>
    /// clase con parametros para realizar movimientos
    /// </summary>
    public class Movimientos
    {
        /// <summary>
        /// numero de cuenta 
        /// </summary>
        public int NCuenta { get; set; }
        /// <summary>
        /// valor de la cuenta
        /// </summary>
        public decimal Valor { get; set; }
    }
}
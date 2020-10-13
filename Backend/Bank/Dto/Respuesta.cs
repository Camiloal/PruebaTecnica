using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Dto
{
    public class Respuesta
    {
        public Boolean Estado { get; set; }
        public String Msg { get; set; }
        public object Data { get; set; }

    }
}
using Bank.Controllers;
using Bank.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Bank.Tests.Controllers
{
    [TestClass]
    public class BankControllerTest
    {
        [TestMethod]
        public void TodosLosUsuarios()
        {

            // Disponer
            var bankController = new BankController();

            //Actuar 
            var result = ((OkNegotiatedContentResult<Respuesta>)bankController.TodosLosUsuarios()).Content;
            var datos = (List<Usuarios>)result.Data;

            // Declarar     
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Estado);
            Assert.AreEqual(2, datos.Count);
        }

    }
}

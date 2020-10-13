using Bank.Dto;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.HtmlControls;

namespace Bank.Controllers
{
    /// <summary>
    /// controlador de bank
    /// </summary>
    [RoutePrefix("api/bank")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BankController : ApiController
    {

        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// optiene todos los usurios
        /// </summary>
        /// <returns>lista de usuarios</returns>
        [HttpGet]
        [Route("lista-usuarios")]
        public IHttpActionResult TodosLosUsuarios()
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (var db = new BankEntities())
                {

                    db.Configuration.LazyLoadingEnabled = false;
                    var usuarios = db.Usuario.Include("Cuenta").Select(x => new Usuarios
                    {
                        IdUsuario = x.IdUsuario,
                        Nombre = x.Nombre,
                        Apellido = x.Apellido,
                        NCuenta = x.Cuenta != null ? x.Cuenta.NCuenta : 0
                    }).ToList();
                    if (usuarios.Count() == 0)
                    {
                        respuesta.Estado = false;
                        respuesta.Msg = "No hay datos";
                        respuesta.Data = null;
                        return Ok(respuesta);
                    }
                    log.Info("Se consulto la lista de usuarios");
                    respuesta.Estado = true;
                    respuesta.Msg = "Se consulto la lista de usuarios";
                    respuesta.Data = usuarios;
                    return Ok(respuesta);
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error al consultar {ex.Message}");
                respuesta.Estado = false;
                respuesta.Msg = "Algo salio mal al traer la informaciòn";
                respuesta.Data = null;
                return Ok(respuesta);
            }


        }

        /// <summary>
        /// Crea una cuante de ahorros a un usuario con valor inicial de cuenta
        /// </summary>
        /// <param name="nuevacuenta">objeto con datos para crear cuenta</param>
        /// <returns>resultado de la operacion</returns>
        [HttpPost]
        [Route("crear-cuenta")]
        public IHttpActionResult CrearCuentaH([FromBody] CuentaAhorros nuevacuenta)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Usuario nuevoUsuario = new Usuario()
                {
                    Nombre = nuevacuenta.Nombre,
                    Apellido = nuevacuenta.Apellido
                };

                Cuenta nuevaCuenta = new Cuenta()
                {
                    Valor = nuevacuenta.Valor,
                };
                nuevoUsuario.Cuenta = nuevaCuenta;
                nuevaCuenta.Usuario = nuevoUsuario;

                using (var db = new BankEntities())
                {
                    db.Usuario.Add(nuevoUsuario);
                    db.SaveChanges();
                }

                log.Info($"Secreo la cuenta del usuario {nuevoUsuario.Nombre}");
                respuesta.Estado = true;
                respuesta.Msg = $"Se creo la cuenta {nuevoUsuario.Cuenta.NCuenta} al cliente {nuevoUsuario.Nombre} con un saldo de ${nuevoUsuario.Cuenta.Valor}";
                respuesta.Data = nuevoUsuario;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                log.Error($"Error al consultar {ex.Message}");
                respuesta.Estado = false;
                respuesta.Msg = "Erro al crear cuenta de ahorros";
                respuesta.Data = null;
                return Ok(respuesta);
            }

        }

        /// <summary>
        /// consigna dinero a una cuenta de ahorros
        /// </summary>
        /// <param name="consignar">objeto con datos para consignar</param>
        /// <returns>resultados de la operacion</returns>
        [HttpPost]
        [Route("consignar")]
        public IHttpActionResult Consignar([FromBody] Movimientos consignar)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (var db = new BankEntities())
                {
                    var ncuenta = db.Cuenta.Where(x => x.NCuenta == consignar.NCuenta).FirstOrDefault();
                    if (ncuenta != null)
                    {
                        ncuenta.Valor = ncuenta.Valor + consignar.Valor;

                        db.SaveChanges();

                        log.Info($"Se consigno ${consignar.Valor} a la cuenta {ncuenta.NCuenta}");
                        respuesta.Estado = true;
                        respuesta.Msg = $"Se consigno ${consignar.Valor} a la cuenta {ncuenta.NCuenta}";
                        respuesta.Data = consignar;
                        return Ok(respuesta);
                    }
                    else
                    {
                        log.Warn("La cuenta no existe");
                        respuesta.Estado = false;
                        respuesta.Msg = "La cuenta no existe";
                        respuesta.Data = null;
                        return Ok(respuesta);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error($"Error al consultar {ex.Message}");
                respuesta.Estado = false;
                respuesta.Msg = "Error al consignar";
                respuesta.Data = null;
                return Ok(respuesta);

            }
        }

        /// <summary>
        /// Retita dinero de una cuenta de ahorros
        /// </summary>
        /// <param name="retirar">objeto con datos para retirar</param>
        /// <returns>resultados de la operacion</returns>
        [HttpPost]
        [Route("retirar")]
        public IHttpActionResult Retirar([FromBody] Movimientos retirar)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (var db = new BankEntities())
                {
                    var ncuenta = db.Cuenta.Where(x => x.NCuenta == retirar.NCuenta).FirstOrDefault();
                    if (ncuenta != null)
                    {
                        if (ncuenta.Valor >= retirar.Valor)
                        {
                            ncuenta.Valor = ncuenta.Valor - retirar.Valor;

                            db.SaveChanges();
                        }
                        else
                        {
                            log.Warn("La cantidad a retirar es superior a la cantidad disponible.");
                            respuesta.Estado = false;
                            respuesta.Msg = "La cantidad a retirar es superior a la cantidad disponible.";
                            respuesta.Data = null;
                            return Ok(respuesta);
                        }

                        log.Info($"Se retiro ${retirar.Valor} de la cuenta {ncuenta.NCuenta}");
                        respuesta.Estado = true;
                        respuesta.Msg = $"Se retiro ${retirar.Valor} de la cuenta {ncuenta.NCuenta}";
                        respuesta.Data = retirar;
                        return Ok(respuesta);
                    }
                    else
                    {
                        log.Warn("La cuenta no existe");
                        respuesta.Estado = false;
                        respuesta.Msg = "La cuenta no existe";
                        respuesta.Data = null;
                        return Ok(respuesta);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error($"Error al consultar {ex.Message}");
                respuesta.Estado = false;
                respuesta.Msg = "Error al retirar";
                respuesta.Data = null;
                return Ok(respuesta);
            }
        }
        /// <summary>
        /// consulta el salgo de una cuenta
        /// </summary>
        /// <param name="ncuenta">numero de cuenta</param>
        /// <returns>resultados de la operacion</returns>
        [HttpGet]
        [Route("consultar-saldo/{ncuenta}")]
        public IHttpActionResult ConsultarSaldo(int ncuenta)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (var db = new BankEntities())
                {
                    var consultar = db.Cuenta.Where(x => x.NCuenta == ncuenta).FirstOrDefault();
                    if (consultar != null)
                    {
                        log.Info($"La cuenta {consultar.NCuenta} tiene un saldo de ${consultar.Valor}");
                        respuesta.Estado = true;
                        respuesta.Msg = $"La cuenta {consultar.NCuenta} tiene un saldo de ${consultar.Valor}";
                        respuesta.Data = null;
                        return Ok(respuesta);

                    }
                    else
                    {

                        log.Warn("La cuenta no existe");
                        respuesta.Estado = false;
                        respuesta.Msg = "La cuenta no existe";
                        respuesta.Data = null;
                        return Ok(respuesta);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error($"Error al consultar {ex.Message}");
                respuesta.Estado = false;
                respuesta.Msg = "Error al consultar saldo.";
                respuesta.Data = null;
                return Ok(respuesta);
            }

        }
    }
}

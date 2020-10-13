import axios from "axios";

export default class BankService {
  // Url del servicio
  url = "https://localhost:44392/api/bank/";

  //Servicio que trae todos los usuarios
  todosLosUsuarios() {
    return axios.get(this.url + "lista-usuarios");
  }
  //Servicio que crea un usuario con su respectiva cuenta
  crearCeuntahorros(cuentaAhorros) {
    return axios.post(this.url + "crear-cuenta", cuentaAhorros);
  }
  //Servicio para cosignar dinero a una cuenta especifica
  consignar(cuenta) {
    return axios.post(this.url + "consignar", cuenta);
  }
  //Servicio para retirar dinero de una cuenta especifica
  retirar(cuenta) {
    return axios.post(this.url + "retirar", cuenta);
  }
  //Servicio para consultar el saldo de una cuenta especifica
  consultarSaldo(nCuenta) {
    return axios.get(this.url + "consultar-saldo/" + nCuenta);
  }
}

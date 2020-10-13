<template>
  <div style="margin: 0 auto; width: 50%">
    <!-- Mensaje -->
    <Toast />

    <!-- Contenedor -->
    <Panel header="Cuenta de Ahorros">
      <!-- Menu -->
      <Menubar :model="items" />
      <br />

      <!-- Tabla -->
      <DataTable
        :value="usuarios"
        :paginator="true"
        :rows="10"
        selectionMode="single"
        :selection.sync="seleccionarUsuario"
        dataKey="IdUsuario"
      >
        <Column field="IdUsuario" header="Codigo"></Column>
        <Column field="Nombre" header="Nombre"></Column>
        <Column field="Apellido" header="Apellido"></Column>
        <Column field="NCuenta" header="Numero de cuenta"></Column>
      </DataTable>
    </Panel>

    <!-- Modal para crear cuenta de ahorros -->
    <Dialog header="Crear cuenta" :visible.sync="displayModal" :modal="true">
      <span class="p-float-label">
        <InputText
          id="nombre"
          type="text"
          v-model="usuarioCuenta.nombre"
          style="width: 100%"
        />

        <small v-if="errorsNombre" id="nombre-help" class="p-invalid">{{
          errorsNombre
        }}</small>
        <label for="nombre">Nombre del usuario</label>
      </span>
      <br />
      <span class="p-float-label">
        <InputText
          id="apellido"
          type="text"
          v-model="usuarioCuenta.apellido"
          style="width: 100%"
        />
        <small v-if="errorsApellido" id="apellido-help" class="p-invalid">{{
          errorsApellido
        }}</small>
        <label for="apellido">Apellido</label>
      </span>
      <br />
      <span class="p-float-label">
        <InputText
          id="valor"
          type="number"
          min="0"
          v-model="usuarioCuenta.valor"
          style="width: 100%"
        />
        <small v-if="errorsValor" id="valor-help" class="p-invalid">{{
          errorsValor
        }}</small>
        <label for="valor">Valor inicial de la cuenta</label>
      </span>
      <template #footer>
        <Button
          label="Cancelar"
          icon="pi pi-times"
          @click="cerrarModalCrear"
          class="p-button-text"
        />
        <Button
          label="Guerdar"
          icon="pi pi-check"
          @click="validacionCrear"
          autofocus
        />
      </template>
    </Dialog>

    <!-- Modal para realizar consignación -->
    <Dialog
      header="Consignar"
      :visible.sync="displayModalCongignar"
      :modal="true"
    >
      <span class="p-float-label">
        <InputText
          id="valor"
          type="number"
          min="0"
          v-model="movimiento.valor"
          style="width: 100%"
        />
        <small v-if="errorsValor" id="valor-help" class="p-invalid">{{
          errorsValor
        }}</small>
        <label for="valor">Valor a congignar</label>
      </span>
      <template #footer>
        <Button
          label="Cancelar"
          icon="pi pi-times"
          @click="cerrarModalConsignar"
          class="p-button-text"
        />
        <Button
          label="Consignar"
          icon="pi pi-check"
          @click="validacionConsignar"
          autofocus
        />
      </template>
    </Dialog>

    <!-- Modal para realizar retiro -->
    <Dialog header="Retirar" :visible.sync="displayModalRetirar" :modal="true">
      <span class="p-float-label">
        <InputText
          id="valor"
          type="number"
          min="0"
          v-model="movimiento.valor"
          style="width: 100%"
        />
        <small v-if="errorsValor" id="valor-help" class="p-invalid">{{
          errorsValor
        }}</small>
        <label for="valor">Valor a retirar</label>
      </span>
      <template #footer>
        <Button
          label="Cancelar"
          icon="pi pi-times"
          @click="cerrarModalRetirar"
          class="p-button-text"
        />
        <Button
          label="Retirar"
          icon="pi pi-check"
          @click="validacionRetirar"
          autofocus
        />
      </template>
    </Dialog>
  </div>
</template>

<script>
import BankService from "../service/BankService";

export default {
  name: "BankApp",
  data() {
    return {
      //objeto para crear una cuenta de ahorros
      usuarioCuenta: {
        nombre: null,
        apellido: null,
        valor: null,
      },
      //objeto para seleccionar un usuario con su respectiva cuenta
      seleccionarUsuario: {
        idUsuario: null,
        nombre: null,
        apellido: null,
        nCuenta: null,
      },
      //objeto para realizar movimientos "Consignar y retirar"
      movimiento: {
        nCuenta: null,
        valor: null,
      },
      //Lista de usuarios
      usuarios: null,

      //Botones del menu
      items: [
        {
          //Boton para crear cuenta
          label: "Crear cuenta",
          icon: "pi pi-fw pi-plus",
          command: () => {
            this.abrirModalCrear();
          },
        },
        {
          //Boton para consignar
          label: "Consignar",
          icon: "pi pi-sign-in",
          command: () => {
            this.abrirModalConsignar();
          },
        },
        {
          //Boton para retirar
          label: "Retirar",
          icon: "pi pi-sign-out",
          command: () => {
            this.abrirModalRetirar();
          },
        },
        {
          //Boton para consultar saldo
          label: "Consultar saldo",
          icon: "pi pi-money-bill",
          command: () => {
            this.consultarSaldo();
          },
        },
      ],
      //Estado de modal crear cuenta
      displayModal: false,
      //Estado de modal consignar
      displayModalCongignar: false,
      //Estado de modal retirar
      displayModalRetirar: false,
      //Validacion de nombre
      errorsNombre: null,
      //Validacion de apellido
      errorsApellido: null,
      //Validacion de valor
      errorsValor: null,
    };
  },
  //Variable global del servicio
  bankService: null,
  created() {
    this.bankService = new BankService();
  },
  mounted() {
    this.todosLosUsuarios();
  },
  methods: {
    //Método para validar campos vacíos al crear una cuenta
    validacionCrear(e) {
      if (
        this.usuarioCuenta.nombre &&
        this.usuarioCuenta.apellido &&
        this.usuarioCuenta.valor
      ) {
        this.crearCuenta();
      }

      this.errorsNombre = null;
      this.errorsApellido = null;
      this.errorsValor = null;

      if (!this.usuarioCuenta.nombre) {
        this.errorsNombre = "El nombre es obligatorio.";
      }
      if (!this.usuarioCuenta.apellido) {
        this.errorsApellido = "El apellido es obligatorio.";
      }
      if (!this.usuarioCuenta.valor) {
        this.errorsValor = "El valor es obligatorio.";
      }

      e.preventDefault();
    },

    //Método para validar campo vacío al realizar una consignación
    validacionConsignar(e) {
      if (this.movimiento.valor) {
        this.consignar();
      }
      this.errorsValor = null;
      if (!this.movimiento.valor) {
        this.errorsValor = "El valor es obligatorio.";
      }
      e.preventDefault();
    },

    //Método para validar campo vacío al realizar un retiro
    validacionRetirar(e) {
      if (this.movimiento.valor) {
        this.retirar();
      }
      this.errorsValor = null;
      if (!this.movimiento.valor) {
        this.errorsValor = "El valor es obligatorio.";
      }
      e.preventDefault();
    },

    //Método para trae todos los usuarios con su respectiva cuenta
    todosLosUsuarios() {
      try {
        this.bankService.todosLosUsuarios().then((data) => {
          this.usuarios = data.data.Data;
        });
      } catch (error) {
        console.log(error);
      }
    },

    //Método para abrir el modal de crear cuenta
    abrirModalCrear() {
      this.displayModal = true;
    },

    //Método para crear una cuenta
    crearCuenta() {
      try {
        this.bankService.crearCeuntahorros(this.usuarioCuenta).then((data) => {
          if (data.data.Estado === true) {
            this.usuarioCuenta = {
              nombre: null,
              apellido: null,
              valor: null,
            };
            this.cerrarModalCrear();
            this.todosLosUsuarios();
            this.$toast.add({
              severity: "success",
              summary: "retiro",
              detail: data.data.Msg,
              life: 5000,
            });
          } else {
            this.cerrarModalCrear();
            this.todosLosUsuarios();
            this.$toast.add({
              severity: "warn",
              summary: "retiro",
              detail: data.data.Msg,
              life: 5000,
            });
          }
        });
      } catch (error) {
        console.log(error);
      }
    },

    //Método para cerrae el modal de crear cuenta
    cerrarModalCrear() {
      this.displayModal = false;
    },

    //Método para abrir el modal de consignar
    abrirModalConsignar() {
      this.displayModalCongignar = true;

      this.movimiento = {
        nCuenta: this.seleccionarUsuario.NCuenta,
      };
    },

    //Método para consignar
    consignar() {
      try {
        this.bankService.consignar(this.movimiento).then((data) => {
          if (data.data.Estado === true) {
            this.movimiento = {
              nCuenta: null,
              valor: null,
            };
            this.cerrarModalConsignar();
            this.$toast.add({
              severity: "success",
              summary: "Consignacion",
              detail: data.data.Msg,
              life: 5000,
            });
          } else {
            this.cerrarModalConsignar();
            this.$toast.add({
              severity: "warn",
              summary: "Consignacion",
              detail: data.data.Msg,
              life: 5000,
            });
          }
        });
      } catch (error) {
        console.log(error);
      }
    },

    //Método para cerrar el modal de consignar
    cerrarModalConsignar() {
      this.displayModalCongignar = false;
    },

    //Método para abrir el modal de retirar
    abrirModalRetirar() {
      this.displayModalRetirar = true;

      this.movimiento = {
        nCuenta: this.seleccionarUsuario.NCuenta,
      };
    },

    //Método para retirar
    retirar() {
      try {
        this.bankService.retirar(this.movimiento).then((data) => {
          if (data.data.Estado === true) {
            this.movimiento = {
              nCuenta: null,
              valor: null,
            };
            this.cerrarModalRetirar();
            this.$toast.add({
              severity: "success",
              summary: "Retiro",
              detail: data.data.Msg,
              life: 5000,
            });
          } else {
            this.cerrarModalRetirar();
            this.$toast.add({
              severity: "warn",
              summary: "Retiro",
              detail: data.data.Msg,
              life: 5000,
            });
          }
        });
      } catch (error) {
        console.log(error);
      }
    },

    //Método para cerrar el modal de retirar
    cerrarModalRetirar() {
      this.displayModalRetirar = false;
    },

    //Método para consultar el saldo
    consultarSaldo() {
      try {
        if (this.seleccionarUsuario.NCuenta) {
          this.bankService
            .consultarSaldo(this.seleccionarUsuario.NCuenta)
            .then((data) => {
              if (data.data.Estado === true) {
                this.$toast.add({
                  severity: "info",
                  summary: "Saldo",
                  detail: data.data.Msg,
                  life: 5000,
                });
              } else {
                this.$toast.add({
                  severity: "warn",
                  summary: "Saldo",
                  detail: data.data.Msg,
                  life: 5000,
                });
              }
            });
        }
        if (!this.seleccionarUsuario.NCuenta) {
          this.$toast.add({
            severity: "warn",
            summary: "Saldo",
            detail: "Selecciones una cuenta",
            life: 5000,
          });
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style></style>

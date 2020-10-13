PruebaTecnica
# PruebaTecnica

## BackEnd

### a. Instrucciones de cómo ejecutar la aplicación en un ambiente local
```
Descargar el proyecto del repositorio, y abrir la solucion en visual studio(2019) y compilar.
```
### b. Supuestos de negocio y tecnología que realizó para solucionar el problema
```
Supuestos de negocio:
Basado en los requerimientos de la prueba tecnica.

tecnologías:

IDE: Microsoft Visual Studio Community 2019 Versión 16.7.5.

.NET Framework Versión 4.7.2

ASP.NET web aplication(Web api 2)

Entity Framewrok Versión 6.2.0

Log4Net Versión 2.0.11

SQL server local DB

```
### c. Explicación de la arquitectura que planteo para solucionar el problema.
```
La arquitectura planteada esta basada en servicios con una capa de entrada/salida donde se reciben las peticiones y se devuelven las respuestas y una capa de datos en la que se realizan transacciones con la BD.
```
### d. Explicación de las tecnologías seleccionadas para resolver el problema.
```
Dado a que uno de los requerimientos era trabajar sobre C# se uso Visual Studio IDE de desarrollo,
Se crearon 2 proyectos para la solución: "Bank" ASP.NET web aplicación donde se construye el api de servicios usando web api2 para crear los servicios y "Bank.Test" el de las pruebas unitarias, como mecanismo de auditoria se utilizo Log4Net ya que esta es una de las librerías mas conocidas y con mayor documentación sobre este tema, como mecanismo de persistencia de información se uso Entity Framewrok el cual se conecta a una BD SQL server local embebida dentro de la aplicación.
```
### e. Qué haría mejor o como podría atacar mejor el problema si tuviera más tiempo.
```
Utilizar tecnologías mas recientes como .Net Core y Entity Framewrok Core.
Implementar mas capas (Bibliotecas de clases) dentro de la aplicacion.
```

## FrontEnd

### a. Instrucciones de cómo ejecutar la aplicación en un ambiente local
```
Desde cmd sobre la carpeta del proyecto:

Configuración del proyecto
npm install

Compilar
npm run serve
```
### b. Supuestos de negocio y tecnología que realizó para solucionar el problema
```
Node.js Versión 12.18.3 

Vue Framework 

Vue Cli Versión  4.5.4

PrimeVue Versión 2.1.0

Primeicons Versión 4.0.0

axios Versión 0.20.0
```
### c. Explicación de la arquitectura que planteo para solucionar el problema.
```
La arquitectura cuenta con un clase de servicios "BankService.js" ubicada en el folder service que va permitir conectarse con la aplicación en .Net, una clase componente de vue "BankApp.vue" ubicada en el folder components la cuel se divide en varias partes: "template" donde se encuentra el html, "script" donde se define el javaScript y por ultimo el "style" donde se definen los CSS  y la clase "main.js" en la cual se importan todos los componentes de primevue a utilizar.
```
### d. Explicación de las tecnologías seleccionadas para resolver el problema.
```
npm es el gestor de paquetes de Node.js es muy importante tenerlo instalado para poder ejecutar comandos npm,
se crea el proyecto "Bank-app" con Vue Cli el cual nos genera un proyecto independiente mas robusto en el cual procedemos a desarrollar el FrontEnd utilizando la la biblioteca de interfaz de usuario PrimeVue la cual cuenta con unos componentes increíbles, por ultimo axios nos permite realizar las peticiones http asía el BackEnd.
```

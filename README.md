Grupo 3#
Marco Antonio Di Pane Argeñal
 Gyven Daniel Orellana Orellana
Jose Benkay Zelaya Ham 
Manuel Alejandro Linares Valenzuela
Raquel Antonia Salvador Vasquez

UBICACIONES API - GUÍA DE PRUEBA EN LOCAL
Esta API sirve para gestionar un CRUD de contactos con ubicaciones, imágenes y videos. Está desarrollada con .NET 10, Arquitectura Limpia, Entity Framework Core y MySQL. Corre en el puerto 4445 e incluye Swagger para pruebas.

1. REQUISITOS PREVIOS
Instalar los siguientes componentes antes de iniciar:

.NET SDK 10: Descargarlo del sitio oficial de Microsoft. Para verificar la instalación, abrir una terminal y ejecutar:
dotnet --version
(Debe mostrar una versión que inicie con 10)

MySQL Server 8.0: Debe estar activo en el equipo. Tomar nota del usuario (usualmente root) y la contraseña. No es necesario crear la base de datos manualmente.

Opcional: Visual Studio 2022/2026 con la carga de trabajo de ASP.NET. Si no se tiene, se puede utilizar la terminal.

2. CONFIGURACIÓN DE LA BASE DE DATOS (OBLIGATORIO)
El archivo appsettings.json dentro de Ubicaciones.Api contiene la cadena de conexión con la contraseña vacía por seguridad. Hay dos opciones para configurar la contraseña propia:

Opción A (Modificar archivo): Abrir appsettings.json y colocar la contraseña en el campo Password:
"MySql": "Server=localhost;Port=3306;Database=ubicaciones_bd;User=root;Password=TU_CONTRASEÑA"

Opción B (User Secrets - Recomendado para no alterar el repositorio): Abrir una terminal en la carpeta Ubicaciones.Api y ejecutar:
dotnet user-secrets set "ConnectionStrings:MySql" "Server=localhost;Port=3306;Database=ubicaciones_bd;User=root;Password=TU_PASSWORD"

Nota: La base de datos y las tablas se crean de forma automática al iniciar la aplicación gracias a Entity Framework Core.

3. EJECUCIÓN DE LA API
Opción A (Visual Studio):

Abrir el archivo UbicacionesApi.sln.

Establecer el proyecto Ubicaciones.Api como proyecto de inicio.

Presionar F5 o el botón de iniciar. El navegador abrirá Swagger automáticamente.

Opción B (Terminal):
Abrir una terminal en la raíz del proyecto y ejecutar:
cd Ubicaciones.Api
dotnet run
La API estará activa cuando la terminal indique: Now listening on: http://0.0.0.0:4445

4. PRUEBA DE ENDPOINTS (SWAGGER)
Con la API en ejecución, ingresar en el navegador a: http://localhost:4445/swagger

Principales acciones disponibles mediante el botón "Try it out":

GET /api/contactos : Lista los contactos (permite filtrar usando ?buscar=nombre).

GET /api/contactos/{id} : Obtiene un contacto específico por ID.

POST /api/contactos : Crea un contacto.

PUT /api/contactos/{id} : Actualiza un contacto.

DELETE /api/contactos/{id} : Elimina un contacto.

Estructura JSON para crear o editar un contacto (POST / PUT):

{
"nombre": "Jose Escalante",
"telefono": "99887766",
"latitud": 15.50,
"longitud": -88.00,
"imagenBase64": null,
"videoRuta": null
}

Reglas de validación (de lo contrario, devuelve error 400):

nombre: Obligatorio, solo letras y espacios.

telefono: Obligatorio, exactamente 8 dígitos.

latitud: Obligatoria, entre -90 y 90.

longitud: Obligatoria, entre -180 y 180.

imagenBase64 y videoRuta: Opcionales.

5. CONEXIÓN CON LA APP ANDROID
Para conectar la aplicación de Android a esta API local, modificar la variable URL_BASE en el archivo datos/ClienteApi.kt de Android según el caso:

Si se usa el emulador de Android Studio: http://10.0.2.2:4445/

Si se usa un dispositivo físico (mismo WiFi que la PC): http://IP_LOCAL_DE_LA_PC:4445/

Nota para dispositivo físico: Se debe abrir el puerto en el Firewall de Windows ejecutando este comando como administrador:
netsh advfirewall firewall add rule name="API 4445" dir=in action=allow protocol=TCP localport=4445

6. RESOLUCIÓN DE PROBLEMAS COMUNES
Error 'Access denied for user root': La contraseña de MySQL configurada en el paso 2 no es correcta.

Error 'Unable to connect to host': El servicio de MySQL está apagado o el puerto no es el 3306.

Swagger no carga: Verificar que se esté ingresando mediante http y no https (http://localhost:4445/swagger).

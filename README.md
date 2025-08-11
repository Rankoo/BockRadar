# BookRadar 📚
Aplicación ASP.NET Core MVC (.NET 8) que permite buscar libros por autor usando la API pública de Open Library, mostrando resultados en una tabla y almacenando el historial de búsquedas en SQL Server mediante Stored Procedures.
## 🚀 Características
- **Búsqueda en Open Library API** por nombre de autor.
- **Prevención de búsquedas duplicadas** (caché de 1 minuto).
- **Resultados ordenados** por año de publicación.
- **Persistencia en SQL Server usando Entity Framework Core** + Stored Procedures.
- **Índice** en la columna `Autor` para mejorar consultas.
- **Historial de búsquedas** consultado desde SP.
- **Interfaz con Bootstrap 5 y paginación con DataTables.js.**
- **Validaciones en frontend** para evitar envíos vacíos.

## 🛠 Tecnologías utilizadas
- **Backend:** .NET 8, ASP.NET Core MVC, C#, Entity Framework Core.
- **Frontend:** Razor Views, Bootstrap 5, DataTables.js, jQuery.
- **Base de datos:** SQL Server, Stored Procedures, índices.
- **Infraestructura:** IHttpClientFactory, IMemoryCache, Repository Pattern.

## 📦 Instalación y ejecución
### 1️⃣ Requisitos previos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/es/) o VS Code.

### 2️⃣ Clonar repositorio
```
git clone https://github.com/Rankoo/BookRadar
cd BookRadar
```
### 3️⃣ Configurar base de datos
1. Crear la base de datos en SQL Server:
```
CREATE DATABASE BookRadarDB;
```
2. Ejecutar el script `database.sql` incluido en el repositorio, que contiene:
	* Creación de tabla `HistorialBusquedas`
	* Índice en `Autor`
    * Stored Procedures `sp_GuardarBusqueda` y `sp_ObtenerHistorial`
### 4️⃣ Configurar variables de entorno
En la raíz del proyecto, crear un archivo `.env` con:
```
DB_CONNECTION_STRING=Server=localhost;Database=BookRadarDB;Trusted_Connection=True;TrustServerCertificate=True;
```
### 5️⃣ Restaurar dependencias y ejecutar
```
dotnet restore
dotnet run
```
La aplicación estará disponible en:
[http://localhost:5000](http://localhost:5000) o [https://localhost:7000](https://localhost:7000)
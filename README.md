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



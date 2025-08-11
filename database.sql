-- =============================================
-- Crear base de datos si no existe
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BookRadarDB')
BEGIN
    CREATE DATABASE BookRadarDB;
END
GO

USE BookRadarDB;
GO

-- =============================================
-- Crear tabla HistorialBusquedas
-- =============================================
IF OBJECT_ID('dbo.HistorialBusquedas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.HistorialBusquedas (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Autor NVARCHAR(255) NOT NULL,
        Titulo NVARCHAR(255) NOT NULL,
        AnioPublicacion INT NULL,
        Editorial NVARCHAR(255) NULL,
        FechaConsulta DATETIME NOT NULL
    );
END
GO

-- =============================================
-- Crear índice en Autor para búsquedas rápidas
-- =============================================
IF NOT EXISTS (
    SELECT name 
    FROM sys.indexes 
    WHERE name = 'IX_HistorialBusquedas_Autor'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_HistorialBusquedas_Autor
    ON dbo.HistorialBusquedas (Autor);
END
GO

-- =============================================
-- Stored Procedure para guardar búsqueda
-- =============================================
IF OBJECT_ID('dbo.sp_GuardarBusqueda', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GuardarBusqueda;
GO

CREATE PROCEDURE dbo.sp_GuardarBusqueda
    @Autor NVARCHAR(255),
    @Titulo NVARCHAR(255),
    @AnioPublicacion INT = NULL,
    @Editorial NVARCHAR(255) = NULL,
    @FechaConsulta DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.HistorialBusquedas (Autor, Titulo, AnioPublicacion, Editorial, FechaConsulta)
    VALUES (@Autor, @Titulo, @AnioPublicacion, @Editorial, @FechaConsulta);
END
GO

-- =============================================
-- Stored Procedure para obtener historial
-- =============================================
IF OBJECT_ID('dbo.sp_ObtenerHistorial', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ObtenerHistorial;
GO

CREATE PROCEDURE dbo.sp_ObtenerHistorial
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Autor, Titulo, AnioPublicacion, Editorial, FechaConsulta
    FROM dbo.HistorialBusquedas
    ORDER BY FechaConsulta DESC;
END
GO

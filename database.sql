CREATE DATABASE DbPrestamos;
GO

USE DbPrestamos;
GO

/* =========================================
   TABLAS
========================================= */

CREATE TABLE TasaEdad
(
    IdTasa INT IDENTITY(1,1) PRIMARY KEY,
    Edad INT NOT NULL,
    Tasa DECIMAL(5,2) NOT NULL
);
GO

ALTER TABLE TasaEdad
ADD CONSTRAINT UQ_TasaEdad UNIQUE (Edad);
GO

CREATE TABLE PlazoPrestamo
(
    IdPlazo INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL,
    ValorMeses INT NOT NULL
);
GO

ALTER TABLE PlazoPrestamo
ADD CONSTRAINT UQ_PlazoPrestamo UNIQUE (ValorMeses);
GO

CREATE TABLE LogConsultaPrestamo
(
    IdConsulta INT IDENTITY(1,1) PRIMARY KEY,
    FechaConsulta DATETIME NOT NULL DEFAULT GETDATE(),
    FechaNacimiento DATE NOT NULL,
    Edad INT NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    Meses INT NOT NULL,
    Tasa DECIMAL(5,2) NULL,
    ValorCuota DECIMAL(18,2) NULL,
    MensajeResultado VARCHAR(255) NULL,
    IpConsulta VARCHAR(50) NULL
);
GO

/* =========================================
   DATOS INICIALES
========================================= */

INSERT INTO TasaEdad (Edad, Tasa)
VALUES
(18, 1.20),
(19, 1.18),
(20, 1.16),
(21, 1.14),
(22, 1.12),
(23, 1.10),
(24, 1.08),
(25, 1.05);
GO

INSERT INTO PlazoPrestamo (Descripcion, ValorMeses)
VALUES
('3 Meses', 3),
('6 Meses', 6),
('9 Meses', 9),
('12 Meses', 12);
GO

/* =========================================
   STORED PROCEDURES
========================================= */

CREATE PROCEDURE sp_ObtenerTasaPorEdad
    @Edad INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        IdTasa,
        Edad,
        Tasa
    FROM TasaEdad
    WHERE Edad = @Edad;
END
GO

CREATE PROCEDURE sp_ObtenerPlazoPorMeses
    @Meses INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        IdPlazo,
        Descripcion,
        ValorMeses
    FROM PlazoPrestamo
    WHERE ValorMeses = @Meses;
END
GO

CREATE PROCEDURE sp_InsertarLogConsultaPrestamo
    @FechaNacimiento DATE,
    @Edad INT,
    @Monto DECIMAL(18,2),
    @Meses INT,
    @Tasa DECIMAL(5,2) = NULL,
    @ValorCuota DECIMAL(18,2) = NULL,
    @MensajeResultado VARCHAR(255) = NULL,
    @IpConsulta VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO LogConsultaPrestamo
    (
        FechaNacimiento,
        Edad,
        Monto,
        Meses,
        Tasa,
        ValorCuota,
        MensajeResultado,
        IpConsulta
    )
    VALUES
    (
        @FechaNacimiento,
        @Edad,
        @Monto,
        @Meses,
        @Tasa,
        @ValorCuota,
        @MensajeResultado,
        @IpConsulta
    );
END
GO

/* =========================================
   PRUEBAS RÁPIDAS
========================================= */

EXEC sp_ObtenerTasaPorEdad @Edad = 23;
GO

EXEC sp_ObtenerPlazoPorMeses @Meses = 6;
GO

EXEC sp_InsertarLogConsultaPrestamo
    @FechaNacimiento = '2002-05-10',
    @Edad = 23,
    @Monto = 50000,
    @Meses = 6,
    @Tasa = 1.10,
    @ValorCuota = 9166.67,
    @MensajeResultado = 'Cálculo realizado correctamente.',
    @IpConsulta = '127.0.0.1';
GO

SELECT * FROM LogConsultaPrestamo;
GO

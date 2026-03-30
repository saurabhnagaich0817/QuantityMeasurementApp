-- Naya database simple naam se
CREATE DATABASE QuantityMeasurementDBB;
GO

USE QuantityMeasurementDBB;
GO

-- Main table with proper schema (NULL allowed for optional fields)
CREATE TABLE QuantityMeasurements (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OperationType NVARCHAR(50) NOT NULL,
    MeasurementType NVARCHAR(50) NOT NULL,
    FromValue DECIMAL(18,6) NOT NULL,
    FromUnit NVARCHAR(50) NOT NULL,
    ToValue DECIMAL(18,6) NOT NULL,
    ToUnit NVARCHAR(50) NOT NULL,
    Result DECIMAL(18,6) NOT NULL,
    ResultUnit NVARCHAR(50) NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE()
);
GO

-- Indexes for performance
CREATE INDEX IX_OperationType ON QuantityMeasurements(OperationType);
CREATE INDEX IX_MeasurementType ON QuantityMeasurements(MeasurementType);
CREATE INDEX IX_CreatedAt ON QuantityMeasurements(CreatedAt);
GO

-- Sample data insert
INSERT INTO QuantityMeasurements 
    (OperationType, MeasurementType, FromValue, FromUnit, ToValue, ToUnit, Result, ResultUnit)
VALUES 
    ('Compare', 'Length', 1, 'Feet', 12, 'Inches', 1, 'Boolean'),
    ('Compare', 'Weight', 1, 'Kilogram', 1000, 'Gram', 1, 'Boolean'),
    ('Convert', 'Length', 1, 'Feet', 12, 'Inches', 12, 'Inches');
GO

-- Check data
SELECT * FROM QuantityMeasurements;
GO

USE QuantityMeasurementDBB;
SELECT * FROM QuantityMeasurements ORDER BY CreatedAt DESC;





USE QuantityMeasurementDB;
GO

-- Create stored procedure for inserting measurements
CREATE PROCEDURE sp_InsertMeasurement
    @OperationTypeName NVARCHAR(50),
    @MeasurementTypeName NVARCHAR(50),
    @FromValue DECIMAL(18,6),
    @FromUnitName NVARCHAR(50),
    @ToValue DECIMAL(18,6),
    @ToUnitName NVARCHAR(50),
    @Result DECIMAL(18,6),
    @ResultUnitName NVARCHAR(50),
    @UserId NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Insert directly into table (simplified version)
        INSERT INTO QuantityMeasurements (
            OperationType,
            MeasurementType,
            FromValue,
            FromUnit,
            ToValue,
            ToUnit,
            Result,
            ResultUnit,
            CreatedAt
        )
        VALUES (
            @OperationTypeName,
            @MeasurementTypeName,
            @FromValue,
            @FromUnitName,
            @ToValue,
            @ToUnitName,
            @Result,
            @ResultUnitName,
            GETDATE()
        );
        
        SELECT SCOPE_IDENTITY() AS Id, 'Success' AS Status;
    END TRY
    BEGIN CATCH
        SELECT 
            0 AS Id,
            'Error' AS Status,
            ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END;
GO

-- Test the stored procedure
EXEC sp_InsertMeasurement 
    @OperationTypeName = 'Compare',
    @MeasurementTypeName = 'Length',
    @FromValue = 1,
    @FromUnitName = 'Feet',
    @ToValue = 12,
    @ToUnitName = 'Inches',
    @Result = 1,
    @ResultUnitName = 'Boolean';
GO

-- Check if procedure created
SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertMeasurement';
GO


USE QuantityMeasurementDBB;
SELECT * FROM QuantityMeasurements;

USE QuantityMeasurementDBB;
SELECT * FROM QuantityMeasurements WHERE Id = 6;





USE QuantityMeasurementDBB;
GO

-- Create OperationTypes table
CREATE TABLE OperationTypes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    CreatedAt DATETIME2 DEFAULT GETDATE()
);
GO

-- Insert Operation Types
INSERT INTO OperationTypes (Name, Description) VALUES
('Compare', 'Compare two quantities for equality'),
('Convert', 'Convert quantity to different unit'),
('Add', 'Add two quantities'),
('Subtract', 'Subtract one quantity from another'),
('Divide', 'Divide one quantity by another');
GO

-- Create MeasurementTypes table
CREATE TABLE MeasurementTypes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    BaseUnit NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    CreatedAt DATETIME2 DEFAULT GETDATE()
);
GO

-- Insert Measurement Types
INSERT INTO MeasurementTypes (Name, BaseUnit, Description) VALUES
('Length', 'Inches', 'Length measurements in various units'),
('Weight', 'Kilograms', 'Weight/mass measurements'),
('Volume', 'Liters', 'Volume measurements'),
('Temperature', 'Celsius', 'Temperature measurements');
GO

-- Create Units table
CREATE TABLE Units (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MeasurementTypeId INT NOT NULL,
    Name NVARCHAR(50) NOT NULL,
    Symbol NVARCHAR(10) NOT NULL,
    ConversionFactor DECIMAL(18,6) NOT NULL,
    IsBaseUnit BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    
    CONSTRAINT FK_Units_MeasurementType FOREIGN KEY (MeasurementTypeId) 
        REFERENCES MeasurementTypes(Id),
    CONSTRAINT UQ_Units_Name_MeasurementType UNIQUE (MeasurementTypeId, Name)
);
GO

-- Insert Units for Length (MeasurementTypeId = 1)
INSERT INTO Units (MeasurementTypeId, Name, Symbol, ConversionFactor, IsBaseUnit) VALUES
(1, 'Inches', 'in', 1.0, 1),
(1, 'Feet', 'ft', 12.0, 0),
(1, 'Yards', 'yd', 36.0, 0),
(1, 'Centimeters', 'cm', 0.393701, 0),
(1, 'Meters', 'm', 39.3701, 0),
(1, 'Kilometers', 'km', 39370.1, 0);
GO

-- Insert Units for Weight (MeasurementTypeId = 2)
INSERT INTO Units (MeasurementTypeId, Name, Symbol, ConversionFactor, IsBaseUnit) VALUES
(2, 'Grams', 'g', 0.001, 0),
(2, 'Kilograms', 'kg', 1.0, 1),
(2, 'Pounds', 'lb', 0.453592, 0),
(2, 'Ounces', 'oz', 0.0283495, 0),
(2, 'Tons', 't', 1000, 0);
GO

-- Insert Units for Volume (MeasurementTypeId = 3)
INSERT INTO Units (MeasurementTypeId, Name, Symbol, ConversionFactor, IsBaseUnit) VALUES
(3, 'Milliliters', 'ml', 0.001, 0),
(3, 'Liters', 'L', 1.0, 1),
(3, 'Gallons', 'gal', 3.78541, 0),
(3, 'Cubic Meters', 'm³', 1000, 0);
GO

-- Insert Units for Temperature (MeasurementTypeId = 4)
INSERT INTO Units (MeasurementTypeId, Name, Symbol, ConversionFactor, IsBaseUnit) VALUES
(4, 'Celsius', '°C', 1.0, 1),
(4, 'Fahrenheit', '°F', 1.0, 0),
(4, 'Kelvin', 'K', 1.0, 0);
GO

-- Recreate stored procedure (updated version)
CREATE OR ALTER PROCEDURE sp_InsertMeasurement
    @OperationTypeName NVARCHAR(50),
    @MeasurementTypeName NVARCHAR(50),
    @FromValue DECIMAL(18,6),
    @FromUnitName NVARCHAR(50),
    @ToValue DECIMAL(18,6),
    @ToUnitName NVARCHAR(50),
    @Result DECIMAL(18,6),
    @ResultUnitName NVARCHAR(50),
    @UserId NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Insert directly into table (simplified version)
        INSERT INTO QuantityMeasurements (
            OperationType,
            MeasurementType,
            FromValue,
            FromUnit,
            ToValue,
            ToUnit,
            Result,
            ResultUnit,
            CreatedAt
        )
        VALUES (
            @OperationTypeName,
            @MeasurementTypeName,
            @FromValue,
            @FromUnitName,
            @ToValue,
            @ToUnitName,
            @Result,
            @ResultUnitName,
            GETDATE()
        );
        
        SELECT SCOPE_IDENTITY() AS Id, 'Success' AS Status;
    END TRY
    BEGIN CATCH
        SELECT 
            0 AS Id,
            'Error' AS Status,
            ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END;
GO

SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE';


USE QuantityMeasurementDBB;

-- Add missing columns
ALTER TABLE QuantityMeasurements ADD 
    OperationTypeId INT NULL,
    MeasurementTypeId INT NULL,
    FromUnitId INT NULL,
    ToUnitId INT NULL,
    ResultUnitId INT NULL;

-- Update existing records
UPDATE QuantityMeasurements SET 
    OperationTypeId = CASE OperationType
        WHEN 'Compare' THEN 1
        WHEN 'Convert' THEN 2
        WHEN 'Add' THEN 3
        WHEN 'Subtract' THEN 4
        WHEN 'Divide' THEN 5
    END,
    FromUnitId = 2, -- Feet
    ToUnitId = 1,   -- Inches
    ResultUnitId = CASE ResultUnit
        WHEN 'Boolean' THEN NULL
        WHEN 'Feet' THEN 2
        WHEN 'Inches' THEN 1
        ELSE NULL
    END;


-- =====================================================
-- UC16: Complete Database Schema for Quantity Measurement
-- Database: QuantityMeasurementDB_UC16
-- =====================================================

-- Create Database
CREATE DATABASE QuantityMeasurementDB;
GO

USE QuantityMeasurementDB;
GO

-- =====================================================
-- 1. ENUMS TABLES (Lookup tables)
-- =====================================================

-- Operation Types lookup
CREATE TABLE OperationTypes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    CreatedAt DATETIME2 DEFAULT GETDATE()
);
GO

-- Measurement Types lookup
CREATE TABLE MeasurementTypes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    BaseUnit NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    CreatedAt DATETIME2 DEFAULT GETDATE()
);
GO

-- Units lookup
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

-- =====================================================
-- 2. MAIN TABLES
-- =====================================================

-- Main measurements table
CREATE TABLE QuantityMeasurements (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OperationTypeId INT NOT NULL,
    MeasurementTypeId INT NOT NULL,
    
    -- Source values
    FromValue DECIMAL(18,6) NOT NULL,
    FromUnitId INT NOT NULL,
    FromUnitName NVARCHAR(50) NOT NULL,
    
    -- Target values
    ToValue DECIMAL(18,6) NOT NULL,
    ToUnitId INT NOT NULL,
    ToUnitName NVARCHAR(50) NOT NULL,
    
    -- Result
    Result DECIMAL(18,6) NOT NULL,
    ResultUnitId INT NOT NULL,
    ResultUnitName NVARCHAR(50) NOT NULL,
    
    -- Metadata
    SessionId UNIQUEIDENTIFIER DEFAULT NEWID(),
    UserId NVARCHAR(100) NULL,
    IsSuccessful BIT DEFAULT 1,
    ErrorMessage NVARCHAR(500) NULL,
    
    -- Timestamps
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,
    
    -- Constraints
    CONSTRAINT FK_Measurements_OperationType FOREIGN KEY (OperationTypeId) 
        REFERENCES OperationTypes(Id),
    CONSTRAINT FK_Measurements_MeasurementType FOREIGN KEY (MeasurementTypeId) 
        REFERENCES MeasurementTypes(Id),
    CONSTRAINT FK_Measurements_FromUnit FOREIGN KEY (FromUnitId) 
        REFERENCES Units(Id),
    CONSTRAINT FK_Measurements_ToUnit FOREIGN KEY (ToUnitId) 
        REFERENCES Units(Id),
    CONSTRAINT FK_Measurements_ResultUnit FOREIGN KEY (ResultUnitId) 
        REFERENCES Units(Id)
);
GO

-- Audit/History table
CREATE TABLE MeasurementAudit (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MeasurementId INT NOT NULL,
    ActionType NVARCHAR(20) NOT NULL,
    OldValues NVARCHAR(MAX) NULL,
    NewValues NVARCHAR(MAX) NULL,
    ChangedBy NVARCHAR(100) NULL,
    ChangedAt DATETIME2 DEFAULT GETDATE(),
    
    CONSTRAINT FK_Audit_Measurement FOREIGN KEY (MeasurementId) 
        REFERENCES QuantityMeasurements(Id)
);
GO

-- =====================================================
-- 3. INDEXES for performance
-- =====================================================

CREATE NONCLUSTERED INDEX IX_Measurements_OperationTypeId 
    ON QuantityMeasurements(OperationTypeId) 
    INCLUDE (FromValue, ToValue, Result, CreatedAt);
GO

CREATE NONCLUSTERED INDEX IX_Measurements_MeasurementTypeId 
    ON QuantityMeasurements(MeasurementTypeId) 
    INCLUDE (FromValue, ToValue, Result, CreatedAt);
GO

CREATE NONCLUSTERED INDEX IX_Measurements_CreatedAt 
    ON QuantityMeasurements(CreatedAt DESC);
GO

-- =====================================================
-- 4. STORED PROCEDURES
-- =====================================================

-- Insert new measurement
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
    SET XACT_ABORT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Get or create Operation Type
        DECLARE @OperationTypeId INT;
        SELECT @OperationTypeId = Id FROM OperationTypes WHERE Name = @OperationTypeName;
        
        IF @OperationTypeId IS NULL
        BEGIN
            INSERT INTO OperationTypes (Name) VALUES (@OperationTypeName);
            SET @OperationTypeId = SCOPE_IDENTITY();
        END
        
        -- Get or create Measurement Type
        DECLARE @MeasurementTypeId INT;
        SELECT @MeasurementTypeId = Id FROM MeasurementTypes WHERE Name = @MeasurementTypeName;
        
        IF @MeasurementTypeId IS NULL
        BEGIN
            INSERT INTO MeasurementTypes (Name, BaseUnit) 
            VALUES (@MeasurementTypeName, @ResultUnitName);
            SET @MeasurementTypeId = SCOPE_IDENTITY();
        END
        
        -- Get Unit IDs
        DECLARE @FromUnitId INT, @ToUnitId INT, @ResultUnitId INT;
        
        SELECT @FromUnitId = Id FROM Units WHERE Name = @FromUnitName AND MeasurementTypeId = @MeasurementTypeId;
        SELECT @ToUnitId = Id FROM Units WHERE Name = @ToUnitName AND MeasurementTypeId = @MeasurementTypeId;
        SELECT @ResultUnitId = Id FROM Units WHERE Name = @ResultUnitName AND MeasurementTypeId = @MeasurementTypeId;
        
        -- Insert measurement
        INSERT INTO QuantityMeasurements (
            OperationTypeId, MeasurementTypeId,
            FromValue, FromUnitId, FromUnitName,
            ToValue, ToUnitId, ToUnitName,
            Result, ResultUnitId, ResultUnitName,
            UserId
        )
        VALUES (
            @OperationTypeId, @MeasurementTypeId,
            @FromValue, @FromUnitId, @FromUnitName,
            @ToValue, @ToUnitId, @ToUnitName,
            @Result, @ResultUnitId, @ResultUnitName,
            @UserId
        );
        
        DECLARE @NewId INT = SCOPE_IDENTITY();
        
        -- Audit log
        INSERT INTO MeasurementAudit (MeasurementId, ActionType, NewValues, ChangedBy)
        VALUES (@NewId, 'INSERT', 
            CONCAT('From:', @FromValue, ' ', @FromUnitName, ' To:', @ToValue, ' ', @ToUnitName, ' Result:', @Result, ' ', @ResultUnitName),
            @UserId);
        
        COMMIT TRANSACTION;
        
        SELECT @NewId AS Id, 'Success' AS Status;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        THROW;
    END CATCH
END;
GO

-- Get measurements with pagination
CREATE PROCEDURE sp_GetMeasurements
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @MeasurementType NVARCHAR(50) = NULL,
    @OperationType NVARCHAR(50) = NULL,
    @FromDate DATETIME2 = NULL,
    @ToDate DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    
    SELECT 
        qm.Id,
        ot.Name AS OperationType,
        mt.Name AS MeasurementType,
        qm.FromValue,
        qm.FromUnitName AS FromUnit,
        qm.ToValue,
        qm.ToUnitName AS ToUnit,
        qm.Result,
        qm.ResultUnitName AS ResultUnit,
        qm.CreatedAt,
        qm.UserId
    FROM QuantityMeasurements qm
    INNER JOIN OperationTypes ot ON qm.OperationTypeId = ot.Id
    INNER JOIN MeasurementTypes mt ON qm.MeasurementTypeId = mt.Id
    WHERE (@MeasurementType IS NULL OR mt.Name = @MeasurementType)
        AND (@OperationType IS NULL OR ot.Name = @OperationType)
        AND (@FromDate IS NULL OR qm.CreatedAt >= @FromDate)
        AND (@ToDate IS NULL OR qm.CreatedAt <= @ToDate)
    ORDER BY qm.CreatedAt DESC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    
    -- Return total count
    SELECT COUNT(*) AS TotalCount
    FROM QuantityMeasurements qm
    INNER JOIN OperationTypes ot ON qm.OperationTypeId = ot.Id
    INNER JOIN MeasurementTypes mt ON qm.MeasurementTypeId = mt.Id
    WHERE (@MeasurementType IS NULL OR mt.Name = @MeasurementType)
        AND (@OperationType IS NULL OR ot.Name = @OperationType)
        AND (@FromDate IS NULL OR qm.CreatedAt >= @FromDate)
        AND (@ToDate IS NULL OR qm.CreatedAt <= @ToDate);
END;
GO

-- Get statistics
CREATE PROCEDURE sp_GetStatistics
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Total counts by operation
    SELECT 
        ot.Name AS OperationType,
        COUNT(*) AS Count,
        AVG(qm.Result) AS AvgResult,
        MIN(qm.CreatedAt) AS FirstOccurrence,
        MAX(qm.CreatedAt) AS LastOccurrence
    FROM QuantityMeasurements qm
    INNER JOIN OperationTypes ot ON qm.OperationTypeId = ot.Id
    GROUP BY ot.Name;
    
    -- Total counts by measurement type
    SELECT 
        mt.Name AS MeasurementType,
        COUNT(*) AS Count,
        AVG(qm.Result) AS AvgResult
    FROM QuantityMeasurements qm
    INNER JOIN MeasurementTypes mt ON qm.MeasurementTypeId = mt.Id
    GROUP BY mt.Name;
END;
GO

-- =====================================================
-- 5. VIEWS
-- =====================================================

CREATE VIEW vw_RecentMeasurements AS
SELECT TOP 100
    qm.Id,
    ot.Name AS OperationType,
    mt.Name AS MeasurementType,
    qm.FromValue,
    qm.FromUnitName AS FromUnit,
    qm.ToValue,
    qm.ToUnitName AS ToUnit,
    qm.Result,
    qm.ResultUnitName AS ResultUnit,
    qm.CreatedAt
FROM QuantityMeasurements qm
INNER JOIN OperationTypes ot ON qm.OperationTypeId = ot.Id
INNER JOIN MeasurementTypes mt ON qm.MeasurementTypeId = mt.Id
ORDER BY qm.CreatedAt DESC;
GO

-- =====================================================
-- 6. FUNCTIONS
-- =====================================================

CREATE FUNCTION fn_GetCountByDateRange
(
    @StartDate DATETIME2,
    @EndDate DATETIME2
)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    
    SELECT @Count = COUNT(*)
    FROM QuantityMeasurements
    WHERE CreatedAt BETWEEN @StartDate AND @EndDate;
    
    RETURN @Count;
END;
GO

-- =====================================================
-- 7. INSERT MASTER DATA
-- =====================================================

-- Insert Operation Types
INSERT INTO OperationTypes (Name, Description) VALUES
('Compare', 'Compare two quantities for equality'),
('Add', 'Add two quantities'),
('Subtract', 'Subtract one quantity from another'),
('Divide', 'Divide one quantity by another'),
('Convert', 'Convert quantity to different unit');
GO

-- Insert Measurement Types
INSERT INTO MeasurementTypes (Name, BaseUnit, Description) VALUES
('Length', 'Inches', 'Length measurements in various units'),
('Weight', 'Kilograms', 'Weight/mass measurements'),
('Volume', 'Liters', 'Volume measurements'),
('Temperature', 'Celsius', 'Temperature measurements');
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

-- =====================================================
-- 8. SAMPLE DATA
-- =====================================================

INSERT INTO QuantityMeasurements (
    OperationTypeId, MeasurementTypeId,
    FromValue, FromUnitId, FromUnitName,
    ToValue, ToUnitId, ToUnitName,
    Result, ResultUnitId, ResultUnitName,
    CreatedAt
)
VALUES 
(1, 1, 1, 2, 'Feet', 12, 1, 'Inches', 1, 1, 'Inches', DATEADD(day, -1, GETDATE())),
(2, 2, 1, 8, 'Kilograms', 1000, 5, 'Grams', 2000, 8, 'Kilograms', DATEADD(day, -2, GETDATE())),
(3, 3, 5, 11, 'Liters', 2, 9, 'Milliliters', 3000, 11, 'Liters', DATEADD(day, -3, GETDATE()));
GO

-- =====================================================
-- 9. VERIFY DATA
-- =====================================================

SELECT * FROM vw_RecentMeasurements;
GO

PRINT '========================================';
PRINT '✅ UC16 Database created successfully!';
PRINT 'Database: QuantityMeasurementDB_UC16';
PRINT '========================================';
GO
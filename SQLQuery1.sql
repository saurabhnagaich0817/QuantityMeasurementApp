CREATE DATABASE QuantityMeasurementDB;
GO
USE QuantityMeasurementDB;
GO
CREATE TABLE QuantityMeasurements
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Value1 FLOAT NOT NULL,
    Unit1 VARCHAR(50) NOT NULL,

    Value2 FLOAT NOT NULL,
    Unit2 VARCHAR(50) NOT NULL,

    OperationType VARCHAR(50) NOT NULL,
    MeasurementType VARCHAR(50) NOT NULL,

    Result FLOAT NOT NULL,

    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE QuantityMeasurementHistory
(
    HistoryId INT IDENTITY(1,1) PRIMARY KEY,

    MeasurementId INT,
    ActionType VARCHAR(20),

    ActionDate DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (MeasurementId)
    REFERENCES QuantityMeasurements(Id)
);

CREATE INDEX IDX_MeasurementType
ON QuantityMeasurements(MeasurementType);


CREATE PROCEDURE sp_InsertMeasurement
    @Value1 FLOAT,
    @Unit1 VARCHAR(50),
    @Value2 FLOAT,
    @Unit2 VARCHAR(50),
    @OperationType VARCHAR(50),
    @MeasurementType VARCHAR(50),
    @Result FLOAT
AS
BEGIN
    INSERT INTO QuantityMeasurements
    (
        Value1,
        Unit1,
        Value2,
        Unit2,
        OperationType,
        MeasurementType,
        Result
    )
    VALUES
    (
        @Value1,
        @Unit1,
        @Value2,
        @Unit2,
        @OperationType,
        @MeasurementType,
        @Result
    );
END

CREATE PROCEDURE sp_GetAllMeasurements
AS
BEGIN
    SELECT * FROM QuantityMeasurements
END



CREATE PROCEDURE sp_GetMeasurementById
    @Id INT
AS
BEGIN
    SELECT * FROM QuantityMeasurements
    WHERE Id = @Id
END

CREATE TRIGGER trg_InsertMeasurementHistory
ON QuantityMeasurements
AFTER INSERT
AS
BEGIN
    INSERT INTO QuantityMeasurementHistory
    (
        MeasurementId,
        ActionType
    )
    SELECT Id,'INSERT'
    FROM inserted;
END


SELECT * FROM QuantityMeasurements

CREATE PROCEDURE sp_SaveResult
    @Value1 FLOAT,
    @Value2 FLOAT,
    @Unit VARCHAR(50),
    @Result FLOAT
AS
BEGIN
    INSERT INTO QuantityMeasurements
    (
        Value1,
        Unit1,
        Value2,
        Unit2,
        OperationType,
        MeasurementType,
        Result
    )
    VALUES
    (
        @Value1,
        @Unit,
        @Value2,
        @Unit,
        'ADD',
        'Length',
        @Result
    )
END

SELECT * FROM QuantityMeasurements

SELECT * FROM QuantityMeasurementHistory


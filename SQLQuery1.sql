-- Auth Service ke liye database
CREATE DATABASE AuthServiceDB;
GO

-- User Service ke liye database  
CREATE DATABASE UserServiceDB;
GO

-- Quantity Service ke liye database
CREATE DATABASE QuantityServiceDB;
GO

-- History Service ke liye database
CREATE DATABASE HistoryServiceDB;
GO

-- Databases check karo
SELECT name FROM sys.databases;

USE AuthServiceDB;
SELECT * FROM sys.tables WHERE name = 'Users';
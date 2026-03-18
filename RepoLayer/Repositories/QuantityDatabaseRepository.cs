using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ModelLayer.Models;
using ModelLayer.Exceptions;
using RepoLayer.Interfaces;

namespace RepoLayer.Repositories
{
    public class QuantityDatabaseRepository : IQuantityRepository
    {
        private readonly string _connectionString;
        
        public QuantityDatabaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public Quantity<T> Save<T>(Quantity<T> quantity) where T : struct, Enum
        {
            return quantity;
        }
        
        public QuantityMeasurementEntity SaveToDatabase(QuantityMeasurementEntity entity)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    
                    // Using direct INSERT instead of stored procedure
                    string query = @"
                        INSERT INTO QuantityMeasurements 
                        (OperationType, MeasurementType, FromValue, FromUnit, ToValue, ToUnit, Result, ResultUnit, CreatedAt)
                        VALUES 
                        (@OperationType, @MeasurementType, @FromValue, @FromUnit, @ToValue, @ToUnit, @Result, @ResultUnit, @CreatedAt);
                        
                        SELECT SCOPE_IDENTITY();";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OperationType", entity.OperationType ?? "");
                        cmd.Parameters.AddWithValue("@MeasurementType", entity.MeasurementType ?? "");
                        cmd.Parameters.AddWithValue("@FromValue", entity.FromValue);
                        cmd.Parameters.AddWithValue("@FromUnit", entity.FromUnit ?? "");
                        cmd.Parameters.AddWithValue("@ToValue", entity.ToValue);
                        cmd.Parameters.AddWithValue("@ToUnit", entity.ToUnit ?? "");
                        cmd.Parameters.AddWithValue("@Result", entity.Result);
                        cmd.Parameters.AddWithValue("@ResultUnit", entity.ResultUnit ?? "");
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        
                        int newId = Convert.ToInt32(cmd.ExecuteScalar());
                        entity.Id = newId;
                        
                        return entity;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException($"Database error: {ex.Message}", ex)
                {
                    ErrorCode = ex.Number
                };
            }
        }
        
        public List<QuantityMeasurementEntity> GetAllFromDatabase()
        {
            List<QuantityMeasurementEntity> entities = new List<QuantityMeasurementEntity>();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    
                    // Simple query with correct column names
                    string query = @"
                        SELECT 
                            Id,
                            OperationType,
                            MeasurementType,
                            FromValue,
                            FromUnit,
                            ToValue,
                            ToUnit,
                            Result,
                            ISNULL(ResultUnit, '') as ResultUnit,
                            CreatedAt
                        FROM QuantityMeasurements 
                        ORDER BY CreatedAt DESC";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                entities.Add(new QuantityMeasurementEntity
                                {
                                    Id = reader.GetInt32(0),
                                    OperationType = reader.GetString(1),
                                    MeasurementType = reader.GetString(2),
                                    FromValue = Convert.ToDouble(reader.GetDecimal(3)),
                                    FromUnit = reader.GetString(4),
                                    ToValue = Convert.ToDouble(reader.GetDecimal(5)),
                                    ToUnit = reader.GetString(6),
                                    Result = Convert.ToDouble(reader.GetDecimal(7)),
                                    ResultUnit = reader.GetString(8),
                                    CreatedAt = reader.GetDateTime(9)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException($"Error reading database: {ex.Message}", ex);
            }
            
            return entities;
        }
        
        public List<QuantityMeasurementEntity> GetByOperationType(string operationType)
        {
            List<QuantityMeasurementEntity> entities = new List<QuantityMeasurementEntity>();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    
                    // Simple query without joins
                    string query = @"
                        SELECT 
                            Id,
                            OperationType,
                            MeasurementType,
                            FromValue,
                            FromUnit,
                            ToValue,
                            ToUnit,
                            Result,
                            ISNULL(ResultUnit, '') as ResultUnit,
                            CreatedAt
                        FROM QuantityMeasurements 
                        WHERE OperationType = @OperationType
                        ORDER BY CreatedAt DESC";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OperationType", operationType);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                entities.Add(new QuantityMeasurementEntity
                                {
                                    Id = reader.GetInt32(0),
                                    OperationType = reader.GetString(1),
                                    MeasurementType = reader.GetString(2),
                                    FromValue = Convert.ToDouble(reader.GetDecimal(3)),
                                    FromUnit = reader.GetString(4),
                                    ToValue = Convert.ToDouble(reader.GetDecimal(5)),
                                    ToUnit = reader.GetString(6),
                                    Result = Convert.ToDouble(reader.GetDecimal(7)),
                                    ResultUnit = reader.GetString(8),
                                    CreatedAt = reader.GetDateTime(9)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException($"Error reading database: {ex.Message}", ex);
            }
            
            return entities;
        }
        
        public List<QuantityMeasurementEntity> GetByMeasurementType(string measurementType)
        {
            List<QuantityMeasurementEntity> entities = new List<QuantityMeasurementEntity>();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    
                    string query = @"
                        SELECT 
                            Id,
                            OperationType,
                            MeasurementType,
                            FromValue,
                            FromUnit,
                            ToValue,
                            ToUnit,
                            Result,
                            ISNULL(ResultUnit, '') as ResultUnit,
                            CreatedAt
                        FROM QuantityMeasurements 
                        WHERE MeasurementType = @MeasurementType
                        ORDER BY CreatedAt DESC";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MeasurementType", measurementType);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                entities.Add(new QuantityMeasurementEntity
                                {
                                    Id = reader.GetInt32(0),
                                    OperationType = reader.GetString(1),
                                    MeasurementType = reader.GetString(2),
                                    FromValue = Convert.ToDouble(reader.GetDecimal(3)),
                                    FromUnit = reader.GetString(4),
                                    ToValue = Convert.ToDouble(reader.GetDecimal(5)),
                                    ToUnit = reader.GetString(6),
                                    Result = Convert.ToDouble(reader.GetDecimal(7)),
                                    ResultUnit = reader.GetString(8),
                                    CreatedAt = reader.GetDateTime(9)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException($"Error reading database: {ex.Message}", ex);
            }
            
            return entities;
        }
        
        public int GetTotalCount()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    
                    string query = "SELECT COUNT(*) FROM QuantityMeasurements";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException($"Error getting count: {ex.Message}", ex);
            }
        }
    }
}
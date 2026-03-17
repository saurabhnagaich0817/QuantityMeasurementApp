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
                    
                    using (SqlCommand cmd = new SqlCommand("sp_InsertMeasurement", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@OperationTypeName", entity.OperationType ?? "");
                        cmd.Parameters.AddWithValue("@MeasurementTypeName", entity.MeasurementType ?? "");
                        cmd.Parameters.AddWithValue("@FromValue", entity.FromValue);
                        cmd.Parameters.AddWithValue("@FromUnitName", entity.FromUnit ?? "");
                        cmd.Parameters.AddWithValue("@ToValue", entity.ToValue);
                        cmd.Parameters.AddWithValue("@ToUnitName", entity.ToUnit ?? "");
                        cmd.Parameters.AddWithValue("@Result", entity.Result);
                        cmd.Parameters.AddWithValue("@ResultUnitName", entity.ResultUnit ?? "");
                        cmd.Parameters.AddWithValue("@UserId", entity.UserId ?? (object)DBNull.Value);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                entity.Id = Convert.ToInt32(reader["Id"]);
                            }
                        }
                        
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
                    
                    string query = "SELECT * FROM vw_RecentMeasurements";
                    
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
                    
                    string query = @"
                        SELECT qm.Id, ot.Name as OperationType, mt.Name as MeasurementType,
                               qm.FromValue, qm.FromUnitName as FromUnit,
                               qm.ToValue, qm.ToUnitName as ToUnit,
                               qm.Result, qm.ResultUnitName as ResultUnit,
                               qm.CreatedAt
                        FROM QuantityMeasurements qm
                        INNER JOIN OperationTypes ot ON qm.OperationTypeId = ot.Id
                        INNER JOIN MeasurementTypes mt ON qm.MeasurementTypeId = mt.Id
                        WHERE ot.Name = @OperationType
                        ORDER BY qm.CreatedAt DESC";
                    
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
        
        public string GetStatistics()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("sp_GetStatistics", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            System.Text.StringBuilder stats = new System.Text.StringBuilder();
                            
                            stats.AppendLine("=== OPERATION STATISTICS ===");
                            while (reader.Read())
                            {
                                stats.AppendLine($"{reader["OperationType"]}: {reader["Count"]} operations");
                            }
                            
                            return stats.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error getting statistics: {ex.Message}";
            }
        }
        
        public List<QuantityMeasurementEntity> GetMeasurementsPaged(int pageNumber, int pageSize)
        {
            List<QuantityMeasurementEntity> entities = new List<QuantityMeasurementEntity>();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("sp_GetMeasurements", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@PageSize", pageSize);
                        
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
            catch (Exception ex)
            {
                throw new DatabaseException($"Error in pagination: {ex.Message}", ex);
            }
            
            return entities;
        }
    }
}
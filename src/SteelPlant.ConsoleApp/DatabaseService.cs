using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using SteelPlant.ConsoleApp.Models;

namespace SteelPlant.ConsoleApp;

public class DatabaseService
{
    private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SteelPlantDB;Trusted_Connection=True;Encrypt=False;";
    
    public List<SteelGrade> GetSteelGrades()
    {
        var grades = new List<SteelGrade>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = "SELECT GradeID, GradeName, TargetTemperature FROM SteelGrades";

            using (var command = new SqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        grades.Add(new SteelGrade
                        {
                            GradeId = reader.GetInt32(0),
                            GradeName = reader.GetString(1),
                            TargetTemperature = reader.GetInt32(2)
                        });
                    }
                }
            }
        }
        return grades;
    }

    public void StartNewBatch(int gradeId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string insertBatchSql = @"INSERT INTO SteelBatches (GradeID, Status) 
                                              OUTPUT INSERTED.BatchID
                                              VALUES (@gradeId, 'In Progress')";
                    int newBatchId;
                    using (var command = new SqlCommand(insertBatchSql, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@gradeId", gradeId);
                        newBatchId = (int)command.ExecuteScalar();
                    }
                
                    string insertLogSql = "INSERT INTO ProductionLogs (BatchID, Message) VALUES (@batchID, 'Batch started manually via Console App')";
                    using (var command = new SqlCommand(insertLogSql, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@batchID", newBatchId);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    Console.WriteLine($"Successfully started batch with ID: {newBatchId}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error starting batch: {ex.Message}");
                    transaction.Rollback();
                }
            }
        }
    }

    public void FinishBatch(int batchId, decimal weight)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            
            using (var command = new SqlCommand("sp_FinishBatch", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BatchId", batchId);
                command.Parameters.AddWithValue("@FinalWeight", weight);
                command.ExecuteNonQuery();
                Console.WriteLine($"Batch #{batchId} finished with final weight: {weight} kg");
            }
        }
    }
}

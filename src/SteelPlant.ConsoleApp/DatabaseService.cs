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
}

using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Data;
using Notes.API.Models.Entities;

namespace Notes.API.Repository
{
    public class ValidationRepository
    {
        private readonly string _connectionString;

        public ValidationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveValidation(Note note)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Notes(Id, CardNo, Type, IsValidated) VALUES (@Id, @CardNo, @Type, @IsValidated)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = note.Id;
                    command.Parameters.Add("@CardNo", SqlDbType.NVarChar).Value = note.CardNo;
                    command.Parameters.Add("@Type", SqlDbType.NVarChar).Value = note.Type;
                    command.Parameters.Add("@IsValidated", SqlDbType.Bit).Value = note.IsValidated;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

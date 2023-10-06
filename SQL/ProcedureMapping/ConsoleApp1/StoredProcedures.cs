using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static public class StoredProcedures
    {
        static readonly string connectionString = "Server=127.0.0.1,1433;Database=demoDatabase;User Id=sa;Password=Qwerty123$;";

        static public async Task ExecuteProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            using SqlConnection connection = new(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            await command.ExecuteNonQueryAsync();
        }

        static public async Task<string> ExecuteSelectProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            using SqlConnection connection = new(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            var result = await WriteDataTableAsync(reader);
            return result;
        }

        static async public Task<String> WriteDataTableAsync(SqlDataReader reader)
        {
            // Get the column names
            DataTable schemaTable = reader.GetSchemaTable();
            var columnNames = schemaTable.Rows.Cast<DataRow>()
                .Select(row => row["ColumnName"].ToString())
                .ToList();

            String result = "";
            // Read the data rows
            while (await reader.ReadAsync())
            {
                // Access the column values dynamically
                foreach (var columnName in columnNames)
                {
                    var columnValue = reader[columnName];
                    result += $"{columnName}: {columnValue} ";
                }
                result += "\n";
            }

            return result;
        }
    }
}

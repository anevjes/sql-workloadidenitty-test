using System;
using Microsoft.Data.SqlClient;


class Program
{
    static void Main()
    {
        // Connection string with Active Directory Default authentication
        var connectionString = "Server=anevjes-sql-server.database.windows.net; Authentication=Active Directory MSI; Encrypt=True; User Id=21129c0c-e72a-408b-b589-96b88f9a24c2; Database=anevjes-sql-001";
        Console.WriteLine("Connection string: " + connectionString);

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // SQL command to read data from a table
            using (SqlCommand command = new SqlCommand("SELECT TOP (1000) * FROM [dbo].[authors]", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader[0], reader[1]);
                    }
                }
            }
        }
    }
}
using System;
using Microsoft.Data.SqlClient;


class Program
{
    static void Main()
    {
        // Connection string with Active Directory Default authentication
        var connectionString = "Server=anevjes-sql-server.database.windows.net; Authentication=Active Directory Managed Identity; Encrypt=True; User Id=da739e25-9a44-41be-be43-c682185d0fc1; Database=anevjes-sql-001";
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
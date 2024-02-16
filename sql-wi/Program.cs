using System;
using Microsoft.Data.SqlClient;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;


class Program
{
    static void Main()
    {

        // try out aZKV:
        var keyVaultName = "azwi-kv1312anevjes"; 
        var keyVaultUrl = $"https://{keyVaultName}.vault.azure.net/";
        
        //Thread.Sleep(100000);


        var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        var secret = secretClient.GetSecret("someSecret");

        Console.WriteLine("Trying fetching Secret from keyvault using DefualtCredentials: " + secret.Value.Value);


        // Connection string with Active Directory Default authentication
        var connectionString = "Server=anevjes-sql-server.database.windows.net; Authentication=Active Directory Default; Encrypt=True; Database=anevjes-sql-001";
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
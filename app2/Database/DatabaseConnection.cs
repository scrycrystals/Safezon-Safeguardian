using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace app2.Database
{
    public class DatabaseConnection
    {
        private static readonly string serverName = "database-1.c38ecky2q5jm.eu-north-1.rds.amazonaws.com,1433";
        private static readonly string databaseName = "Safezone";
        private static readonly string username = "admin";
        private static readonly string password = "admin1234&";

        private static readonly string connectionString =
            $"Server={serverName};Database={databaseName};User Id={username};Password={password};TrustServerCertificate=true";

        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection error: {ex.Message}");
                throw;
            }
        }

    }
}

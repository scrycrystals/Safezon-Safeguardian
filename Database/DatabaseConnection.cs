using System;
using MySql.Data.MySqlClient; // ✅ Correct namespace for MySQL

namespace app2.Database
{
    public class DatabaseConnection
    {
        private static readonly string serverName = "mysql-391d0718-kanzulzoha2003-safezine.l.aivencloud.com";
        private static readonly int port = 16446;
        private static readonly string databaseName = "defaultdb";
        private static readonly string username = "avnadmin";
        private static readonly string password = "AVNS_d3pDYPIsAGwLHme_eyx";

        private static readonly string connectionString =
            $"Server={serverName};Port={port};Database={databaseName};User Id={username};Password={password};SslMode=Required;";

        public static MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
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

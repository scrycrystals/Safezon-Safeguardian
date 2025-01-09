using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app2.Database;
using app2.Models;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace app2.ViewModels
{
    public class RegisterViewModel
    {
        public bool RegisterUser(RegisterModel user)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    string query = "INSERT INTO Register (Username, Email, Password, PhoneNumber) VALUES (@Username, @Email, @Password, @PhoneNumber)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                        int result = command.ExecuteNonQuery();
                        return result > 0; // Return true if insertion is successful
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                return false;
            }
        }
    }
}

using System;
using MySql.Data.MySqlClient; // ✅ Correct for MySQL
using app2.Database;
using app2.Models;

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
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Insert user into Register table and get the inserted UserID
                            string registerQuery = "INSERT INTO Register (Username, Email, Password, PhoneNumber) VALUES (@Username, @Email, @Password, @PhoneNumber); SELECT LAST_INSERT_ID();";

                            int userId; // Variable to store the inserted UserId
                            using (var registerCommand = new MySqlCommand(registerQuery, connection, transaction))
                            {
                                registerCommand.Parameters.AddWithValue("@Username", user.Username);
                                registerCommand.Parameters.AddWithValue("@Email", user.Email);
                                registerCommand.Parameters.AddWithValue("@Password", user.Password);
                                registerCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                                userId = Convert.ToInt32(registerCommand.ExecuteScalar()); // Get the generated UserId
                            }

                            // Call the separate function to create UserProfile
                            bool profileCreated = CreateUserProfile(connection, transaction, userId, user.Username, user.Password, user.Email, user.PhoneNumber);

                            if (!profileCreated)
                            {
                                throw new Exception("User profile creation failed.");
                            }

                            // Commit transaction if everything succeeds
                            transaction.Commit();
                            return true; // Registration successful
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Console.WriteLine($"Error during registration: {ex.Message}");
                            return false; // Registration failed
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection error: {ex.Message}");
                return false; // Connection failed
            }
        }

        /// <summary>
        /// Inserts a new user into the UserProfile table.
        /// </summary>
        private bool CreateUserProfile(MySqlConnection connection, MySqlTransaction transaction, int registrationId, string userName, string password, string email, string phoneNumber)
        {
            try
            {
                string profileQuery = "INSERT INTO UserProfile (RegistrationId, UserName, Password, Email, PhoneNumber) VALUES (@RegistrationId, @UserName, @Password, @Email, @PhoneNumber)";

                using (var profileCommand = new MySqlCommand(profileQuery, connection, transaction))
                {
                    profileCommand.Parameters.AddWithValue("@RegistrationId", registrationId);
                    profileCommand.Parameters.AddWithValue("@UserName", userName);
                    profileCommand.Parameters.AddWithValue("@Password", password);
                    profileCommand.Parameters.AddWithValue("@Email", email);
                    profileCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                    int result = profileCommand.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user profile: {ex.Message}");
                return false;
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MySql.Data.MySqlClient; // ✅ Correct for MySQL
using app2.Database;
using Xamarin.Essentials;
using Dapper;
using app2.Models;


namespace app2.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value ?? string.Empty;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value ?? string.Empty;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set
            {
                phone = value ?? string.Empty;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string dateOfBirth;
        public string DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value ?? string.Empty;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value ?? string.Empty;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string instagram;
        public string Instagram
        {
            get => instagram;
            set
            {
                instagram = value ?? string.Empty;
                OnPropertyChanged(nameof(Instagram));
            }
        }

        private string profileImage;
        public string ProfileImage
        {
            get => profileImage;
            set
            {
                profileImage = value ?? "https://via.placeholder.com/80"; // Default image
                OnPropertyChanged(nameof(ProfileImage));
            }
        }

        public ICommand LoadProfileCommand { get; }

        public ProfileViewModel()
        {
            LoadProfileCommand = new Command(async () => await LoadProfileData());
        }



        public async Task<bool> UpdateData(string username, string email, string number,
                                    string instagram, string profileImage, DateTime? dateOfBirth)
        {
            try
            {
                int userId = UserSession.UserId;
                if (userId == 0) return false;

                using (var connection = DatabaseConnection.GetConnection())
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        await connection.OpenAsync();

                    string query = @"
                UPDATE UserProfile 
                SET 
                    UserName = ISNULL(@Username, UserName), 
                    Email = ISNULL(@Email, Email), 
                    PhoneNumber = ISNULL(@Number, PhoneNumber), 
                    InstaId = ISNULL(@Instagram, InstaId), 
                    ProfileImage = ISNULL(@ProfileImage, ProfileImage), 
                    DateOfBirth = ISNULL(@DateOfBirth, DateOfBirth)
                WHERE RegistrationId = @UserId";

                    int rowsAffected = await connection.ExecuteAsync(query, new
                    {
                        Username = string.IsNullOrWhiteSpace(username) ? null : username,
                        Email = string.IsNullOrWhiteSpace(email) ? null : email,
                        Number = string.IsNullOrWhiteSpace(number) ? null : number,
                        Instagram = string.IsNullOrWhiteSpace(instagram) ? null : instagram,
                        ProfileImage = string.IsNullOrWhiteSpace(profileImage) ? null : profileImage,
                        DateOfBirth = dateOfBirth,
                        UserId = userId
                    });

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating profile: {ex.Message}");
                return false;
            }
        }


        public async Task LoadProfileData()
        {
            try
            {
                int userId = UserSession.UserId;
                if (userId == 0) return;

                using (var connection = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT UserName, Email, PhoneNumber, DateOfBirth, InstaId, ProfileImage, Password FROM UserProfile WHERE RegistrationId = @UserId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                Name = reader["UserName"] as string ?? string.Empty;
                                Email = reader["Email"] as string ?? string.Empty;
                                Phone = reader["PhoneNumber"] as string ?? string.Empty;

                                // Convert DateOfBirth safely
                                if (reader["DateOfBirth"] != DBNull.Value)
                                {
                                    DateTime dob = (DateTime)reader["DateOfBirth"];
                                    DateOfBirth = dob.ToString("yyyy-MM-dd"); // Format to a readable string
                                }
                                else
                                {
                                    DateOfBirth = string.Empty; // Handle null values
                                }

                                Password = reader["Password"] as string ?? string.Empty;
                                Instagram = reader["InstaId"] as string ?? string.Empty;
                                ProfileImage = reader["ProfileImage"] as string ?? string.Empty;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to load profile: {ex.Message}", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> UpdatePassword(string newPassword)
        {
            try
            {
                int userId = UserSession.UserId;
                if (userId == 0) return false;

                using (var connection = DatabaseConnection.GetConnection())
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        await connection.OpenAsync();

                    string query = @"
            UPDATE UserProfile 
            SET Password = @NewPassword
            WHERE RegistrationId = @UserId";

                    int rowsAffected = await connection.ExecuteAsync(query, new
                    {
                        NewPassword = newPassword,
                        UserId = userId
                    });

                    return rowsAffected > 0; // Return true if update was successful
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating password: {ex.Message}");
                return false;
            }
        }

    }
}

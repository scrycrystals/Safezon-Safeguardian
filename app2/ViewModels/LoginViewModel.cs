using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app2.Models;
using MySql.Data.MySqlClient; // ✅ Correct for MySQL
using System.ComponentModel;
using app2.Database;
using System.Windows.Input;
using System.Text.RegularExpressions;
using app2.Views;

namespace app2.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email and Password are required!", "OK");
                return;
            }

            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            bool validEmail = Regex.IsMatch(email, regex);
            if (!validEmail)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email format is not correct!", "OK");
                return;
            }

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT idRegister FROM Register WHERE Email = @Email AND Password = @Password";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Password", Password);

                        var userId = command.ExecuteScalar();

                        if (userId != null)
                        {
                            // Store UserId in UserSession
                            UserSession.UserId = Convert.ToInt32(userId);

                            await App.Current.MainPage.DisplayAlert("Success", "Login Successful!", "OK");
                            await App.Current.MainPage.Navigation.PushAsync(new SliderPage());
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error", "Invalid Email or Password!", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Database connection error: {ex.Message}", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

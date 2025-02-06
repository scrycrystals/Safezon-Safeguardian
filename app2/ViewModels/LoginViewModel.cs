using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app2.Models;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using app2.Database;
using System.Windows.Input;
using System.Net.Mail;
using app2.Views;
using System.Text.RegularExpressions;

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

            bool validemail = Regex.IsMatch(email, regex);
            if (!validemail)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email format is not correct!", "OK");
                return;
            }

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT UserId FROM Register WHERE Email = @Email AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Password", Password);

                        var userId = command.ExecuteScalar(); // Get UserId if email and password match

                        if (userId != null)
                        {
                            // Store UserId in UserSession
                            UserSession.UserId = (int)userId;

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

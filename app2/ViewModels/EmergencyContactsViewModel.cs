using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using app2.Database;
using app2.Models;

namespace app2.ViewModels
{
    public class EmergencyContactsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PrimaryContact> PrimaryContacts { get; set; } = new();

        public EmergencyContactsViewModel() 
        {

            Task.Run(async () => await LoadPrimaryContacts()); // Load contacts on initialization
                                                               
        }

            public async void AddPrimaryContact(PrimaryContact contact)
        {
            if (contact == null) return;

            PrimaryContacts.Add(contact);
            await SavePrimaryContactToDatabase(contact);
        }

        private async Task SavePrimaryContactToDatabase(PrimaryContact contact)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        await connection.OpenAsync();

                    string query = @"
                    INSERT INTO PrimaryContacts (UserId, ContactName, ContactNumber, SingleTap, DoubleTap, Always)
                    VALUES (@UserId, @ContactName, @ContactNumber, @SingleTap, @DoubleTap, @Always)";

                    await connection.ExecuteAsync(query, contact);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
            }
        }

        public async Task LoadPrimaryContacts()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        await connection.OpenAsync();

                    string query = "SELECT * FROM PrimaryContacts WHERE UserId = @UserId";
                    var contacts = await connection.QueryAsync<PrimaryContact>(query, new { UserId = UserSession.UserId });

                    PrimaryContacts.Clear(); // Clear old data
                    foreach (var contact in contacts)
                    {
                        PrimaryContacts.Add(contact);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

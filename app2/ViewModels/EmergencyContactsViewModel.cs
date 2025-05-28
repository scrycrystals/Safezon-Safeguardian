using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using app2.Database;
using app2.Models;
using System.Data;

namespace app2.ViewModels
{
    public class EmergencyContactsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PrimaryContact> PrimaryContacts { get; set; } = new();

        public EmergencyContactsViewModel()
        {
        }

        public async Task AddPrimaryContact(PrimaryContact contact)
        {
            if (contact == null) return;

            PrimaryContacts.Add(contact);
            await SavePrimaryContactToDatabase(contact);
        }

        public async Task SavePrimaryContactToDatabase(PrimaryContact contact)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        await connection.OpenAsync();

                    string query = @"
                        INSERT INTO PrimaryContacts (UserId, ContactName, ContactNumber, SingleTap, DoubleTap, Always)
                        VALUES (@UserId, @ContactName, @ContactNumber, @SingleTap, @DoubleTap, @Always)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", contact.UserId);
                        command.Parameters.AddWithValue("@ContactName", contact.ContactName);
                        command.Parameters.AddWithValue("@ContactNumber", contact.ContactNumber);
                        command.Parameters.AddWithValue("@SingleTap", contact.SingleTap);
                        command.Parameters.AddWithValue("@DoubleTap", contact.DoubleTap);
                        command.Parameters.AddWithValue("@Always", contact.Always);

                        int rows = await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Rows affected: {rows}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error (Insert): {ex.Message}");
            }
        }

        public async Task LoadPrimaryContacts()
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        await connection.OpenAsync();

                    string query = "SELECT * FROM PrimaryContacts WHERE UserId = @UserId";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", UserSession.UserId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            PrimaryContacts.Clear();

                            while (await reader.ReadAsync())
                            {
                                PrimaryContact contact = new PrimaryContact
                                {
                                    Id = reader.GetInt32("Id"),
                                    UserId = reader.GetInt32("UserId"),
                                    ContactName = reader.GetString("ContactName"),
                                    ContactNumber = reader.GetString("ContactNumber"),
                                    SingleTap = reader.GetBoolean("SingleTap"),
                                    DoubleTap = reader.GetBoolean("DoubleTap"),
                                    Always = reader.GetBoolean("Always")
                                };

                                PrimaryContacts.Add(contact);
                            }
                        }
                    }
                }

                // ✅ Only update UserSession after loading all
                UserSession.PrimaryContacts.Clear();
                UserSession.PrimaryContacts.AddRange(PrimaryContacts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error (Load): {ex.Message}");
            }
        }

        public async Task DeletePrimaryContactFromDatabase(int contactId)
        {
            try
            {
                using var connection = DatabaseConnection.GetConnection();
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                string query = "DELETE FROM PrimaryContacts WHERE Id = @Id";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", contactId);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB delete error: " + ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

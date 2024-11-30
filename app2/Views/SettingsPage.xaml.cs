using System.Collections.ObjectModel;
using System.Windows.Input;

namespace app2.Views
{
    public partial class SettingsPage : ContentPage
    {
        public ObservableCollection<Address> AddressList { get; set; }
        public ICommand AddNewAddressCommand { get; set; }

        public SettingsPage()
        {
            InitializeComponent();

            // Sample data
            AddressList = new ObservableCollection<Address>
            {
                new Address { Title = "Omer Hall Jannat Road", City = "Lahore"},
                new Address { Title = "Girl's Service Center Street 15", City = "Lahore" },
                new Address { Title = "Khadija Hall Jannat Road", City = "Lahore" },
                new Address { Title = "Home", City = "Bahawalpur" },
                new Address { Title = "Usman Hall Jannat Road", City = "Lahore" },
                new Address { Title = "Zohra Hall", City = "Lahore" }
            };

            AddNewAddressCommand = new Command(AddNewAddress);

            BindingContext = this;
        }

        private void AddNewAddress()
        {
            // Handle adding a new address
            DisplayAlert("Add New Address", "Add Address button clicked.", "OK");
        }

        private void OnEditAddressClicked(object sender, EventArgs e)
        {
            // Handle editing an address
            DisplayAlert("Edit Address", "Edit button clicked.", "OK");
        }

        private void OnDeleteAddressClicked(object sender, EventArgs e)
        {
            // Handle deleting an address
            DisplayAlert("Delete Address", "Delete button clicked.", "OK");
        }
    }

    public class Address
    {
        public required string Title { get; set; }
        public required string City { get; set; }
    }
}

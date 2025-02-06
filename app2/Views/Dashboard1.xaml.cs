using Microsoft.Maui.Controls;

namespace app2
{
    public partial class Dashboard1 : ContentPage
    {
              public Dashboard1()
        {
            InitializeComponent();
            BindingContext = this;  // Set the BindingContext for data binding
        }


        private void OnMenuClicked(object sender, EventArgs e)
        {
            // Toggle the visibility of the dropdown menu
            DropdownMenu.IsVisible = !DropdownMenu.IsVisible;
        }

        private void OnCloseMenuClicked(object sender, EventArgs e)
        {
            // Close the dropdown menu
            DropdownMenu.IsVisible = false;
        }

        private void OnProfileClicked(object sender, EventArgs e)
        {
            // Handle Profile option click
            //DisplayAlert("Profile", "You selected Profile", "OK");
            Navigation.PushAsync(new Views.Profile());
            DropdownMenu.IsVisible = false; // Hide the menu after selection
        }

        private void OnDefenseScoreClicked(object sender, EventArgs e)
        {
            // Handle Defense Score option click
            //DisplayAlert("Defense Score", "You selected Defense Score", "OK");
            DropdownMenu.IsVisible = false; // Hide the menu after selection
            Navigation.PushAsync(new DefenseScore());

        }

        private void OnAboutClicked(object sender, EventArgs e)
        {
            // Handle About option click
            //DisplayAlert("About", "You selected About", "OK");
            Navigation.PushAsync(new Views.About());
            DropdownMenu.IsVisible = false; // Hide the menu after selection
        }

        private void OnPrivacyPolicyClicked(object sender, EventArgs e)
        {
            // Handle Privacy Policy option click
            //DisplayAlert("Privacy Policy", "You selected Privacy Policy", "OK");
            Navigation.PushAsync(new Views.PrivacyPolicy());
            DropdownMenu.IsVisible = false; // Hide the menu after selection
        }

        //private void OnSettingsClicked(object sender, EventArgs e)
        //{
        //    // Handle Settings option click
        //    DisplayAlert("Settings", "You selected Settings", "OK");
        //    DropdownMenu.IsVisible = false; // Hide the menu after selection
        //}

        public void OnCallPoliceClicked(object sender, EventArgs e)
        {
            // Show police station details in popup
            DetailsPopup.IsVisible = true;
            PopupTitle.Text = "Main Street Police Station";
            PopupAddress.Text = "123 Main Street";
            PopupPhoneNumber.Text = "123-456-7890";
        }

        public void OnHospitalTapped(object sender, EventArgs e)
        {
            // Handle the hospital tap
            // You can show hospital details in a similar way
            DetailsPopup.IsVisible = true;
            PopupTitle.Text = "City General Hospital";
            PopupAddress.Text = "456 Hospital Ave";
            PopupPhoneNumber.Text = "098-765-4321";
        }

        public void OnCallHospitalClicked(object sender, EventArgs e)
        {
            // Show hospital details in popup
            DetailsPopup.IsVisible = true;
            PopupTitle.Text = "City General Hospital";
            PopupAddress.Text = "456 Hospital Ave";
            PopupPhoneNumber.Text = "098-765-4321";
        }

        public void OnCallButtonClicked(object sender, EventArgs e)
        {
            // Your code to handle call button click (e.g., initiate a phone call)
        }

        public void OnClosePopupClicked(object sender, EventArgs e)
        {
            DetailsPopup.IsVisible = false; // Hide the popup
        }

        private void OnPoliceStationTapped(object sender, TappedEventArgs e)
        {
            // Handle police station tapped event
            // Similar to the hospital tapped method
            DetailsPopup.IsVisible = true;
            PopupTitle.Text = "Main Street Police Station";
            PopupAddress.Text = "123 Main Street";
            PopupPhoneNumber.Text = "123-456-7890";
        }

        // Setting Futher Menu 
        private void OnSettingsArrowClicked(object sender, EventArgs e)
        {
            // Toggle the visibility of the Settings dropdown menu
            SettingsDropdownMenu.IsVisible = !SettingsDropdownMenu.IsVisible;


        }

        private void OnDarkModeTapped(object sender, EventArgs e)
        {
            // Implement the logic for enabling Dark Mode
            DisplayAlert("Dark Mode", "Dark Mode activated!", "OK");
        }

        private void OnOfflineModeTapped(object sender, EventArgs e)
        {
            // Implement the logic for enabling Offline Mode
            DisplayAlert("Offline Mode", "Offline Mode activated!", "OK");
        }

        private void OnDeleteAccountTapped(object sender, EventArgs e)
        {
            // Implement the logic for deleting the account
            DisplayAlert("Delete Account", "Are you sure you want to delete your account?", "Yes", "No");
        }

        // dark mode option 
        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            // Ensure Application.Current is not null
            if (Application.Current is not null)
            {
                // Check if app supports user-defined themes
                if (Application.Current is app2.App app)
                {
                    // Enable or disable dark mode based on switch value
                    app.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
                }
            }
            else
            {
                // Log or handle null case for Current (unlikely scenario)
                Console.WriteLine("Application.Current is null. Unable to change theme.");
            }
        }

        private void OnPageTapped(object sender, EventArgs e)
        {
            // Hide dropdown and popup menus if they are visible
            if (DropdownMenu.IsVisible)
            {
                DropdownMenu.IsVisible = false;
            }

            if (DetailsPopup.IsVisible)
            {
                DetailsPopup.IsVisible = false;
            }
        }












    }
}

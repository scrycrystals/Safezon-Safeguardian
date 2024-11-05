using Microsoft.Maui.Controls;

namespace app2
{
    public partial class Dashboard1 : ContentPage
    {
        // Bindable properties for button colors
        public Color LocationButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
        public Color HealthButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
        public Color HomeButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
        public Color PoliceButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
        public Color CallButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent

        public Dashboard1()
        {
            InitializeComponent();
            BindingContext = this;  // Set the BindingContext for data binding
        }

        private void ResetButtonColors()
        {
            // Reset the colors as needed
            OnPropertyChanged(nameof(LocationButtonColor));
            OnPropertyChanged(nameof(HealthButtonColor));
            OnPropertyChanged(nameof(HomeButtonColor));
            OnPropertyChanged(nameof(PoliceButtonColor));
            OnPropertyChanged(nameof(CallButtonColor));
        }

        private async Task AnimateButton(ImageButton button)
        {
            // Scale up and add shadow effect
            await button.ScaleTo(1.3, 100); // Scale to 130%
            button.Opacity = 0.5; // Slightly decrease opacity to simulate pressed state

            // Reset opacity to original
            button.Opacity = 1.0; // Reset opacity to original
        }

        private void ResetOtherButtons(ImageButton a, ImageButton b, ImageButton c, ImageButton d)
        {
            a.Opacity = 0.5;
            a.ScaleTo(1.0, 100);
            b.Opacity = 0.5;
            b.ScaleTo(1.0, 100);
            c.Opacity = 0.5;
            c.ScaleTo(1.0, 100);
            d.Opacity = 0.5;
            d.ScaleTo(1.0, 100);
        }

        private async void OnLocationButtonClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                await AnimateButton(button); // Animate the button
            }
            ResetButtonColors();
            ResetOtherButtons(HealthButton, HomeButton, GuardButton, ContactButton);
            // Implement navigation or other effects as needed
            // await Navigation.PushAsync(new LocationPage());
        }

        private async void OnHealthcareButtonClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                await AnimateButton(button); // Animate the button
            }
            ResetButtonColors();
            ResetOtherButtons(LocationButton, HomeButton, GuardButton, ContactButton);
            // await Navigation.PushAsync(new HealthPage());
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                await AnimateButton(button); // Animate the button
            }
            ResetButtonColors();
            ResetOtherButtons(LocationButton, HealthButton, GuardButton, ContactButton);
            // await Navigation.PushAsync(new HomePage());
        }

        private async void OnPoliceButtonClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                await AnimateButton(button); // Animate the button
            }
            ResetButtonColors();
            ResetOtherButtons(LocationButton, HomeButton, HealthButton, ContactButton);
            // await Navigation.PushAsync(new PolicePage());
        }

        private async void OnTelephoneButtonClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                await AnimateButton(button); // Animate the button
            }
            ResetButtonColors();
            ResetOtherButtons(LocationButton, HomeButton, GuardButton, HealthButton);
            // await Navigation.PushAsync(new CallPage());
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
            Navigation.PushAsync(new Profile());
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
            DisplayAlert("About", "You selected About", "OK");
            DropdownMenu.IsVisible = false; // Hide the menu after selection
        }

        private void OnPrivacyPolicyClicked(object sender, EventArgs e)
        {
            // Handle Privacy Policy option click
            DisplayAlert("Privacy Policy", "You selected Privacy Policy", "OK");
            DropdownMenu.IsVisible = false; // Hide the menu after selection
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            // Handle Settings option click
            DisplayAlert("Settings", "You selected Settings", "OK");
            DropdownMenu.IsVisible = false; // Hide the menu after selection
        }

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
    }
}

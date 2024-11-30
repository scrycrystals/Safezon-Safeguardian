using CommunityToolkit.Maui.Views;

namespace app2.Views
{
    public partial class LocationPage : ContentPage
    {
        public Color LocationButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
        public Color HealthButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
        public Color HomeButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
        public Color PoliceButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
        public Color CallButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent

        public LocationPage()
        {
            InitializeComponent();
            BindingContext = this;
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
            await Task.Delay(100); // Brief delay before resetting opacity
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

        // Show the popup
        private void OnShareLocationClicked(object sender, EventArgs e)
        {
            Popup.IsVisible = true; // Show the popup
        }

        // Hide the popup
        private void ClosePopup(object sender, EventArgs e)
        {
            Popup.IsVisible = false; // Hide the popup
        }

        // Navigate to the SettingsPage when the "Setting" button is clicked
        private void OnSettingsClicked(object sender, EventArgs e)
        {
            // Navigate to the SettingsPage
           Navigation.PushAsync(new SettingsPage());
        }
    }
}

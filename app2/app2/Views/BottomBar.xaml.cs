namespace app2.Views;

public partial class BottomBar : ContentView
{
	public BottomBar()
	{
		InitializeComponent();
        BindingContext = this;  // Set the BindingContext for data binding

    }

    public Color LocationButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
    public Color HealthButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
    public Color HomeButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
    public Color PoliceButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
    public Color CallButtonColor { get; set; } = Color.FromArgb("#00000000"); // Transparent
    private async Task AnimateButton(ImageButton button)
    {
        // Scale up and add shadow effect
        await button.ScaleTo(1.3, 100); // Scale to 130%
        button.Opacity = 0.5; // Slightly decrease opacity to simulate pressed state

        // Reset opacity to original
        button.Opacity = 1.0; // Reset opacity to original
    }

    private async void LocationButton_Clicked(object sender, EventArgs e)
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

    private void ResetButtonColors()
    {
        // Reset the colors as needed
        OnPropertyChanged(nameof(LocationButtonColor));
        OnPropertyChanged(nameof(HealthButtonColor));
        OnPropertyChanged(nameof(HomeButtonColor));
        OnPropertyChanged(nameof(PoliceButtonColor));
        OnPropertyChanged(nameof(CallButtonColor));
    }


    private async void OnHealthcareButtonClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button)
        {
            await AnimateButton(button); // Animate the button
        }
        ResetButtonColors();
        ResetOtherButtons(LocationButton, HomeButton, GuardButton, ContactButton);
        await Navigation.PushAsync(new MainHealthPage());
    }

    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button)
        {
            await AnimateButton(button); // Animate the button
        }
        ResetButtonColors();
        ResetOtherButtons(LocationButton, HealthButton, GuardButton, ContactButton);
        await Navigation.PushAsync(new Dashboard1());
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
}
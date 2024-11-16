namespace app2.Views;

public partial class VirtualGuardianPage : ContentPage
{
    public VirtualGuardianPage()
    {
        InitializeComponent();
    }
    private async void OnVoiceInputClicked(object sender, EventArgs e)
    {
        // Simulate voice-to-text (you'd use a real voice API here)
        string voiceText = await SimulateVoiceToTextAsync();
        MessageInput.Text = voiceText;

        // Show send button after voice input
        var sendButton = new Button
        {
            Text = "Send",
            BackgroundColor = Color.FromArgb("#21F490"),
            TextColor = Colors.Black
        };
        sendButton.Clicked += OnSendButtonClicked;
        ((StackLayout)MessageInput.Parent).Children.Add(sendButton);
    }

    private Task<string> SimulateVoiceToTextAsync()
    {
        // Simulated voice-to-text result
        return Task.FromResult("This is a sample voice input converted to text.");
    }

    private void OnSendButtonClicked(object sender, EventArgs e)
    {
        // Send message logic
        DisplayAlert("Message Sent", $"Message: {MessageInput.Text}", "OK");
        ((StackLayout)MessageInput.Parent).Children.Remove((Button)sender);
    }

    private async void OnNotifyContactsClicked(object sender, EventArgs e)
    {
        var contacts = new List<string> { "John Doe", "Jane Smith", "Emily Johnson" };

        string selectedContact = await DisplayActionSheet(
            "Select an Emergency Contact",
            "Cancel",
            null,
            contacts.ToArray());

        if (!string.IsNullOrEmpty(selectedContact) && selectedContact != "Cancel")
        {
            // Simulate notifying the contact
            await DisplayAlert("Notification Sent", $"Notified {selectedContact}.", "OK");
        }
    }


}

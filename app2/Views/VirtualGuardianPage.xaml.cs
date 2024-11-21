namespace app2.Views;

public partial class VirtualGuardianPage : ContentPage
{
    public VirtualGuardianPage()
    {
        InitializeComponent();
    }

    private async void OnVoiceInputClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Recording", "Recording voice input...", "Stop");
        string voiceText = await SimulateVoiceToTextAsync();
        MessageInput.Text = voiceText;
    }

    private Task<string> SimulateVoiceToTextAsync()
    {
        return Task.FromResult("Simulated voice message");
    }

    private void OnSendButtonClicked(object sender, EventArgs e)
    {
        string message = MessageInput.Text;
        if (!string.IsNullOrWhiteSpace(message))
        {
            DisplayAlert("Message Sent", $"You sent: {message}", "OK");
            MessageInput.Text = string.Empty;
        }
        else
        {
            DisplayAlert("Empty Message", "Please enter a message before sending.", "OK");
        }
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
            await DisplayAlert("Notification Sent", $"Notified {selectedContact}.", "OK");
        }
    }

    private async void OnEmergencyServicesClicked(object sender, EventArgs e)
    {
        // List of emergency services
        var emergencyNumbers = new List<(string Name, string Number)>
        {
            ("Police", "15"),
            ("Ambulance", "112"),
            ("Fire Brigade", "101")
        };

        // Display options
        var actionSheet = await DisplayActionSheet(
            "Emergency Services",
            "Cancel",
            null,
            emergencyNumbers.Select(e => $"{e.Name} ({e.Number})").ToArray());

        if (actionSheet != "Cancel" && actionSheet != null)
        {
            string selectedNumber = emergencyNumbers.First(e => actionSheet.Contains(e.Number)).Number;

            var result = await DisplayActionSheet(
                $"Emergency Service: {selectedNumber}",
                "Cancel",
                null,
                "Call",
                "Send Message");

            if (result == "Call")
            {
                // Call logic (simulated)
                await Launcher.OpenAsync($"tel:{selectedNumber}");
            }
            else if (result == "Send Message")
            {
                // Simulate sending an emergency message
                await DisplayAlert("Message Sent", $"Message sent to {selectedNumber}.", "OK");
            }
        }
    }
}

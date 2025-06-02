using app2.Services;
using System.Linq;
using app2.Models;

namespace app2.Views;

public partial class VirtualGuardianPage : ContentPage
{
    private readonly ChatbotService _chatbotService;

    public VirtualGuardianPage()
    {
        InitializeComponent();
        _chatbotService = new ChatbotService();
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

    private async void OnSendButtonClicked(object sender, EventArgs e)
    {
        string userMessage = MessageInput.Text;
        if (!string.IsNullOrWhiteSpace(userMessage))
        {
            ChatbotResponseLabel.TextColor = Colors.Gray;
            ChatbotResponseLabel.Text = "Virtual Guardian is responding...";

            string botReply = await _chatbotService.GetResponseAsync(userMessage);

            ChatbotResponseLabel.TextColor = Colors.Black;
            ChatbotResponseLabel.Text = botReply;

            MessageInput.Text = string.Empty;
        }
        else
        {
            await DisplayAlert("Empty Message", "Please enter a message before sending.", "OK");
        }
    }

    private async void OnNotifyContactsClicked(object sender, EventArgs e)
    {
        var contacts = UserSession.PrimaryContacts;

        if (contacts == null || contacts.Count == 0)
        {
            await DisplayAlert("No Contacts", "No emergency contacts found. Please add them first.", "OK");
            return;
        }

        string[] contactNames = contacts.Select(c => $"{c.ContactName} ({c.ContactNumber})").ToArray();

        string selected = await DisplayActionSheet(
            "Select an Emergency Contact",
            "Cancel",
            null,
            contactNames);

        if (!string.IsNullOrEmpty(selected) && selected != "Cancel")
        {
            // You can add your actual notification logic here (SMS, Firebase, etc.)
            await DisplayAlert("Notification Sent", $"Notified {selected}.", "OK");
        }
    }


    private async void OnEmergencyServicesClicked(object sender, EventArgs e)
    {
        var emergencyNumbers = new List<(string Name, string Number)>
        {
            ("Police", "15"),
            ("Ambulance", "112"),
            ("Fire Brigade", "101")
        };

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
                await Launcher.OpenAsync($"tel:{selectedNumber}");
            }
            else if (result == "Send Message")
            {
                await DisplayAlert("Message Sent", $"Message sent to {selectedNumber}.", "OK");
            }
        }
    }
}

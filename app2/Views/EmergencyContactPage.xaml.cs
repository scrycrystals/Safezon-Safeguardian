using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace app2.Views
{
    public partial class EmergencyContactPage : ContentPage
    {
        public EmergencyContactPage()
        {
            InitializeComponent();
        }

        // Import primary contacts
        private async void ImportPrimaryContactsClicked(object sender, EventArgs e)
        {
            var availableContacts = new[]
            {
                "John Doe - 123-456-7890",
                "Jane Smith - 987-654-3210",
                "Alice Johnson - 456-789-1234"
            };

            string selectedContact = await DisplayActionSheet("Select Contact to Add", "Cancel", null, availableContacts);

            if (!string.IsNullOrEmpty(selectedContact) && selectedContact != "Cancel")
            {
                AddPrimaryContact(selectedContact);
                NoPrimaryContactsLabel.IsVisible = false;
            }
        }

        private void AddPrimaryContact(string contact)
        {
            var contactLayout = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 30 },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = 30 }
                },
                Margin = new Thickness(0, 5, 0, 5)
            };

            var phoneIcon = new Image { Source = "phone.png", HeightRequest = 20, WidthRequest = 20, VerticalOptions = LayoutOptions.Center };
            contactLayout.Children.Add(phoneIcon);
            Grid.SetColumn(phoneIcon, 0);

            var contactLabel = new Label { Text = contact, FontSize = 16, VerticalOptions = LayoutOptions.Center, TextColor = Color.FromArgb("#757575") };
            contactLayout.Children.Add(contactLabel);
            Grid.SetColumn(contactLabel, 1);

            var deleteButton = new ImageButton { Source = "arrow.png", HeightRequest = 20, WidthRequest = 20, BackgroundColor = Colors.Transparent, VerticalOptions = LayoutOptions.Center };
            deleteButton.Clicked += (s, e) => RemovePrimaryContact(contactLayout);
            contactLayout.Children.Add(deleteButton);
            Grid.SetColumn(deleteButton, 2);

            PrimaryContactsStack.Children.Add(contactLayout);
        }

        private void RemovePrimaryContact(Grid contactLayout)
        {
            PrimaryContactsStack.Children.Remove(contactLayout);

            if (PrimaryContactsStack.Children.Count == 0)
            {
                NoPrimaryContactsLabel.IsVisible = true;
            }
        }

        // Notification settings
        private void NotificationSettingsClicked(object sender, EventArgs e)
        {
            NotificationSettingsPopup.IsVisible = true;
        }

        private void SaveNotificationSettingsClicked(object sender, EventArgs e)
        {
            bool alwaysNotify = AlwaysNotifySlider.Value == 1;
            bool singleTapNotify = SingleTapNotifySlider.Value == 1;
            bool doubleTapNotify = SingleTapNotifySlider.Value == 1;

            Preferences.Set("AlwaysNotify", alwaysNotify);
            Preferences.Set("SingleTapNotify", singleTapNotify);
            Preferences.Set("DoubleTapNotify", doubleTapNotify);

            NotificationSettingsPopup.IsVisible = false;
            DisplayAlert("Settings Saved", "Your notification preferences have been updated.", "OK");
        }

        private void CancelNotificationSettingsClicked(object sender, EventArgs e)
        {
            NotificationSettingsPopup.IsVisible = false;
        }

        // Sync contacts
        private async void SyncAndImportClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Sync & Import", "Contacts synced from Google Drive.", "OK");
        }

        // Import secondary contacts
        private async void ImportSecondaryContactsClicked(object sender, EventArgs e)
        {
            var availableContacts = new[]
            {
                "Michael Brown - 567-890-1234",
                "Emily Davis - 654-321-9870",
                "Chris Wilson - 890-123-4567"
            };

            string selectedContact = await DisplayActionSheet("Select Contact to Add", "Cancel", null, availableContacts);

            if (!string.IsNullOrEmpty(selectedContact) && selectedContact != "Cancel")
            {
                AddSecondaryContact(selectedContact);
                NoSecondaryContactsLabel.IsVisible = false;
            }
        }

        private void AddSecondaryContact(string contact)
        {
            var contactLayout = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 30 },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = 30 }
                },
                Margin = new Thickness(0, 5, 0, 5)
            };

            var phoneIcon = new Image { Source = "phone.png", HeightRequest = 20, WidthRequest = 20, VerticalOptions = LayoutOptions.Center };
            contactLayout.Children.Add(phoneIcon);
            Grid.SetColumn(phoneIcon, 0);

            var contactLabel = new Label { Text = contact, FontSize = 16, VerticalOptions = LayoutOptions.Center, TextColor = Color.FromArgb("#757575") };
            contactLayout.Children.Add(contactLabel);
            Grid.SetColumn(contactLabel, 1);

            var deleteButton = new ImageButton { Source = "arrow.png", HeightRequest = 20, WidthRequest = 20, BackgroundColor = Colors.Transparent, VerticalOptions = LayoutOptions.Center };
            deleteButton.Clicked += (s, e) => RemoveSecondaryContact(contactLayout);
            contactLayout.Children.Add(deleteButton);
            Grid.SetColumn(deleteButton, 2);

            SecondaryContactsStack.Children.Add(contactLayout);
        }

        private void RemoveSecondaryContact(Grid contactLayout)
        {
            SecondaryContactsStack.Children.Remove(contactLayout);

            if (SecondaryContactsStack.Children.Count == 0)
            {
                NoSecondaryContactsLabel.IsVisible = true;
            }
        }

        // Create group functionality
        private async void CreateGroupClicked(object sender, EventArgs e)
        {
            string? groupName = await DisplayPromptAsync("Create Group", "Enter group name:");

            if (string.IsNullOrWhiteSpace(groupName))
            {
                await DisplayAlert("Error", "Group name cannot be empty.", "OK");
                return;
            }

            // Group layout
            var groupLayout = new StackLayout
            {
                Spacing = 10,
                Margin = new Thickness(0, 10)
            };

            // Group header (name and Add Member button)
            var groupHeader = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                VerticalOptions = LayoutOptions.Center
            };

            var groupNameLabel = new Label
            {
                Text = groupName,
                FontAttributes = FontAttributes.Bold,
                FontSize = 18,
                VerticalOptions = LayoutOptions.Center
            };

            var addMemberButton = new Button
            {
                Text = "Add Member",
                FontSize = 14,
                BackgroundColor = Color.FromArgb(""),
                TextColor = Colors.White,
                CornerRadius = 5,
                Padding = new Thickness(5, 5)
            };

            addMemberButton.Clicked += async (s, e) => await AddMembersToGroup(groupLayout);

            groupHeader.Children.Add(groupNameLabel);
            groupHeader.Children.Add(addMemberButton);

            groupLayout.Children.Add(groupHeader);
            GroupsStack.Children.Add(groupLayout);

            NoGroupsLabel.IsVisible = false;
        }

        // Function to add members to a group
        private async Task AddMembersToGroup(StackLayout groupLayout)
        {
            var availableContacts = new List<Contact>
    {
        new Contact { Name = "John Doe", IsSelected = false },
        new Contact { Name = "Jane Smith", IsSelected = false },
        new Contact { Name = "Alice Johnson", IsSelected = false },
        new Contact { Name = "Michael Brown", IsSelected = false },
        new Contact { Name = "Emily Davis", IsSelected = false }
    };

            ContentPage? contactSelectionPage = null;

            contactSelectionPage = new ContentPage
            {
                Title = "Select Contacts",
                BackgroundColor = Colors.Black,

                Content = new StackLayout
                {
                    Children =
            {
                new Label { Text = "Select Contacts to Add", FontSize = 20, HorizontalOptions = LayoutOptions.Center, Margin = 50 },
                new ListView
                {
                    ItemsSource = availableContacts,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        var checkbox = new CheckBox();
                        checkbox.SetBinding(CheckBox.IsCheckedProperty, nameof(Contact.IsSelected));

                        var nameLabel = new Label();
                        nameLabel.SetBinding(Label.TextProperty, nameof(Contact.Name));

                        return new ViewCell
                        {
                            View = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children = { checkbox, nameLabel }
                            }
                        };
                    })
                },
                new Button
                {
                    Text = "Add to Group",
                     FontSize = 16,
                     BackgroundColor = Color.FromArgb("#008000"), // Green button
                     TextColor = Colors.White,
                    Command = new Command(async () =>
                    {
                        var selectedContacts = availableContacts.Where(c => c.IsSelected).ToList();

                        if (!selectedContacts.Any())
                        {
                            await contactSelectionPage.DisplayAlert("Error", "Select at least one contact.", "OK");
                            return;
                        }

                        foreach (var contact in selectedContacts)
                        {
                            var contactLayout = CreateContactLayout(contact.Name, groupLayout);
                            groupLayout.Children.Add(contactLayout);
                        }

                        await Navigation.PopAsync();
                    })
                }
            }
                }
            };

            await Navigation.PushAsync(contactSelectionPage);
        }

        // Function to create a contact layout with a delete button
        private StackLayout CreateContactLayout(string contactName, StackLayout groupLayout)
        {
            var contactLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                Margin = new Thickness(0, 5)
            };

            var contactLabel = new Label
            {
                Text = contactName,
                TextColor = Color.FromArgb("#000000"),
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center
            };

            var deleteButton = new Button
            {
                Text = "X",
                FontSize = 14,
                TextColor = Colors.Red,
                BackgroundColor = Colors.Transparent,
                Padding = 20
            };

            deleteButton.Clicked += (s, e) => groupLayout.Children.Remove(contactLayout);

            contactLayout.Children.Add(contactLabel);
            contactLayout.Children.Add(deleteButton);

            return contactLayout;
        }

        // Contact Model
        public class Contact
        {
            public string Name { get; set; } = string.Empty; // Provide default value to avoid null warnings
            public bool IsSelected { get; set; }
        }
    }
}
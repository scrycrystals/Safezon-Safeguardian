using System;
using Microsoft.Maui.Controls;
using Plugin.Media;
using Plugin.Media.Abstractions;
using app2.ViewModels;

namespace app2
{
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Load the profile data when the page is about to appear
            await ((ProfileViewModel)BindingContext).LoadProfileData();
        }

        // Handle profile image upload
        private async void OnEditProfileImageClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Upload Photo", "Cancel", null, "Take Photo", "Choose from Gallery");
            if (action == "Take Photo")
            {
                await TakePhotoAsync();
            }
            else if (action == "Choose from Gallery")
            {
                await ChoosePhotoAsync();
            }
        }

        // Take photo with camera
        private async Task TakePhotoAsync()
        {
            var media = CrossMedia.Current;
            if (!media.IsCameraAvailable || !media.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "Camera is not available.", "OK");
                return;
            }

            var file = await media.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                Directory = "ProfilePhotos",
                Name = "profile.jpg"
            });

            if (file != null)
            {
                ProfileImage.Source = ImageSource.FromStream(() => file.GetStream());
            }
        }

        // Choose photo from gallery
        private async Task ChoosePhotoAsync()
        {
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            });

            if (file != null)
            {
                ProfileImage.Source = ImageSource.FromStream(() => file.GetStream());
            }
        }

        // Ensure Gmail account ends with '@gmail.com'
        private void OnGmailTextChanged(object sender, TextChangedEventArgs e)
        {
            string email = e.NewTextValue;
            if (!email.EndsWith("@gmail.com"))
            {
                GmailEntry.TextColor = Colors.Red;
            }
            else
            {
                GmailEntry.TextColor = Colors.Black;
            }
        }

        // Save changes
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string username = UsernameEntry.Text;
                string phoneNumber = PhoneNumberEntry.Text;
                string instagram = InstagramEntry.Text;
                string email = GmailEntry.Text;
                string profileImage = ProfileImage.Source?.ToString();
                DateTime? dateOfBirth = DateOfBirthPicker.Date != default ? DateOfBirthPicker.Date : (DateTime?)null;

                if (BindingContext is ProfileViewModel viewModel)
                {
                    bool isUpdated = await viewModel.UpdateData(username, email, phoneNumber, instagram, profileImage, dateOfBirth);

                    if (isUpdated)
                    {
                        await DisplayAlert("Profile Updated", "Your profile changes have been saved!", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Update Failed", "No changes were made or an error occurred.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "BindingContext is not set properly.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }


        // Cancel and go back
        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Go back without saving
        }
    }
}

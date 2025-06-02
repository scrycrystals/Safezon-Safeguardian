using System;
using Microsoft.Maui.Controls;
using app2.ViewModels;

namespace app2
{
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage(string currentPassword)
        {
            InitializeComponent();
            // Show the old password in the entry
            BindingContext = new ProfileViewModel();
        }

        // Password Strength Checker
        private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
        {
            string newPassword = e.NewTextValue;
            PasswordStrengthLabel.Text = $"Password Strength: {GetPasswordStrength(newPassword)}";
        }

        private string GetPasswordStrength(string password)
        {
            // Simple strength checker logic
            if (password.Length >= 12 && password.Any(char.IsUpper) && password.Any(char.IsDigit) && password.Any(char.IsSymbol))
            {
                return "Strong";
            }
            if (password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsDigit))
            {
                return "Moderate";
            }
            return "Weak";
        }

        // Save Button Clicked
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            string newPassword = NewPasswordEntry.Text;
            string reEnteredPassword = ReEnterPasswordEntry.Text;

            if (newPassword == reEnteredPassword)
            {
                if (BindingContext is ProfileViewModel viewModel)
                {
                    bool isUpdated = await viewModel.UpdatePassword(newPassword);

                    if (isUpdated)
                    {
                        await DisplayAlert("Success", "Your password has been changed!", "OK");
                        await Navigation.PopAsync(); // Go back to the original page
                    }
                    else
                    {
                        await DisplayAlert("Failed", "Your password has not been changed!", "OK");
                    }

                }
                else
                {
                    await DisplayAlert("Error", "Binding Not Possible", "OK");

                }
            }
            else
            {
                await DisplayAlert("Error", "Passwords do not match. Please try again.", "OK");
            }
        }

        // Cancel Button Clicked
        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Go back to the original page
        }
    }
}

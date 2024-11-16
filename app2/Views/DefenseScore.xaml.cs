using System;
using Microsoft.Maui.Controls;

namespace app2
{
    public partial class DefenseScore : ContentPage
    {
        public DefenseScore()
        {
            InitializeComponent();
        }

        private void OnCalculateDefenseScoreClicked(object sender, EventArgs e)
        {
            // Initialize the defense score
            int defenseScore = 0;

            // Check each checkbox and add to the score if checked
            if (WeaponCheckBox.IsChecked)
                defenseScore += 10; // Adjust score increment as needed

            if (AlarmCheckBox.IsChecked)
                defenseScore += 10;

            if (CompanionCheckBox.IsChecked)
                defenseScore += 15;

            if (PhoneCheckBox.IsChecked)
                defenseScore += 15;

            if (GPSCheckBox.IsChecked)
                defenseScore += 10;

            if (SafeRouteCheckBox.IsChecked)
                defenseScore += 20;

            if (EmergencyContactsCheckBox.IsChecked)
                defenseScore += 10;

            if (WellLitAreaCheckBox.IsChecked)
                defenseScore += 10;

            // Display the calculated score in the DefenseScoreLabel
            DefenseScoreLabel.Text = defenseScore.ToString();

            // Show a message based on the score range
            if (defenseScore >= 70)
            {
                DisplayAlert("Great!", "You are well-prepared and have a high defense score. Stay vigilant!", "OK");
            }
            else if (defenseScore >= 40)
            {
                DisplayAlert("Caution", "Your defense score is moderate. Consider improving your safety precautions.", "OK");
            }
            else
            {
                DisplayAlert("Warning", "Your defense score is low. Take necessary steps to improve your safety.", "OK");
            }
        }
    }
}

using app2.Services;
using app2.ViewModels;

namespace app2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

             MainPage = new AppShell();
            SetupNotificationListener(); // 👈 Add this

        }



        private void SetupNotificationListener()
        {
            var notificationManager = ServiceHelper.GetService<INotificationManagerService>();
            notificationManager.NotificationTapped += OnNotificationTapped;
        }

        private async void OnNotificationTapped(object sender, EventArgs e)
        {
            var gps = BraceletDataViewModel.Instance?.Data?.GPS;
            if (gps != null)
            {
                await MainPage.DisplayAlert("Location", $"Lat: {gps.f_latitude}\nLon: {gps.f_longitude}", "OK");
            }
        }

    }
}

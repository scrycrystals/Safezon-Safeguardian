using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using System.Xml.Linq;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using app2.Database;
using app2.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using app2.Services; // Make sure this namespace includes your INotificationManagerService

namespace app2.ViewModels
{
    public class BraceletDataViewModel : INotifyPropertyChanged
    {
        // 🔒 Static instance to access data globally (e.g. in App.xaml.cs)
        public static BraceletDataViewModel Instance { get; private set; }

        FirebaseConnection conn = new FirebaseConnection();
        private readonly INotificationManagerService _notificationManager;

        private BraceletDataModel _data;
        public BraceletDataModel Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        bool hasNotified = false;
        bool previousState = false;

        public BraceletDataViewModel()
        {
            Instance = this; // ✅ Set static instance

            _notificationManager = DependencyInjectionService();
            Data = new BraceletDataModel();

            SubscribeToFirebaseUpdates();
            StartFirebaseListener();
        }

        private INotificationManagerService DependencyInjectionService()
        {
#if ANDROID
            return ServiceHelper.GetService<INotificationManagerService>();
#else
            return null;
#endif
        }

        private async void StartFirebaseListener()
        {
            while (true)
            {
                SubscribeToFirebaseUpdates();
                await Task.Delay(15000); // Reconnect every 15 seconds
            }
        }

        private async void FetchDataOnce()
        {
            if (conn.client == null)
            {
                Console.WriteLine("Firebase client is NULL! Cannot fetch data.");
                return;
            }

            try
            {
                FirebaseResponse response = await conn.client.GetAsync("/");
                Console.WriteLine("Raw Firebase Response: " + response.Body);

                if (response.Body != "null")
                {
                    Data = JsonConvert.DeserializeObject<BraceletDataModel>(response.Body);
                    Console.WriteLine("Fetched initial data from Firebase:");
                    Console.WriteLine("BPM: " + Data.BPM);
                }
                else
                {
                    Console.WriteLine("No data found in Firebase.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching Firebase data: " + ex.Message);
            }
        }

        int CheckButtonState(int buttonState, double longitude, double latitude)
        {
            bool currentState = (buttonState == 1);

            if (currentState && !previousState && !hasNotified)
            {
                // Rising edge detected: 0 -> 1
                _notificationManager?.SendNotification("Emergency Alert", "Emergency Button Pressed on Bracelet!", latitude, longitude);
                hasNotified = true;
                return 0;
            }

            if (!currentState && previousState)
            {
                // Falling edge detected: 1 -> 0 (button released)
                hasNotified = false;
            }

            // Save state for next call
            previousState = currentState;
            return 0;
        }

        private async void FetchFullData()
        {
            try
            {
                FirebaseResponse response = await conn.client.GetAsync("/");
                if (response.Body != "null")
                {
                    Data = JsonConvert.DeserializeObject<BraceletDataModel>(response.Body);

                    Console.WriteLine("Updated Data from Firebase:");
                    Console.WriteLine("BPM: " + Data.BPM);
                    Console.WriteLine("Temp: " + Data.Temp);
                    Console.WriteLine("Button: " + Data.Button);
                    Console.WriteLine("X_Angl: " + Data.X_Angl);
                    Console.WriteLine("Y_Angl: " + Data.Y_Angl);
                    Console.WriteLine("Longitude: " + Data.GPS.f_longitude);
                    Console.WriteLine("Latitude: " + Data.GPS.f_latitude);
                    // ✅ Emergency Notification Logic (only once per press)
                    //Data.Button = CheckButtonState(Data.Button, Data.GPS.f_longitude, Data.GPS.f_latitude);
                    if (Data.Button == 1)
                    {
                        _notificationManager?.SendNotification(
                            "Emergency Alert",
                            "Emergency Button Pressed on Bracelet!",
                            Data.GPS.f_latitude,
                            Data.GPS.f_longitude
                        );

                        // 🔄 Update Button = 0 back to Firebase
                        try
                        {
                            //var updateData = new { Button = 0 };
                            var resetResponse = await conn.client.SetAsync("Button", 0); 
                            Console.WriteLine("Button state reset to 0 in Firebase.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to reset button in Firebase: " + ex.Message);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching full data: " + ex.Message);
            }
        }

        private void SubscribeToFirebaseUpdates()
        {
            if (conn.client == null)
            {
                Console.WriteLine("Firebase client is NULL! Check your internet connection and Firebase credentials.");
                return;
            }

            Console.WriteLine("Firebase connected successfully!");

            conn.client.OnAsync("/", (sender, args, context) =>
            {
                Console.WriteLine("Firebase data change detected!");
                FetchFullData();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

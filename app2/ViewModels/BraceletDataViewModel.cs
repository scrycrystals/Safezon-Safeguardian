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

namespace app2.ViewModels
{
    public class BraceletDataViewModel : INotifyPropertyChanged
    {
        FirebaseConnection conn = new FirebaseConnection();

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
        /*public BraceletDataModel LoadData()
        {
            try
            {
                FirebaseResponse response = conn.client.Get("/"); // Fetch data from root
                BraceletDataModel data = JsonConvert.DeserializeObject<BraceletDataModel>(response.Body.ToString());
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving data: " + ex.Message);
                return null;
            }
        }*/

        public BraceletDataViewModel()
        {
            Data = new BraceletDataModel();
            //FetchDataOnce();  
            
            SubscribeToFirebaseUpdates();
            StartFirebaseListener();
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
                    Console.WriteLine("BPM: " + Data.Temp);
                    Console.WriteLine("BPM: " + Data.Button);
                    Console.WriteLine("BPM: " + Data.X_Angl);
                    Console.WriteLine("BPM: " + Data.Y_Angl);
                    Console.WriteLine("BPM: " + Data.GPS.f_longitude);
                    Console.WriteLine("BPM: " + Data.GPS.f_latitude);
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

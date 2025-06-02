using Android.App;
using Android.Content.PM;
using Android.OS;
using Android;
using app2.Platforms.Android;
using app2.Services;
using Android.Content;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.ApplicationModel;



namespace app2
{
    [Activity(Label = "SafeZone", Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation |
                              ConfigChanges.UiMode | ConfigChanges.ScreenLayout |
                              ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Request SMS and Phone permissions
            RequestPermissions(new string[]
            {
                Manifest.Permission.SendSms,
                Manifest.Permission.ReadPhoneState
            }, 0);

            // Request POST_NOTIFICATIONS permission only on Android 13+ (API 33)
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
            {
                if (CheckSelfPermission(Manifest.Permission.PostNotifications) != Permission.Granted)
                {
                    RequestPermissions(new string[] { Manifest.Permission.PostNotifications }, 1);
                }
            }
        }

        public async Task<string> ResolveAddressAsync(double lat, double lng)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lng);
                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    return $"{placemark.Thoroughfare}, {placemark.Locality}, {placemark.AdminArea}, {placemark.CountryName}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Reverse Geocoding failed: {ex.Message}");
            }

            return "Unknown Location";
        }


        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            if (intent?.Action == "NOTIFICATION_TAP")
            {
                double lat = intent.GetDoubleExtra("lat", 0);
                double lng = intent.GetDoubleExtra("lng", 0);

                // Access BraceletDataViewModel.Instance or directly call method to show dialog
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(lat, lng);
                    var place = placemarks?.FirstOrDefault();

                    string address = place != null
                        ? $"{place.Thoroughfare}, {place.Locality}, {place.CountryName}"
                        : "Unknown location";

                    bool openMaps = await Shell.Current.CurrentPage.DisplayAlert(
                    "Emergency Location",
                    $"User is at:\n\n{address}\n\nDo you want to navigate there?",
                    "Navigate",
                    "Cancel");


                    if (openMaps)
                    {
                        string mapsUri = $"https://www.google.com/maps/dir/?api=1&destination={lat},{lng}";
                        await Launcher.OpenAsync(mapsUri);
                    }
                });
            }
        }



    }
}

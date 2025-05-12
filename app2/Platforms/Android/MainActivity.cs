using Android.App;
using Android.Content.PM;
using Android.OS;
using Android;

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

            // Request both ReadContacts and SendSMS permissions
            RequestPermissions(new string[]
{
    Android.Manifest.Permission.SendSms,
    Android.Manifest.Permission.ReadPhoneState
}, 0);

        }
    }
}

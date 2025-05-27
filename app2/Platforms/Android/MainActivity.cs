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
    }
}

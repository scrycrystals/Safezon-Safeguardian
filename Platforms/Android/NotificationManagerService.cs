using System;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using app2.Services;
using Microsoft.Maui.ApplicationModel;

namespace app2.Platforms.Android
{
    public class NotificationManagerService : INotificationManagerService
    {
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "Default channel for notifications";
        bool channelInitialized = false;
        int messageId = 0;

        NotificationManagerCompat notificationManager;

        public event EventHandler NotificationReceived;
        public event EventHandler NotificationTapped;

        public NotificationManagerService()
        {
            CreateNotificationChannel();
            notificationManager = NotificationManagerCompat.From(Platform.CurrentActivity ?? Platform.AppContext);
        }

        void CreateNotificationChannel()
        {
            if (channelInitialized)
                return;

            var channel = new NotificationChannel(channelId, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var manager = (NotificationManager)Platform.AppContext.GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(channel);
            channelInitialized = true;
        }

        public void SendNotification(string title, string message, double latitude = 0, double longitude = 0)
        {
            var intent = new Intent(Platform.AppContext, typeof(MainActivity));
            intent.SetAction("NOTIFICATION_TAP");
            intent.PutExtra("lat", latitude);
            intent.PutExtra("lng", longitude);
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

            var pendingIntent = PendingIntent.GetActivity(
                Platform.AppContext,
                0,
                intent,
                PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent
            );

            var builder = new NotificationCompat.Builder(Platform.AppContext, channelId)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.ic_notification) // add this icon
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            notificationManager.Notify(messageId++, builder.Build());
        }

        // 🔁 Called from MainActivity when notification is tapped
        public void RaiseTapped()
        {
            NotificationTapped?.Invoke(this, EventArgs.Empty);
        }

        public void ReceiveNotification(string title, string message)
        {
            // Optionally raise NotificationReceived
            NotificationReceived?.Invoke(this, EventArgs.Empty);
        }
    }
}

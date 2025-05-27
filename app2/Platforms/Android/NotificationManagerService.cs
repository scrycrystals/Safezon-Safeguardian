using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void SendNotification(string title, string message, DateTime? notifyTime = null)
        {
            var builder = new NotificationCompat.Builder(Platform.AppContext, channelId)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.ic_notification) // add this icon later
                .SetAutoCancel(true);

            notificationManager.Notify(messageId++, builder.Build());
        }

        public void ReceiveNotification(string title, string message)
        {
            // Not used yet
        }
    }
}

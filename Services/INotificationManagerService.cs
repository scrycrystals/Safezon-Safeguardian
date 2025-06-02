using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2.Services
{
    public interface INotificationManagerService
    {
        event EventHandler NotificationReceived;

        //void SendNotification(string title, string message, DateTime? notifyTime = null);
        void ReceiveNotification(string title, string message);

        void SendNotification(string title, string message, double latitude = 0, double longitude = 0);


        event EventHandler NotificationTapped;
    }
}

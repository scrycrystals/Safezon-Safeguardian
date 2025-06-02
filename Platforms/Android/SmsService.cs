using Android.Content;
using Android.Telephony;

namespace app2.Platforms.Android
{
    public class SmsService
    {
        public static void SendSMS(string phoneNumber, string message)
        {
            SmsManager smsManager = SmsManager.Default;
            IList<string> messageParts = smsManager.DivideMessage(message);
            smsManager.SendMultipartTextMessage(phoneNumber, null, messageParts, null, null);
        }
    }
}

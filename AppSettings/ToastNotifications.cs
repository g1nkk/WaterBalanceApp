using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Notifications;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace WaterBalance
{
    internal class ToastNotificationsTimer
    {
        List<int> notificationTime = new List<int>();
        private DispatcherTimer timer;

        ToastNotificationsTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Start();

            notificationTime.Add(9);
            notificationTime.Add(12);
            notificationTime.Add(14);
            notificationTime.Add(17);
            notificationTime.Add(20);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeToNotify())
            {
                ToastNotificationsManager.ShowToastNotification();
            }
        }

        bool timeToNotify()
        {
            var currentTime = DateTime.Now.Hour;
            foreach (var time in notificationTime)
            {
                if (time == currentTime)
                {
                    return true;
                }
            }
            return false;
        }

    }
    static class ToastNotificationsManager
    {
        static public void ShowToastNotification()
        {
            string[] notificationTexts = new string[] 
            {
                "Stay refreshed! Drink up.",
                "Don't forget to drink water!",
                "Stay hydrated! Take a water break.",
                "Keep hydrated!",
                "Remember to sip water.",
            };

            var rand = new Random();
            string randomText = notificationTexts[rand.Next(notificationTexts.Length)];

            var xml = $@"<toast>
                    <visual>
                     <binding template=""ToastImageAndText04"">
                          <text id=""1"">{randomText}</text>
                    </binding>
                    </visual>
                </toast>";
            var toastXml = new Windows.Data.Xml.Dom.XmlDocument();
            toastXml.LoadXml(xml);
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier("Water Balance").Show(toast);
        }

    }
}


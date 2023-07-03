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
    public class ToastNotifications
    {
        MainWindow mainWindow;
        private HashSet<int> notifiedHours = new HashSet<int>();
        private DispatcherTimer timer;

        public ToastNotifications(MainWindow mainWindow)
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Start();
            this.mainWindow = mainWindow;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (ShouldNotify())
            {
                Notify();
            }
        }

        private bool ShouldNotify()
        {
            bool isNotificationEnabled = mainWindow.userProfile.NotificationsEnabled;
            bool isSameHourAsArray = IsCurrentHourInArray();

            return isNotificationEnabled && isSameHourAsArray;
        }

        private bool IsCurrentHourInArray()
        {
            int currentHour = DateTime.Now.Hour;
            int[] notificationHours = GetNotificationHours();

            return notificationHours.Contains(currentHour) && !notifiedHours.Contains(currentHour);
        }

        private void Notify()
        {
            ToastNotificationsManager.ShowToastNotification();

            int currentHour = DateTime.Now.Hour;
            notifiedHours.Add(currentHour);
        }

        private int[] GetNotificationHours()
        {
            return new int[] { 8, 10, 12, 14, 16, 20 };
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


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
        PanelManager manager;
        private HashSet<int> notifiedHours = new HashSet<int>();
        private DispatcherTimer timer;

        public ToastNotifications(PanelManager manager)
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Start();
            this.manager = manager;
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
            bool isNotificationEnabled = manager.userProfile?.NotificationsEnabled ?? true;

            bool isSameHourAsArray = IsCurrentHourInArray();

            return isSameHourAsArray && isNotificationEnabled;
        }

        private bool IsCurrentHourInArray()
        {
            int currentHour = DateTime.Now.Hour;
            int[] notificationHours = GetNotificationHours();

            return notificationHours.Contains(currentHour) && !notifiedHours.Contains(currentHour);
        }

        private void Notify()
        {
            ToastNotificationsManager.ShowReminderNotification();

            int currentHour = DateTime.Now.Hour;
            notifiedHours.Add(currentHour);
        }

        private int[] GetNotificationHours()
        {
            return new int[] { 8, 10, 12, 14, 16, 19, 20 };
        }

    }
    static class ToastNotificationsManager
    {
        static public void ShowReminderNotification()
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

        static public void ShowAchievementCompletedNotification(Achievement achievement)
        {
            var title = $"Achievement \"{achievement.Name}\" completed!";

            var xml = $@"<toast>
                    <visual>
                     <binding template=""ToastImageAndText04"">
                          <text id=""1"">{title}</text>
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


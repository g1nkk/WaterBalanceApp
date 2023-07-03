using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using DevExpress.Mvvm.UI;

namespace WaterBalance
{
    public class SettingsPanel
    {
        MainWindow mainWindow;

        public ICommand ShowFoxButton { get; }
        public ICommand DeveloperPageButton { get; }
        public ICommand ClearAllDataButton { get; }
        public ICommand ToggleNotificationsButton { get; }

        public SettingsPanel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            ShowFoxButton = new RelayCommand(ShowFoxPictureClick);
            DeveloperPageButton = new RelayCommand(DeveloperPageButtonClick);
            ClearAllDataButton = new RelayCommand(ClearAllDataClick);
            ToggleNotificationsButton = new RelayCommand(ToggleNotifications);
        }

        void ToggleNotifications()
        {
            bool notificationsEnabled = !mainWindow.userProfile.NotificationsEnabled;
            mainWindow.userProfile.NotificationsEnabled = notificationsEnabled;

            string buttonContent = notificationsEnabled ? "disable notifications" : "enable notifications";
            mainWindow.NotificationsButton.Content = buttonContent;

            SaveAndLoadManager.SaveProfile(mainWindow.userProfile);
        }
        void ClearAllDataClick()
        {
            if (MessageBox.Show("You sure you want to clear all data and create new profile?",
            "Clear All Data", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                mainWindow.ShowPanel(mainWindow.panels[5]); // calculate panel
                mainWindow.HidePanel(mainWindow.panels[4]); // options menu
                mainWindow.HidePanel(mainWindow.panels[6]); // up and down buttons
            }
        }

        void ShowFoxPictureClick()
        {
            try
            {
                var rand = new Random();
                string apiUrl = "https://randomfox.ca/images/" + rand.Next(1, 100) + ".jpg";

                using (var client = new WebClient())
                {
                    byte[] imageData = client.DownloadData(apiUrl);

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = new MemoryStream(imageData);
                    bitmap.EndInit();

                    mainWindow.foxImage.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message);
            }
        }

        void DeveloperPageButtonClick()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/Gink83",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message);
            }
        }
    }
}

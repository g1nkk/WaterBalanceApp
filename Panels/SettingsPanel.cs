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

namespace WaterBalance
{
    public class SettingsPanel : MainWindow
    {
        public ICommand ShowFoxButton { get; }
        public ICommand RecalculateButton { get; }
        public ICommand DeveloperPageButton { get; }
        public ICommand ClearAllDataButton { get; }
        public ICommand LiterSliderValueChanged { get; }

        public SettingsPanel()
        {
            ShowFoxButton = new RelayCommand(ShowFoxPictureClick);
            RecalculateButton = new RelayCommand(RecalculateButtonClick);
            DeveloperPageButton = new RelayCommand(DeveloperPageButtonClick);
            ClearAllDataButton = new RelayCommand(ClearAllDataClick);
            LiterSliderValueChanged = new RelayCommand(LitersSlider_ValueChanged);
        }

        void RecalculateButtonClick()
        {
            showPanel(panels[5]); // calculate panel
            hidePanel(panels[4]); // options menu
            hidePanel(panels[6]); // up and down buttons
        }

        void ClearAllDataClick()
        {
            if (MessageBox.Show("You sure you want to clear all data?",
            "Clear All Data", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

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

                    foxImage.Source = bitmap;
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

        void LitersSlider_ValueChanged()
        {
            if (!IsInitialized) { return; }

            userProfile.DailyGoal = (float)litersSlider.Value;
            GoalLitersText.Content = userProfile.DailyGoal.ToString() + " L";
        }
    }
}

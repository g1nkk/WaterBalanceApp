using Microsoft.Toolkit.Mvvm.Input;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace WaterBalance
{
    public class AddWaterPanel
    {
        public ICommand AddWaterButton { get; }
        public ICommand AddWaterAmountButton { get; }

        private readonly MainWindow mainWindow;

        private bool IsAddWaterPanelVisible = false;

        public AddWaterPanel(MainWindow mainWindow) 
        {
            this.mainWindow = mainWindow;
            AddWaterButton = new RelayCommand(AddButton);
            AddWaterAmountButton = new RelayCommand<string>(AddWaterAmount);
        }

        void AddButton()
        {
            if (IsAddWaterPanelVisible)
            {
                HideAddPanel();
            }
            else
            {
                ShowAddPanel();
            }
        }

        void ShowAddPanel()
        {
            var gridAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase()
            };
            var backgroundAnimation = new DoubleAnimation
            {
                From = 0,
                To = 4,
                Duration = TimeSpan.FromMilliseconds(240),
                EasingFunction = new QuadraticEase()
            };

            mainWindow.addGrid.Visibility = Visibility.Visible;
            mainWindow.addGrid.BeginAnimation(Window.OpacityProperty, gridAnimation);

            mainWindow.mainMenuGrid.Effect.BeginAnimation(BlurEffect.RadiusProperty, backgroundAnimation);

            IsAddWaterPanelVisible = true;
        }

        void HideAddPanel()
        {
            var gridAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new QuadraticEase()
            };
            var backgroundAnimation = new DoubleAnimation
            {
                From = 4,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(170),
                EasingFunction = new QuadraticEase()
            };

            gridAnimation.Completed += (sender, e) =>
            {
                mainWindow.addGrid.Visibility = Visibility.Hidden;
            };

            mainWindow.addGrid.BeginAnimation(Window.OpacityProperty, gridAnimation);
            mainWindow.mainMenuGrid.Effect.BeginAnimation(BlurEffect.RadiusProperty, backgroundAnimation);

            IsAddWaterPanelVisible = false;
        }

        void AddWaterAmount(string addAmount)
        {
            int amount = Convert.ToInt32(addAmount);
            mainWindow.userProfile.TotalWaterConsumed += amount;
            mainWindow.calendarData.AddWater(amount);

            mainWindow.waterConsumedMl.Content = mainWindow.calendarData.GetTodayWaterAmount();
            UpdateWaterPercent();

            AddButton();

            SaveAndLoadManager.SaveCalendar(mainWindow.calendarData);
            SaveAndLoadManager.SaveProfile(mainWindow.userProfile);
        }

        void UpdateWaterPercent()
        {
            int goal = mainWindow.userProfile.DailyGoal;
            int consumed = mainWindow.calendarData.GetTodayWaterAmount();

            mainWindow.waterConsumedPercent.Content = (consumed/goal*100) + "%";
        }
    }
}

using Microsoft.Toolkit.Mvvm.Input;
using Serilog;
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
        readonly PanelManager manager;
        public ICommand AddWaterButton { get; }
        public ICommand AddWaterAmountButton { get; }

        private readonly MainWindow mainWindow;

        private bool IsAddWaterPanelVisible = false;

        public AddWaterPanel(MainWindow mainWindow, PanelManager manager)
        {
            this.manager = manager;
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

        public void PlayGoalCompletedAnimation()
        {
            //TODO: create nice checkbox animation


            //temp animation
            DoubleAnimation showAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(700),
                EasingFunction = new QuadraticEase()
            };

            mainWindow.goalCheckmark.BeginAnimation(MainWindow.OpacityProperty, showAnimation);
        }

        void AddWaterAmount(string addAmount)
        {
            int amount = Convert.ToInt32(addAmount);

            manager.userProfile.TotalWaterConsumed += amount;
            manager.calendarData.AddWater(amount);

            int todayAmount = manager.calendarData.GetTodayWaterAmount();

            Log.Information($"Water added: {amount}, today consumption: {todayAmount}");

            mainWindow.waterConsumedMl.Content = todayAmount + " ml";
            UpdateWaterPercent();

            AddButton();

            if (todayAmount >= manager.userProfile.DailyGoal)
            {
                PlayGoalCompletedAnimation();
                manager.calendarData.SetTodayGoalCompleted();

                if (!manager.achievementManager.IsAchievementCompleted(new FirstGoalAchievement()))
                {
                    manager.achievementManager.CompleteAchievement(new FirstGoalAchievement());
                }
            }

            manager.dataObserver.NotifySubscribers();

            SaveAndLoadManager.SaveCalendar(manager.calendarData);
            SaveAndLoadManager.SaveProfile(manager.userProfile);
        }

        void UpdateWaterPercent()
        {
            int goal = manager.userProfile.DailyGoal;
            float consumed = manager.calendarData.GetTodayWaterAmount();
            float percentage = consumed / goal * 100;

            mainWindow.waterConsumedPercent.Content = (int)percentage + "%";
        }
    }
}

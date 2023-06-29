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
    public class AddWaterPanel : MainWindow
    {
        public ICommand AddWaterButton { get; }

        private bool IsAddWaterPanelVisible = false;

        public AddWaterPanel() 
        {
            AddWaterButton = new RelayCommand(AddButton);
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

            addGrid.Visibility = Visibility.Visible;
            addGrid.BeginAnimation(OpacityProperty, gridAnimation);

            mainMenuGrid.Effect.BeginAnimation(BlurEffect.RadiusProperty, backgroundAnimation);

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
                addGrid.Visibility = Visibility.Hidden;
            };

            addGrid.BeginAnimation(OpacityProperty, gridAnimation);
            mainMenuGrid.Effect.BeginAnimation(BlurEffect.RadiusProperty, backgroundAnimation);

            IsAddWaterPanelVisible = false;
        }
    }
}

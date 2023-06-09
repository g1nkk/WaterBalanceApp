﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WaterBalance
{
    public class CalculatePanel
    {
        readonly PanelManager manager;
        readonly MainWindow mainWindow;

        private Grid[] calculatePanels = new Grid[5];
        private int currentCalculatePanelSelected = 0;

        public ICommand CalculateButton { get; }

        public ICommand WeightUpButton { get; }
        public ICommand WeightDownButton { get; }

        public ICommand HeightUpButton { get; }
        public ICommand HeightDownButton { get; }

        public ICommand AgeUpButton { get; }
        public ICommand AgeDownButton { get; }


        public CalculatePanel(MainWindow mainWindow, PanelManager manager)
        {
            this.manager = manager;
            this.mainWindow = mainWindow;

            calculatePanels[0] = mainWindow.CalculatePanel1;
            calculatePanels[1] = mainWindow.CalculatePanel2;
            calculatePanels[2] = mainWindow.CalculatePanel3;
            calculatePanels[3] = mainWindow.CalculatePanel4;
            calculatePanels[4] = mainWindow.CalculatePanel5;

            CalculateButton = new RelayCommand(ContinueButton);

            WeightUpButton = new RelayCommand(WeightUp);
            WeightDownButton = new RelayCommand(WeightDown);

            HeightDownButton = new RelayCommand(HeightDown);
            HeightUpButton = new RelayCommand(HeightUp);

            AgeDownButton = new RelayCommand(AgeDown);
            AgeUpButton = new RelayCommand(AgeUp);
        }


        void WeightUp()
        {
            if (Convert.ToInt32(mainWindow.calculateKg.Content) < 200)
            {
                mainWindow.calculateKg.Content = Convert.ToInt32(mainWindow.calculateKg.Content) + 1;
            }
        }
        void WeightDown()
        {
            if (Convert.ToInt32(mainWindow.calculateKg.Content) > 10)
            {
                mainWindow.calculateKg.Content = Convert.ToInt32(mainWindow.calculateKg.Content) - 1;
            }
        }
        void HeightUp()
        {
            if (Convert.ToInt32(mainWindow.calculateHeight.Content) < 300)
            {
                mainWindow.calculateHeight.Content = Convert.ToInt32(mainWindow.calculateHeight.Content) + 1;
            }
        }
        void HeightDown()
        {
            if (Convert.ToInt32(mainWindow.calculateHeight.Content) > 10)
            {
                mainWindow.calculateHeight.Content = Convert.ToInt32(mainWindow.calculateHeight.Content) - 1;
            }
        }
        void AgeUp()
        {
            if (Convert.ToInt32(mainWindow.calculateAge.Content) < 130)
            {
                mainWindow.calculateAge.Content = Convert.ToInt32(mainWindow.calculateAge.Content) + 1;
            }
        }
        void AgeDown()
        {
            if (Convert.ToInt32(mainWindow.calculateAge.Content) > 3)
            {
                mainWindow.calculateAge.Content = Convert.ToInt32(mainWindow.calculateAge.Content) - 1;
            }
        }

        void ContinueButton()
        {
            if (currentCalculatePanelSelected >= calculatePanels.Length)
            {
                return;
            }

            if (currentCalculatePanelSelected == calculatePanels.Length - 1)
            {
                if (AllFieldsFilled(currentCalculatePanelSelected))
                {
                    HideInvalidLabelIfVisible();
                    int goal = CalculateWaterGoal(); // goal in liters
                    manager.CreateNewData(goal);
                    manager.SetupUserDependentComponents();
                    PanelManager.HidePanel(calculatePanels[currentCalculatePanelSelected]);
                    PanelManager.HidePanel(manager.panels[5]); // calculate panel
                    PanelManager.ShowPanel(manager.panels[0]); // main menu
                    PanelManager.ShowPanel(manager.panels[6]); // up and down buttons
                    ResetPanelsVisibility();
                    manager.currentPanelSelected = 0;
                }
                else
                {
                    ShowInvalidLabel();
                }
            }
            else
            {
                if (AllFieldsFilled(currentCalculatePanelSelected))
                {
                    HideInvalidLabelIfVisible();
                    currentCalculatePanelSelected++;
                    PanelManager.HidePanel(calculatePanels[currentCalculatePanelSelected - 1]);
                    PanelManager.ShowPanel(calculatePanels[currentCalculatePanelSelected]);
                }
                else
                {
                    ShowInvalidLabel();
                }
            }
        }

        void HideInvalidLabelIfVisible()
        {
            if (mainWindow.InvalidLabel.Opacity > 0)
            {
                mainWindow.InvalidLabel.BeginAnimation(Window.OpacityProperty, PanelManager.hideAnimation);
            }
        }

        void ShowInvalidLabel()
        {
            mainWindow.InvalidLabel.BeginAnimation(Window.OpacityProperty, PanelManager.showAnimation);
        }

        int CalculateWaterGoal()
        {
            /*
            goal = BMR * activity factor

            BMR for men = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age)
            BMR for women = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age)
            */

            double BMR;
            float factor = GetActivityFactor();
            int weight = Convert.ToInt32(mainWindow.calculateKg.Content);
            int height = Convert.ToInt32(mainWindow.calculateHeight.Content);
            int age = Convert.ToInt32(mainWindow.calculateAge.Content);

            if (mainWindow.maleChecked.IsChecked == true) // for male
            {
                BMR = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age);
            }
            else // for female
            {
                BMR = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age);
            }
            int goalMl = (int)(BMR * factor);

            return (goalMl / 100) * 100; ; // goal in ml and round
        }
        float GetActivityFactor()
        {
            if (mainWindow.sedentary.IsChecked == true)
            {
                return 1.2f;
            }
            else if (mainWindow.lightly.IsChecked == true)
            {
                return 1.375f;
            }
            else if (mainWindow.moderately.IsChecked == true)
            {
                return 1.55f;
            }
            else if (mainWindow.highly.IsChecked == true)
            {
                return 1.9f;
            }
            else return 1;
        }

        void ResetPanelsVisibility()
        {
            foreach (var panel in calculatePanels)
            {
                panel.Visibility = Visibility.Hidden;
            }
            calculatePanels[0].Visibility = Visibility.Visible;
            currentCalculatePanelSelected = 0;
        }

        bool IsActivitySelected()
        {
            return mainWindow.sedentary.IsChecked == true || mainWindow.lightly.IsChecked == true
                || mainWindow.moderately.IsChecked == true || mainWindow.highly.IsChecked == true;
        }

        bool IsNameFieldValid()
        {
            return new Regex(@"^[a-zA-Zа-яА-Я\s\-]{1,12}").IsMatch(mainWindow.NameTextBox.Text);
        }

        bool IsGenderSelected()
        {
            return mainWindow.maleChecked.IsChecked == true || mainWindow.femaleChecked.IsChecked == true;
        }


        bool AllFieldsFilled(int currentPanel)
        {
            switch (currentPanel)
            {
                case 0:
                    return true;
                case 1:
                    if (IsNameFieldValid()) return true;
                    else return false;
                case 2:
                    if (IsGenderSelected()) return true;
                    else return false;
                case 3:
                    return true;
                case 4:
                    if (IsActivitySelected()) return true;
                    else return false;
            }
            return false;
        }
    }
}

using System;
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

namespace WaterBalance
{
    public class CalculatePanel : MainWindow
    {
        private Grid[] calculatePanels = new Grid[5];
        private int currentCalculatePanelSelected = 0;

        public ICommand CalculateButton;

        public ICommand WeightUpButton;
        public ICommand WeightDownButton;

        public ICommand HeightUpButton;
        public ICommand HeightDownButton;

        public ICommand AgeUpButton;
        public ICommand AgeDownButton;


        public CalculatePanel()
        {
            calculatePanels[0] = CalculatePanel1;
            calculatePanels[1] = CalculatePanel2;
            calculatePanels[2] = CalculatePanel3;
            calculatePanels[3] = CalculatePanel4;
            calculatePanels[4] = CalculatePanel5;

            CalculateButton = new RelayCommand(CheckWaterFields);

            WeightUpButton = new RelayCommand(WeightUp);
            WeightDownButton = new RelayCommand(WeightDown);

            HeightDownButton = new RelayCommand(HeightDown);
            HeightUpButton = new RelayCommand(HeightUp);

            AgeDownButton = new RelayCommand(AgeDown);
            AgeUpButton = new RelayCommand(AgeUp);
        }


        void WeightUp()
        {
            if (Convert.ToInt32(calculateKg.Content) < 200)
            {
                calculateKg.Content = Convert.ToInt32(calculateKg.Content) + 1;
            }
        }
        void WeightDown()
        {
            if (Convert.ToInt32(calculateKg.Content) > 10)
            {
                calculateKg.Content = Convert.ToInt32(calculateKg.Content) - 1;
            }
        }
        void HeightUp()
        {
            if (Convert.ToInt32(calculateHeight.Content) < 300)
            {
                calculateHeight.Content = Convert.ToInt32(calculateHeight.Content) + 1;
            }
        }
        void HeightDown()
        {
            if (Convert.ToInt32(calculateHeight.Content) > 10)
            {
                calculateHeight.Content = Convert.ToInt32(calculateHeight.Content) - 1;
            }
        }
        void AgeUp()
        {
            if (Convert.ToInt32(calculateAge.Content) < 130)
            {
                calculateAge.Content = Convert.ToInt32(calculateAge.Content) + 1;
            }
        }
        void AgeDown()
        {
            if (Convert.ToInt32(calculateAge.Content) > 3)
            {
                calculateAge.Content = Convert.ToInt32(calculateAge.Content) - 1;
            }
        }

        float CalculateWaterGoal()
        {
            /*
            goal = BMR * activity factor

            BMR for men = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age)
            BMR for women = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age)
            */

            double BMR;
            float factor = GetActivityFactor();
            int weight = Convert.ToInt32(calculateKg.Content);
            int height = Convert.ToInt32(calculateHeight.Content);
            int age = Convert.ToInt32(calculateAge.Content);

            if (maleChecked.IsChecked == true) // for male
            {
                BMR = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age);
            }
            else // for female
            {
                BMR = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age);
            }

            int goalMl = (int)(BMR * factor);

            return (float)Math.Round((double)goalMl / 1000, 1); // convert ml to l and round
        }
        float GetActivityFactor()
        {
            if (sedentary.IsChecked == true)
            {
                return 1.2f;
            }
            else if (lightly.IsChecked == true)
            {
                return 1.375f;
            }
            else if (moderately.IsChecked == true)
            {
                return 1.55f;
            }
            else if (highly.IsChecked == true)
            {
                return 1.9f;
            }
            else return 1;
        }

        void CheckWaterFields()
        {
            if (currentCalculatePanelSelected < calculatePanels.Length - 1)
            {
                if (AllFieldsFilled(currentCalculatePanelSelected))
                {
                    if (InvalidLabel.Opacity > 0)
                        InvalidLabel.BeginAnimation(OpacityProperty, hideAnimation);

                    currentCalculatePanelSelected++;
                    hidePanel(calculatePanels[currentCalculatePanelSelected - 1]);
                    showPanel(calculatePanels[currentCalculatePanelSelected]);
                }
                else
                {
                    InvalidLabel.BeginAnimation(OpacityProperty, showAnimation);
                }
            }
            else
            {
                float goal = CalculateWaterGoal(); // goal in liters

                userProfile = new ProfileData(goal, NameTextBox.Text);

                SetupUserDependentComponents();

                hidePanel(panels[5]); // calculate panel
                showPanel(panels[0]); // main menu
                showPanel(panels[6]); // up and down buttons

                currentPanelSelected = 0;

                foreach (var panel in calculatePanels)
                {
                    panel.Visibility = Visibility.Hidden;
                }
                calculatePanels[0].Visibility = Visibility.Visible;
                currentCalculatePanelSelected = 0;
            }
        }

        bool IsActivitySelected()
        {
            return sedentary.IsChecked == true || lightly.IsChecked == true
                || moderately.IsChecked == true || highly.IsChecked == true;
        }

        bool IsNameFieldValid()
        {
            return new Regex(@"^[a-zA-Zа-яА-Я\s\-]{1,12}").IsMatch(NameTextBox.Text);
        }

        bool IsGenderSelected()
        {
            return maleChecked.IsChecked == true || femaleChecked.IsChecked == true;
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WaterBalance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Grid[] panels = new Grid[5];
        private Button[] ControlButtons = new Button[4];
        private int currentPanelSelected = 4;

        private bool isGridVisible = false;

        private float LitersGoal = 3;


        public MainWindow()
        {
            InitializeComponent();

            GoalLitersText.Content = LitersGoal.ToString("F2") + " L";
            litersSlider.Value = LitersGoal;

            ControlButtons[0] = waterControlButton;
            ControlButtons[1] = calendarControlButton;
            ControlButtons[2] = achievementControlButton;
            ControlButtons[3] = settingsControlButtons;

            panels[0] = mainMenuGrid;
            panels[1] = addGrid;
            // calendar panels[2] = ;
            // achivements panels[3] = ;
            panels[4] = optionsGrid;

        }

        public void ControlButtonClick(object sender, RoutedEventArgs e)
        {
            var pressedbutton = (Button)sender;
            if (currentPanelSelected != Convert.ToInt32(pressedbutton.Tag))
            {
                hidePanel(currentPanelSelected);
                currentPanelSelected = Convert.ToInt32(pressedbutton.Tag); // new panel
                showPanel(currentPanelSelected);
            }
        }

        void hidePanel(int panelIndex)
        {
            var hideAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase()
            };

            panels[panelIndex]. BeginAnimation(OpacityProperty, hideAnimation);
            Thread.Sleep(300);
            panels[panelIndex].Visibility = Visibility.Hidden;
        }

        void showPanel(int panelIndex)
        {
            var showAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(450),
                EasingFunction = new QuadraticEase()
            };

            panels[panelIndex].Visibility = Visibility.Visible;
            panels[panelIndex].BeginAnimation(OpacityProperty, showAnimation);
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MinimizeButton(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void AddButton(object sender, RoutedEventArgs e)
        {
            if (isGridVisible)
            {
                HideGrid();
            }
            else
            {
                ShowGrid();
            }
        }

        private void ShowGrid()
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
                To = 9,
                Duration = TimeSpan.FromMilliseconds(240),
                EasingFunction = new QuadraticEase()
            };

            addGrid.Visibility = Visibility.Visible;
            addGrid.BeginAnimation(OpacityProperty, gridAnimation);

            mainMenuGrid.Effect.BeginAnimation(BlurEffect.RadiusProperty, backgroundAnimation);

            isGridVisible = true;
        }

        private void HideGrid()
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
                From = 9,
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

            isGridVisible = false;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void getFoxPicture(object sender, RoutedEventArgs e)
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

        private void developerPageButton(object sender, RoutedEventArgs e)
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

        private void litersSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(!IsInitialized) { return; }

            LitersGoal = (float)litersSlider.Value;
            GoalLitersText.Content = LitersGoal.ToString("F2") + " L";
        }

        private void calculateWeightUp_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(calculateKg.Content) < 200)
            {
                calculateKg.Content = Convert.ToInt32(calculateKg.Content) + 1;
            }
        }
        private void calculateWeightDown_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(calculateKg.Content) > 10)
            {
                calculateKg.Content = Convert.ToInt32(calculateKg.Content) - 1;
            }
        }
        private void calculateHeightUp_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(calculateHeight.Content) < 300)
            {
                calculateHeight.Content = Convert.ToInt32(calculateHeight.Content) + 1;
            }
        }
        private void calculateHeightDown_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(calculateHeight.Content) > 10)
            {
                calculateHeight.Content = Convert.ToInt32(calculateHeight.Content) - 1;
            }
        }
        private void calculateAgeUp_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(calculateAge.Content) < 130)
            {
                calculateAge.Content = Convert.ToInt32(calculateAge.Content) + 1;
            }
        }
        private void calculateAgeDown_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(calculateAge.Content) > 3)
            {
                calculateAge.Content = Convert.ToInt32(calculateAge.Content) - 1;
            }
        }

        float calculateWaterGoal()
        {
            /*
            goal = BMR * activity factor

            BMR for men = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age)
            BMR for women = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age)
            */

            double BMR;
            float factor = getActivityFactor();
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

            return (float)BMR * factor;
        }
        float getActivityFactor()
        {
            if(sedentary.IsChecked == true)
            {
                return 1.2f;
            }
            else if(lightly.IsChecked == true)
            {
                return 1.375f;
            }
            else if(moderately.IsChecked == true)
            {
                return 1.55f;
            }
            else if(highly.IsChecked == true)
            {
                return 1.9f;
            }
            else return 1;
        }

        private void CalculateButton(object sender, RoutedEventArgs e)
        {
            lit.Content = calculateWaterGoal();
        }
    }
}


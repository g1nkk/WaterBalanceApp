using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private int currentPanelSelected=0;

        private bool isGridVisible = false;


        public MainWindow()
        {
            InitializeComponent();

            ControlButtons[0] = waterControlButton;
            ControlButtons[1] = calendarControlButton;
            ControlButtons[2] = achievementControlButton;
            ControlButtons[3] = settingsControlButtons;

            panels[0] = mainMenuGrid;
            panels[1] = addGrid;
            
        }

        public void ControlButtonClick(object sender, RoutedEventArgs e)
        {
            var pressedbutton = (Button)sender;
            if(currentPanelSelected != Convert.ToInt32(pressedbutton.Tag))
            {
                hidePanel(currentPanelSelected);
                currentPanelSelected = Convert.ToInt32(pressedbutton.Tag); // new panel
                showPanel(currentPanelSelected);
            }
        }

        void hidePanel(int panelIndex)
        {

        }

        void showPanel(int panelIndex)
        {

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
    }
}


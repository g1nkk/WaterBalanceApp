using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace WaterBalance
{
    public partial class MainWindow : Window
    {
        protected int currentPanelSelected = 0;

        private Grid startupPanel;

        protected ProfileData userProfile;
        private CalendarData calendarData;
        private AchievementManager achievementManager;

        protected Grid[] panels = new Grid[7];

        readonly protected DoubleAnimation showAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromMilliseconds(450),
            EasingFunction = new QuadraticEase()
        };

        readonly protected DoubleAnimation hideAnimation = new DoubleAnimation
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromMilliseconds(300),
            EasingFunction = new QuadraticEase()
        };

        public MainWindow()
        {
            InitializeComponent();
            SetupDefaultComponents();
            LoadAndCheckData();
        }

        public void hidePanel(Grid panel)
        {
            panel.BeginAnimation(OpacityProperty, hideAnimation);;
            panel.Visibility = Visibility.Hidden;
        }

        public void showPanel(Grid panel)
        {
            panel.Visibility = Visibility.Visible;
            panel.BeginAnimation(OpacityProperty, showAnimation);
        }

        void LoadAndCheckData()
        {
            if (SaveAndLoadManager.SaveFlesExists())
            {
                SaveAndLoadManager.LoadAllData(out userProfile, out calendarData, out achievementManager);
                startupPanel = panels[0]; // main menu
            }
            else
            {
                startupPanel = panels[5]; // calculate panel
            }
        }

        protected void SetupUserDependentComponents()
        {
            GoalLitersText.Content = userProfile.DailyGoal.ToString("F2") + " L";
            litersSlider.Value = userProfile.DailyGoal;
            MainMenuGoalLabel.Content = userProfile.DailyGoal.ToString("F1") + " L";
            MainMenuNameLabel.Content = userProfile.Name;
        }
        void SetupDefaultComponents()
        {
            DataContext = new PanelManager();

            //panelManager = new PanelManager(this);

            panels[0] = mainMenuGrid;
            panels[1] = addGrid;
            // calendar panels[2] = ;
            // achivements panels[3] = ;
            panels[4] = optionsGrid;
            panels[5] = calculateGrid;
            panels[6] = ContolButtons;
        }
 

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            showPanel(startupPanel);
        }
    }
}


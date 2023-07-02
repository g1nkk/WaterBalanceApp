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
        public int currentPanelSelected = 0;

        private Grid startupPanel;

        public ProfileData userProfile;
        public CalendarData calendarData;
        public AchievementManager achievementManager;

        PanelManager panelManager;

        public Grid[] panels = new Grid[7];

        public MainWindow()
        {
            InitializeComponent();
            SetupDefaultComponents();
            LoadAndCheckData();
        }

        readonly public static DoubleAnimation showAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromMilliseconds(450),
            EasingFunction = new QuadraticEase()
        };

        readonly public static DoubleAnimation hideAnimation = new DoubleAnimation
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromMilliseconds(300),
            EasingFunction = new QuadraticEase()
        };

        public void CreateNewData(int goal)
        {
            userProfile = new ProfileData(goal, NameTextBox.Text);

            calendarData = new CalendarData();

            achievementManager = new AchievementManager();

            SaveAndLoadManager.SaveAllData(userProfile, calendarData, achievementManager);

            SetupUserDependentComponents();
        }

        public void HidePanel(Grid panel)
        {
            panel.BeginAnimation(OpacityProperty, hideAnimation);;
            panel.Visibility = Visibility.Hidden;
        }

        public void ShowPanel(Grid panel)
        {
            panel.Visibility = Visibility.Visible;
            panel.BeginAnimation(OpacityProperty, showAnimation);
        }

        void LoadAndCheckData()
        {
            if (SaveAndLoadManager.SaveFlesExists())
            {
                SaveAndLoadManager.LoadAllData(out userProfile, out calendarData, out achievementManager);
                SetupUserDependentComponents();
                startupPanel = panels[0]; // main menu
            }
            else
            {
                startupPanel = panels[5]; // calculate panel
            }
        }

        public void SetupUserDependentComponents()
        {
            GoalLitersText.Content = userProfile.DailyGoal.ToString() + " ml";
            MainMenuGoalLabel.Content = userProfile.DailyGoal.ToString() + " ml";
            MainMenuNameLabel.Content = userProfile.Name;
        }
        void SetupDefaultComponents()
        {
            panels[0] = mainMenuGrid;
            panels[1] = addGrid;
            // calendar panels[2] = ;
            // achivements panels[3] = ;
            panels[4] = optionsGrid;
            panels[5] = calculateGrid;
            panels[6] = ContolButtons;

            DataContext = panelManager = new PanelManager(this);
        }
 

        void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowPanel(startupPanel);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterBalance;
using Serilog;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows;
using Microsoft.Toolkit.Mvvm.Input;

namespace WaterBalance
{
    //vievmodel
    public class PanelManager
    {
        public ICommand Drag;
        public ICommand WindowLoaded;

        MainWindow mainWindow;

        public Grid[] panels = new Grid[7];
        public int currentPanelSelected = 0;

        public Grid startupPanel;

        public ProfileData userProfile;
        public CalendarData calendarData;
        public AchievementManager achievementManager;
        public ToastNotifications notifications;

        DataObserver dataObserver = new DataObserver();

        public Dictionary<string, object> ClassInstances { get; } = new Dictionary<string, object>();

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

        public PanelManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            SetupComponents();
        }

        void SetupComponents()
        {
            SetupDefaultComponents();
            SetupClassInstances();
            SetupDataObserver();
            LoadAndCheckData();
            SetupLogger();
        }

        void SetupDataObserver()
        {
            dataObserver.Subscribe((AchievementsPanel)ClassInstances["AchievementPanel"]);
            dataObserver.Subscribe((CalendarPanel)ClassInstances["CalendarPanel"]);
        }

        void SetupLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt")
                .CreateLogger();  
        }

        void SetupClassInstances()
        {
            AddClassInstance<AddWaterPanel>("AddWaterPanel", mainWindow, this);
            AddClassInstance<CalculatePanel>("CalсulatePanel", mainWindow, this);
            AddClassInstance<ControlButtonsPanel>("ControlButtonsPanel", mainWindow, this);
            AddClassInstance<SettingsPanel>("SettingsPanel", mainWindow, this);
            AddClassInstance<CalendarPanel>("CalendarPanel", mainWindow, this);
            AddClassInstance<AchievementsPanel>("AchievementPanel", mainWindow, this);
        }


        private void AddClassInstance<T>(string instanceName, MainWindow mainWindow, PanelManager manager) where T : class
        {
            T instance = (T)Activator.CreateInstance(typeof(T), mainWindow, manager);
            ClassInstances[instanceName] = instance;
        }

        public void CreateNewData(int goal)
        {
            userProfile = new ProfileData(goal, mainWindow.NameTextBox.Text);

            calendarData = new CalendarData();

            achievementManager = new AchievementManager();

            Log.Information($"New profile created with name: {userProfile.Name}");

            SaveAndLoadManager.SaveAllData(userProfile, calendarData, achievementManager);

            dataObserver.NotifySubscribers();

            SetupUserDependentComponents();
        }

        public static void HidePanel(Grid panel)
        {
            panel.BeginAnimation(Window.OpacityProperty, hideAnimation); ;
            panel.Visibility = Visibility.Hidden;
        }

        public static void ShowPanel(Grid panel)
        {
            panel.Visibility = Visibility.Visible;
            panel.BeginAnimation(Window.OpacityProperty, showAnimation);
        }

        public void ShowStartupPanel()
        {
            ShowPanel(startupPanel);
        }

        void LoadAndCheckData()
        {
            if (SaveAndLoadManager.SaveFilesExists())
            {
                SaveAndLoadManager.LoadAllData(out userProfile, out calendarData, out achievementManager);
                dataObserver.NotifySubscribers();
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
            if (calendarData.IsDayEmpty(DateTime.Now.Day))
            {
                mainWindow.waterConsumedMl.Content = "0 ml";
                mainWindow.waterConsumedPercent.Content = "0%";
            }
            else
            {
                mainWindow.waterConsumedMl.Content = calendarData.GetTodayWaterAmount() + " ml";
                mainWindow.waterConsumedPercent.Content = TempUpdatePercentage();
            }

            mainWindow.GoalLitersText.Content = userProfile.DailyGoal.ToString() + " ml";
            mainWindow.MainMenuGoalLabel.Content = userProfile.DailyGoal.ToString() + " ml";
            mainWindow.MainMenuNameLabel.Content = userProfile.Name;
            mainWindow.NotificationsButton.Content = userProfile.NotificationsEnabled ? "disable notifications" : "enable notifications";
        }

        string TempUpdatePercentage()
        {
            int goal = userProfile.DailyGoal;
            float consumed = calendarData.GetTodayWaterAmount();
            float percentage = consumed / goal * 100;
            percentage = Convert.ToInt32(percentage);

            return percentage.ToString() + "%";
        }

        public void SetupDefaultComponents()
        {
            //Drag = new RelayCommand(Window_MouseDown);
            //WindowLoaded = new RelayCommand(Window_Loaded);

            panels[0] = mainWindow.mainMenuGrid;
            panels[1] = mainWindow.addGrid;
            panels[2] = mainWindow.calendarGrid;
            panels[3] = mainWindow.achievementsGrid;
            panels[4] = mainWindow.optionsGrid;
            panels[5] = mainWindow.calculateGrid;
            panels[6] = mainWindow.ContolButtons;

            notifications = new ToastNotifications(this);

            mainWindow.DataContext = this;
        }
    }
}

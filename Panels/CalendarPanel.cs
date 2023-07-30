using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace WaterBalance
{
    internal class CalendarPanel : IDataSubscriber
    {
        readonly PanelManager manager;
        readonly MainWindow mainWindow;
        private readonly DispatcherTimer NextDayTimer;

        public CalendarPanel(MainWindow mainWindow, PanelManager manager)
        {          
            this.manager = manager;
            this.mainWindow = mainWindow;
            NextDayTimer = new DispatcherTimer();
            NextDayTimer.Tick += Timer_Tick;
            NextDayTimer.Start();

            SetupCalendar();
        }

        public void SetupCalendar()
        {
            SetupInterface();
            //next steps
        }

        public void Update()
        {
            CheckTodayDate();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (manager.calendarData?.IsNewDay() ?? false)
            {
                CheckTodayDate();
            }
        }

        void CheckTodayDate()
        {
            UpdateCalendarButtons();
            manager.calendarData.UpdateDate();
            UpdateStreak();
            manager.SetupUserDependentComponents();
        }

        void UpdateStreak()
        {
            if(manager.calendarData.IsTodayGoalCompleted())
            {
                manager.userProfile.CurrentStreak++;

                if(manager.userProfile.CurrentStreak > manager.userProfile.MaxStreak)
                {
                    manager.userProfile.MaxStreak = manager.userProfile.CurrentStreak;
                }
            }
        }

        void SetupInterface()
        {
            mainWindow.calendarMonth.Content = DateTime.Now.ToString("MMMM");
        }

        private void UpdateCalendarButtons()
        {
            int dayCount = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            const int buttonsPerRow = 7; 
            const int buttonSize = 40; 
            const int spacing = 5;

            int rows = dayCount / buttonsPerRow + 1; 

            Style buttonStyle = Application.Current.Resources["calendarDayButton"] as Style;

            mainWindow.calendarDays.Children.Clear();

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < buttonsPerRow; column++)
                {
                    int index = row * buttonsPerRow + column;
                    if (index >= dayCount)
                        break;

                    Button button = new Button();
                    button.Style = buttonStyle;
                    button.Width = buttonSize;
                    button.Height = buttonSize;
                    button.Content = (index + 1).ToString();
                    button.Margin = new Thickness(column * (buttonSize + spacing), row * (buttonSize + spacing), 0, 0);
                    button.HorizontalAlignment = HorizontalAlignment.Left;
                    button.VerticalAlignment = VerticalAlignment.Top;
                    button.Background = Brushes.CornflowerBlue;
                    button.Foreground = Brushes.White;
                    button.FontSize = 16;
                    button.FontWeight = FontWeights.Bold;
                    button.Click += Button_Click;

                    if (manager.calendarData.IsDayEmpty(index))
                    {
                        button.IsEnabled = false;
                        button.Opacity = .4;
                    }
                    else if (manager.calendarData.IsDayGoalCompleted(index))
                    {
                        button.Background = Brushes.SpringGreen;
                    }

                    mainWindow.calendarDays.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int dayNum = Convert.ToInt32(button.Content)-1;


            PanelManager.ShowPanel(mainWindow.DaySelectedPanel);
            mainWindow.DayNum.Content = dayNum+1;
            mainWindow.DayGoal.Content = manager.calendarData.IsDayGoalCompleted(dayNum);
            mainWindow.DayWater.Content = manager.calendarData.GetDayWaterAmount(dayNum);
        }





    }
}

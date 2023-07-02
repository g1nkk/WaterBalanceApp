using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace WaterBalance
{
    internal class CalendarPanel
    {
        readonly MainWindow mainWindow;

        public CalendarPanel(MainWindow mainWindow)
        {          
            this.mainWindow = mainWindow;
        }

        public void SetupCalendar()
        {
            SetupInterface();

        }

        void SetupInterface()
        {
            mainWindow.calendarMonth.Content = DateTime.Now.Month.ToString();
            CreateDaysButtons(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }

        private void CreateDaysButtons(int dayCount)
        {
            const int buttonsPerRow = 7; 
            const int buttonSize = 40; 
            const int spacing = 5;

            int rows = dayCount / buttonsPerRow + 1; 

            Style buttonStyle = Application.Current.Resources["calendarDayButton"] as Style;

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

                    if (mainWindow.calendarData.Month.IsDayEmpty(index))
                    {
                        button.IsEnabled = false;
                        button.Opacity = .4;
                    }
                    else if (mainWindow.calendarData.Month.IsGoalCompleted(index))
                    {
                        button.Background = Brushes.SpringGreen;
                    }

                    mainWindow.calendarDays.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Обработка нажатия кнопки
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace WaterBalance.Panels
{
    internal class CalendarPanel
    {
        readonly MainWindow mainWindow;


        public CalendarPanel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            CreateButtons(31);
        }

        private void CreateButtons(int buttonCount)
        {
            const int buttonsPerRow = 7; // Количество кнопок в ряду
            const int buttonSize = 40; // Размер кнопки
            const int spacing = 5; // Расстояние между кнопками

            int rows = buttonCount / buttonsPerRow + 1; // Количество рядов

            Style buttonStyle = Application.Current.Resources["defaultWithBackground"] as Style;
            

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < buttonsPerRow; column++)
                {
                    int index = row * buttonsPerRow + column;
                    if (index >= buttonCount)
                        break;

                    Button button = new Button();
                    button.Style = buttonStyle;
                    button.Width = buttonSize;
                    button.Height = buttonSize;
                    button.Content = (index + 1).ToString();
                    button.Margin = new Thickness(column * (buttonSize + spacing), row * (buttonSize + spacing), 0, 0);
                    button.HorizontalAlignment = HorizontalAlignment.Left;
                    button.VerticalAlignment = VerticalAlignment.Top;
                    button.Background = Brushes.DarkBlue;
                    button.Foreground = Brushes.White;
                    button.FontSize = 16;
                    button.FontWeight = FontWeights.Bold;
                    button.Click += Button_Click;

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

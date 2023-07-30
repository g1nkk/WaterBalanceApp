using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;

namespace WaterBalance
{
    class AchievementsPanel : IDataSubscriber
    {
        PanelManager manager;
        MainWindow mainWindow;

        public AchievementsPanel(MainWindow mainWindow, PanelManager manager)
        {
            this.manager = manager;
            this.mainWindow = mainWindow;
        }
        public void UpdateAchievementPanel()
        {
            mainWindow.achievementStackPanel.Children.Clear();

            var converter = new BrushConverter();

            string lockedImage = "icons/locked.png"; 
            Uri lockedImageUri = new Uri("C:\\Users\\Kirill\\source\\repos\\WaterBalance\\icons\\locked.png");

            string unlockedImage = "icons/demoCheckmark.png";
            Uri unlockedImageUri = new Uri(unlockedImage, UriKind.Relative);

            foreach (var achievement in manager.achievementManager.AchievementList)
            {
                var grid = new Grid();

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) }); // For an image
                grid.ColumnDefinitions.Add(new ColumnDefinition()); // For name and description

                // for image
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                // for name
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                var panel = new Border()
                {
                    CornerRadius = new CornerRadius(25),
                    Margin = new Thickness(6),
                    Padding = new Thickness(10)
                };
                var image = new Image()
                {
                    Width = 30,
                    Height = 30
                };

                if (manager.achievementManager.IsAchievementCompleted(achievement))
                {
                    panel.Background = (Brush)converter.ConvertFromString("#d9ffd6");
                    image.Source = new BitmapImage(lockedImageUri);
                }
                else
                {
                    panel.Background = Brushes.White;
                    image.Source = new BitmapImage(lockedImageUri);
                }

                var title = new TextBlock
                {

                    Foreground = (Brush)converter.ConvertFromString("#FF1E55AF"),
                    FontSize = 16,
                    Text = achievement.Name,
                    TextWrapping = TextWrapping.Wrap,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 5, 0, 5)
                };
                var description = new TextBlock
                {
                    Foreground = (Brush)converter.ConvertFromString("#FF5B8FC8"),
                    FontSize = 14,
                    Text = achievement.Description,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                Grid.SetRow(image, 0);
                Grid.SetColumn(image, 0);
                Grid.SetRowSpan(image, 2); 
                grid.Children.Add(image);

                Grid.SetRow(title, 0);
                Grid.SetColumn(title, 1);
                grid.Children.Add(title);

                Grid.SetRow(description, 2);
                Grid.SetColumn(description, 1);
                grid.Children.Add(description);

                panel.Child = grid;

                mainWindow.achievementStackPanel.Children.Add(panel);
            }
        }
        public void Update()
        {
            UpdateAchievementPanel();
        }
    }
}

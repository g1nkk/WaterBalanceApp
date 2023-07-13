using Serilog;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace WaterBalance
{
    public partial class MainWindow : Window
    {

        PanelManager panelManager;

        public MainWindow()
        {
         
            InitializeComponent();
            panelManager = new PanelManager(this);
        }

        void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Log.Information("App opened");
            panelManager.ShowStartupPanel();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Serilog;

namespace WaterBalance
{
    public class ControlButtonsPanel
    {
        PanelManager manager;
        private Button[] controlButtons = new Button[4];

        readonly MainWindow mainWindow;

        public ICommand Close { get; }
        public ICommand Minimize { get; }
        public ICommand ControlButtonSelect { get; }

        public ControlButtonsPanel(MainWindow mainWindow, PanelManager manager)
        {
            this.manager = manager;
            this.mainWindow = mainWindow;

            controlButtons[0] = mainWindow.waterControlButton;
            controlButtons[1] = mainWindow.calendarControlButton;
            controlButtons[2] = mainWindow.achievementControlButton;
            controlButtons[3] = mainWindow.settingsControlButtons;

            Close = new RelayCommand(CloseButton);
            Minimize = new RelayCommand(MinimizeButton);
            ControlButtonSelect = new RelayCommand<string>(ControlButtonClick);
        }

        void CloseButton()
        {
            Log.Information("App closed");
            Application.Current.Shutdown();
        }
        void MinimizeButton()
        {
            mainWindow.WindowState = WindowState.Minimized;
        }

        void ControlButtonClick(string buttonPos)
        {
            if (manager.currentPanelSelected != int.Parse(buttonPos))
            {
                PanelManager.HidePanel(manager.panels[manager.currentPanelSelected]);
                manager.currentPanelSelected = int.Parse(buttonPos); // new panel
                PanelManager.ShowPanel(manager.panels[manager.currentPanelSelected]);
            }
        }
    }
}

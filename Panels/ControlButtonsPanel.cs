using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace WaterBalance
{
    public class ControlButtonsPanel
    {
        private Button[] controlButtons = new Button[4];

        readonly MainWindow mainWindow;

        public ICommand Close { get; }
        public ICommand Minimize { get; }
        public ICommand ControlButtonSelect { get; }

        public ControlButtonsPanel(MainWindow mainWindow)
        {
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
            Application.Current.Shutdown();
        }
        void MinimizeButton()
        {
            mainWindow.WindowState = WindowState.Minimized;
        }

        void ControlButtonClick(string buttonPos)
        {
                if (mainWindow.currentPanelSelected != int.Parse(buttonPos))
                {
                    mainWindow.hidePanel(mainWindow.panels[mainWindow.currentPanelSelected]);
                    mainWindow.currentPanelSelected = int.Parse(buttonPos); // new panel
                    mainWindow.showPanel(mainWindow.panels[mainWindow.currentPanelSelected]);
                }
        }
    }
}

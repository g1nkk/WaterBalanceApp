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
    public class ControlButtonsPanel : MainMenuPanel
    {
        private Button[] controlButtons = new Button[4];

        public ICommand Close;
        public ICommand Minimize;
        public ICommand ControlButtonSelect;

        public ControlButtonsPanel()
        {
            controlButtons[0] = waterControlButton;
            controlButtons[1] = calendarControlButton;
            controlButtons[2] = achievementControlButton;
            controlButtons[3] = settingsControlButtons;

            Close = new RelayCommand(CloseButton);
            Minimize = new RelayCommand(MinimizeButton);
            ControlButtonSelect = new RelayCommand<int>(ControlButtonClick);
        }

        void CloseButton()
        {
            Application.Current.Shutdown();
        }
        void MinimizeButton()
        {
            WindowState = WindowState.Minimized;
        }

        void ControlButtonClick(int buttonNum)
        {
            if (currentPanelSelected != Convert.ToInt32(buttonNum))
            {
                hidePanel(panels[currentPanelSelected]);
                currentPanelSelected = Convert.ToInt32(buttonNum); // new panel
                showPanel(panels[currentPanelSelected]);
            }
        }
    }
}

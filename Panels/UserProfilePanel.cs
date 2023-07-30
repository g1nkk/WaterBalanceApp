using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterBalance
{
    public class UserProfilePanel : IDataSubscriber
    {
        PanelManager manager;
        MainWindow mainWindow;

        public UserProfilePanel(MainWindow mainWindow, PanelManager manager)
        {
            this.mainWindow = mainWindow;
            this.manager = manager;
        }

        public void Update()
        {
            mainWindow.profileName.Content = manager.userProfile.Name;
            mainWindow.profileMaxStreak.Content = manager.userProfile.MaxStreak;
            mainWindow.profileCurrentStreak.Content= manager.userProfile.CurrentStreak;
            mainWindow.profileTotalWaterConsumed.Content = manager.userProfile.TotalWaterConsumed;
        }
    }
}

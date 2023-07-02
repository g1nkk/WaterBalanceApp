using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterBalance;

namespace WaterBalance
{
    public class PanelManager
    {
        public Dictionary<string, object> ClassInstances { get; } = new Dictionary<string, object>();

        public PanelManager(MainWindow mainWindow)
        {
            AddClassInstance<AddWaterPanel>("AddWaterPanel", mainWindow);
            AddClassInstance<CalculatePanel>("CalсulatePanel", mainWindow);
            AddClassInstance<ControlButtonsPanel>("ControlButtonsPanel", mainWindow);
            AddClassInstance<SettingsPanel>("SettingsPanel", mainWindow);
            AddClassInstance<CalendarPanel>("CalendarPanel", mainWindow);
        }

        private void AddClassInstance<T>(string instanceName, MainWindow mainWindow) where T : class
        {
            T instance = (T)Activator.CreateInstance(typeof(T), mainWindow);
            ClassInstances[instanceName] = instance;
        }
    }
}

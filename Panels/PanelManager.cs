using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterBalance
{
    internal class PanelManager
    {
        public Dictionary<string, object> ClassInstances { get; } = new Dictionary<string, object>();

        public PanelManager()
        {
            AddClassInstance<AddWaterPanel>("AddWaterPanel");
            AddClassInstance<CalculatePanel>("CaclucalatePanel");
            AddClassInstance<ControlButtonsPanel>("ControlButtons");
            AddClassInstance<SettingsPanel>("SettingsPanel");
        }

        private void AddClassInstance<T>(string instanceName) where T : class, new()
        {
            ClassInstances[instanceName] = new T();
        }
    }
}

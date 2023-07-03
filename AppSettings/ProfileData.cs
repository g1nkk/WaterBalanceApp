using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WaterBalance
{
    public class ProfileData
    {
        public string Name { get; set; }
        public int TotalWaterConsumed { get; set; }
        public int DailyGoal { get; set; }
        public int CurrentStreak { get; set; }
        public int MaxStreak { get; set; }
        public bool NotificationsEnabled { get; set; } = true;

        [JsonConstructor]
        public ProfileData(string name, int totalWaterConsumed, int dailyGoal, int currentStreak, int maxStreak, bool notificationsEnabled)
        {
            Name = name;
            TotalWaterConsumed = totalWaterConsumed;
            DailyGoal = dailyGoal;
            CurrentStreak = currentStreak;
            MaxStreak = maxStreak;
            NotificationsEnabled = notificationsEnabled;
        }

        public ProfileData(int dailyGoal, string name) // new profile
        {
            Name = name;
            TotalWaterConsumed = 0;
            DailyGoal = dailyGoal;
            CurrentStreak = 0;
            MaxStreak = 0;
        }
    }
}

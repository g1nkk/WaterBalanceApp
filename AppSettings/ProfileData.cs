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

        [JsonConstructor]
        public ProfileData(string name, int totalWaterConsumed, int dailyGoal, int currentStreak, int maxStreak)
        {
            Name = name;
            TotalWaterConsumed = totalWaterConsumed;
            DailyGoal = dailyGoal;
            CurrentStreak = currentStreak;
            MaxStreak = maxStreak;
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

using System.Collections.Generic;

namespace WaterBalance
{
    public class ProfileData
    {
        public string Name { get; set; }
        public int TotalWaterConsumed { get; set; }
        public float DailyGoal { get; set; }
        public int CurrentStreak { get; set; }
        public int MaxStreak { get; set; }

        public ProfileData(string name, int totalWaterConsumed, float dailyGoal, int currentStreak, int maxStreak)
        {
            Name = name;
            TotalWaterConsumed = totalWaterConsumed;
            DailyGoal = dailyGoal;
            CurrentStreak = currentStreak;
            MaxStreak = maxStreak;
        }

        public ProfileData(float dailyGoal, string name) // new profile
        {
            Name = name;
            TotalWaterConsumed = 0;
            DailyGoal = dailyGoal;
            CurrentStreak = 0;
            MaxStreak = 0;
        }
    }
}

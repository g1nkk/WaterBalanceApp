using System.Windows.Controls;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WaterBalance
{
    public class AchievementsList
    {
        public List<Achievement> Achievements = new();

        public AchievementsList()
        {
            Achievements.Add(new FirstGoalAchievement());
            Achievements.Add(new OneWeekStreakAchievent());
            Achievements.Add(new OneMonthStreakAchievent());
            Achievements.Add(new HundredLitersAchievement());
            Achievements.Add(new ThousandLitersAchievement());
        }
    }

    [JsonObject]
    class FirstGoalAchievement : Achievement
    {
        [JsonProperty]
        public string Name { get; } = "First Goal";
        [JsonProperty]
        public string Description { get; } = "Complete goal for the first time";
        [JsonProperty]
        public bool IsCompleted { get; set; } = false;
    }

    class OneWeekStreakAchievent : Achievement
    {
        public string Name { get; } = "The beginning of the way";
        public string Description { get; } = "Keep your water balance for one week";
        public bool IsCompleted { get; set; } = false;
    }

    class OneMonthStreakAchievent : Achievement
    {
        public string Name { get; } = "Hydration Streak Master";
        public string Description { get; } = "Keep your water balance for one month";
        public bool IsCompleted { get; set; } = false;
    }

    class HundredLitersAchievement : Achievement
    { 
        public string Name { get; } = "You drank 100 liters of water!";
        public string Description { get; } = "100 liters of water is 400 large glasses!";
        public bool IsCompleted { get; set; } = false;
    }

    class ThousandLitersAchievement : Achievement
    {
        public string Name { get; } = "You drank 1000 literts of water!";
        public string Description { get; } = "1000 liters of water is 4000 large glasses!";
        public bool IsCompleted { get; set; } = false;
    }
}

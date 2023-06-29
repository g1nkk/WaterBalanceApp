using System.Windows.Controls;
using System.Collections.Generic;

namespace WaterBalance
{
    class AchievementsList
    {
        public List<Achievement> Achievements = new();

        public AchievementsList()
        {
            Achievements.Add(new FirstGoalAchievement());
            Achievements.Add(new OneWeekAchievement());
            Achievements.Add(new OneMonthAchievement());
            Achievements.Add(new OneWeekStreakAchievent());
            Achievements.Add(new OneMonthStreakAchievent());
            Achievements.Add(new HundredLitersAchievement());
            Achievements.Add(new ThousandLitersAchievement());
        }
    }

    class FirstGoalAchievement : Achievement
    {
        public Image Icon { get; }
        public string Name { get; } = "First Goal";
        public string Description { get; } = "Complete goal for the first time";
        public bool IsCompleted { get; set; } = false;
    }

    class OneWeekAchievement : Achievement
    {
        public Image Icon { get; }
        public string Name { get; } = "Hydration Consistency";
        public string Description { get; } = "C";
        public bool IsCompleted { get; set; } = false;
    }

    class OneMonthAchievement : Achievement
    {
        public Image Icon { get; }
        public string Name { get; } = "";
        public string Description { get; } = "";
        public bool IsCompleted { get; set; } = false;
    }

    class OneWeekStreakAchievent : Achievement
    {
        public Image Icon { get; }
        public string Name { get; } = "";
        public string Description { get; } = "";
        public bool IsCompleted { get; set; } = false;
    }

    class OneMonthStreakAchievent : Achievement
    {
        public Image Icon { get; }
        public string Name { get; } = "Hydration Streak Master";
        public string Description { get; } = "";
        public bool IsCompleted { get; set; } = false;
    }

    class HundredLitersAchievement : Achievement
    { 
        public Image Icon { get; }
        public string Name { get; } = "";
        public string Description { get; } = "";
        public bool IsCompleted { get; set; } = false;
    }

    class ThousandLitersAchievement : Achievement
    {
        public Image Icon { get; }
        public string Name { get; } = "";
        public string Description { get; } = "";
        public bool IsCompleted { get; set; } = false;
    }
}

﻿using System.Windows.Controls;

namespace WaterBalance.Achievements
{
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
        public string Name { get; } = "Hydration Consistency Badge";
        public string Description { get; } = "Complete goal for the first time";
        public bool IsCompleted { get; set; } = false;
    }

    class OneMonthAchievent : Achievement
    {

    }

    class OneWeekStreakAchievent : Achievement
    {
        public Image Icon { get; }
        public string Name { get; } = "Hydration Streak Master";
        public string Description { get; } = "Complete goal for the first time";
        public bool IsCompleted { get; set; } = false;
    }

    class OneMonthStreakAchievent : Achievement
    {
        public Image Icon { get; }
        public string Name { get; } = "Hydration Streak Master";
        public string Description { get; } = "";
        public bool IsCompleted { get; set; } = false;
    }
}
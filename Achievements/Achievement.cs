using Serilog;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows.Controls;

namespace WaterBalance
{
    public interface Achievement
    {
        string Name { get; }
        string Description { get; }
        bool IsCompleted { get; set; }
    }

    public class AchievementManager
    {
        public List<Achievement> AchievementList = new();

        [JsonConstructor]
        public AchievementManager(List<Achievement> Achievements) 
        {
            AchievementList = Achievements;
        }

        public AchievementManager()
        {
            AchievementList = new List<Achievement>
            {
                new FirstGoalAchievement(),
                new OneWeekStreakAchievent(),
                new OneMonthStreakAchievent(),
                new HundredLitersAchievement(),
                new ThousandLitersAchievement()
            };
            SaveAndLoadManager.SaveAchivements(this);
        }

        public void CompleteAchievement(Achievement achievement)
        {
            ToastNotificationsManager.ShowAchievementCompletedNotification(achievement);

            int index = AchievementList.FindIndex(a => a.Equals(achievement));
            if (index != -1)
            {
                AchievementList[index].IsCompleted = true;
            }

            Log.Information($"Achievement completed: {achievement.Name}");

            SaveAndLoadManager.SaveAchivements(this);
        }

        public bool IsAchievementCompleted(Achievement achievement)
        {
            int index = AchievementList.FindIndex(a => a.Equals(achievement));
            if (index != -1)
            {
                return AchievementList[index].IsCompleted;
            }
            else return false;
        }
    }
}

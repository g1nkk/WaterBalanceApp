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
        public readonly AchievementsList AchievementsList = new();
        public List<Achievement> CompletedAchievements;


        public AchievementManager(List<Achievement> completedAchievements) 
        {
            CompletedAchievements = completedAchievements;
        }

        [JsonConstructor]
        public AchievementManager()
        {
            CompletedAchievements = new List<Achievement>();
        }

        public void CompleteAchievement(Achievement achievement)
        {
            ToastNotificationsManager.ShowAchievementCompletedNotification(achievement);
            CompletedAchievements.Add(achievement);
            SaveAndLoadManager.SaveAchivements(this);
        }

        public bool IsAchievementCompleted(Achievement achievement)
        {
            return CompletedAchievements.Contains(achievement);
        }
    }
}

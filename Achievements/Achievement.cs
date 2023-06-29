using System.Collections.Generic;
using System.Windows.Controls;

namespace WaterBalance
{
    public interface Achievement
    {
        Image Icon { get; }
        string Name { get; }
        string Description { get; }
        bool IsCompleted { get; set; }
    }

    class AchievementManager
    {
        readonly AchievementsList AchievementsList = new();
        public List<int> CompletedAchievementIndices { get; set; }

        public AchievementManager(List<int> completedAchivements) 
        {
            CompletedAchievementIndices = completedAchivements;
        }

        public AchievementManager()
        {
            CompletedAchievementIndices = new List<int>();
        }
    }
}

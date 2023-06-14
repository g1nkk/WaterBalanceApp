using System.Windows.Controls;

namespace WaterBalance.Achievements
{
    public interface Achievement
    {
        Image Icon { get; }
        string Name { get; }
        string Description { get; }
        bool IsCompleted { get; set; }
    }
}

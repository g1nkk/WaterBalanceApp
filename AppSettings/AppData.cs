namespace WaterBalance
{
    internal class AppData
    {
        public int DailyGoal { get; set; }
        public int CurrentStreak {  get; set; }


        public AppData(int goal) 
        { 
            DailyGoal = goal;
        }


        public void SaveAllData()
        {

        }
        public void LoadAllData()
        {
            LoadSettings();
            LoadAchivements();
            LoadCalendar();
        }

        void LoadAchivements()
        {

        }
        void LoadCalendar()
        {

        }
        void LoadSettings()
        {

        }
    }
}

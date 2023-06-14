namespace WaterBalance
{
    internal class AppData
    {
        public float DailyGoal { get; set; }
        public int CurrentStreak {  get; set; }
        public int MaxStreak {  get; set; }

        public AppData() 
        {
            LoadAllData();
        }
        public AppData(int goal) // first startup setting
        { 
            DailyGoal = goal;
            CurrentStreak = 0;
            MaxStreak = 0;
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

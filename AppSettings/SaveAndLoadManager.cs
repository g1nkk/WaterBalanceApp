using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using Windows.System;
using System.Windows;

namespace WaterBalance
{
    static class SaveAndLoadManager
    {
        static readonly string saveDataFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "SaveData");
        public static bool SaveFlesExists()
        {
            string[] requiredFiles = { "ProfileData.json", "AchievementsData.json", "CalendarData.json" };

            if (!Directory.Exists(saveDataFolderPath))
            {
                Directory.CreateDirectory(saveDataFolderPath);
                return false;
            }

            foreach (string fileName in requiredFiles)
            {
                string filePath = Path.Combine(saveDataFolderPath, fileName);
                if (!File.Exists(filePath))
                    return false;
            }

            return true;
        }
        public static void SaveAllData(ProfileData appData, CalendarData calendarData, AchievementManager achievementManager)
        {
            SaveProfile(appData);
            SaveAchivements(achievementManager);
            SaveCalendar(calendarData);
        }
        public static void SaveAchivements(AchievementManager achievementManager)
        {
            string saveFilePath = Path.Combine(saveDataFolderPath, "AchievementsData.json");
            string json = JsonConvert.SerializeObject(achievementManager);
            File.WriteAllText(saveFilePath, json);
        }
        public static void SaveCalendar(CalendarData calendarData)
        {
            string saveFilePath = Path.Combine(saveDataFolderPath, "CalendarData.json");
            string json = JsonConvert.SerializeObject(calendarData);
            File.WriteAllText(saveFilePath, json);
        }
        public static void SaveProfile(ProfileData appData)
        {
            string saveFilePath = Path.Combine(saveDataFolderPath, "ProfileData.json");
            string json = JsonConvert.SerializeObject(appData);
            File.WriteAllText(saveFilePath, json);
        }

        public static void LoadAllData(out ProfileData appData, out CalendarData calendarData, out AchievementManager achievementManager)
        {
            appData = LoadProfile();
            achievementManager = LoadAchivements();
            calendarData = LoadCalendar();
        }

        static AchievementManager LoadAchivements()
        {
            string filePath = Path.Combine(saveDataFolderPath, "AchievementsData.json");

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<AchievementManager>(json);
        }
        static CalendarData LoadCalendar()
        {
            string filePath = Path.Combine(saveDataFolderPath, "CalendarData.json");

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<CalendarData>(json);
        }
        static ProfileData LoadProfile()
        {
            string filePath = Path.Combine(saveDataFolderPath, "ProfileData.json");

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ProfileData>(json);
        }
    }
}

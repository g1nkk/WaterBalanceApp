using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using Windows.System;
using System.Windows;
using Serilog;

namespace WaterBalance
{
    static class SaveAndLoadManager
    {
        static readonly string saveDataFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "SaveData");
        public static bool SaveFilesExists()
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
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string saveFilePath = Path.Combine(saveDataFolderPath, "AchievementsData.json");
            string json = JsonConvert.SerializeObject(achievementManager, settings);
            File.WriteAllText(saveFilePath, json);
            Log.Information($"Achievements data successfully saved");
        }
        public static void SaveCalendar(CalendarData calendarData)
        {
            string saveFilePath = Path.Combine(saveDataFolderPath, "CalendarData.json");
            string json = JsonConvert.SerializeObject(calendarData);
            File.WriteAllText(saveFilePath, json);
            Log.Information($"Calendar data successfully saved");
        }
        public static void SaveProfile(ProfileData appData)
        {
            string saveFilePath = Path.Combine(saveDataFolderPath, "ProfileData.json");
            string json = JsonConvert.SerializeObject(appData);
            File.WriteAllText(saveFilePath, json);
            Log.Information($"Profile data successfully saved");
        }

        public static void LoadAllData(out ProfileData appData, out CalendarData calendarData, out AchievementManager achievementManager)
        {
            appData = LoadProfile();
            achievementManager = LoadAchievements();
            calendarData = LoadCalendar();
        }

        static AchievementManager LoadAchievements()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string filePath = Path.Combine(saveDataFolderPath, "AchievementsData.json");

            string json = File.ReadAllText(filePath);
            Log.Information($"Achievements data successfully loaded");
            return JsonConvert.DeserializeObject<AchievementManager>(json, settings);
        }
        static CalendarData LoadCalendar()
        {
            string filePath = Path.Combine(saveDataFolderPath, "CalendarData.json");

            string json = File.ReadAllText(filePath);
            Log.Information($"Calendar data successfully loaded");
            return JsonConvert.DeserializeObject<CalendarData>(json);
        }
        static ProfileData LoadProfile()
        {
            string filePath = Path.Combine(saveDataFolderPath, "ProfileData.json");

            string json = File.ReadAllText(filePath);
            Log.Information($"Profile data successfully loaded");
            return JsonConvert.DeserializeObject<ProfileData>(json);
        }
    }
}

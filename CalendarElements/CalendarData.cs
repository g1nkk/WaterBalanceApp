using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WaterBalance
{
    public class CalendarData                                                                                                                                                                                                                                 
    {
        [JsonProperty]
        Month Month { get; set; }

        [JsonConstructor]
        public CalendarData(Month month)
        {
            Month = month;
            UpdateDate();
        }

        public CalendarData()
        {
            Month = new Month();
            UpdateDate();
        }
        public void UpdateDate()
        {
            UpdateMonthData();
            UpdateDayData();
            SaveAndLoadManager.SaveCalendar(this);
        }

        void UpdateMonthData()
        {
            if (IsNewMonth()) 
                Month = new Month();
        }

        public void SetTodayGoalCompleted()
        {
            Month.days[DateTime.Today.Day-1].GoalCompleted = true;
        }

        public bool IsNewMonth()
        {
            return Month.CurrentMonth.Month != DateTime.Now.Month;
        }

        public bool IsNewDay()
        {
            return Month.days[DateTime.Today.Day - 1] == null;
        }

        public bool IsDayEmpty(int dayIndex)
        {
            return Month.days[dayIndex] == null;
        }

        public bool IsTodayGoalCompleted()
        {
            return Month.days[DateTime.Today.Day-1].GoalCompleted;
        }
        public bool IsDayGoalCompleted(int index)
        {
            return Month.days[index]?.GoalCompleted ?? false;
        }

        void UpdateDayData()
        {
            if (IsDayEmpty(DateTime.Now.Day - 1))
                Month.CreateDay(DateTime.Now.Day-1);
        }

        public int GetTodayWaterAmount()
        {
            return Month.days[DateTime.Now.Day - 1].WaterDrank;
        }

        public int GetDayWaterAmount(int index)
        {
            return Month.days[index].WaterDrank;
        }

        public void AddWater(int waterAmount)
        {
            UpdateDate();

            Month.days[DateTime.Now.Day-1].WaterDrank += waterAmount;
        }
    }
}

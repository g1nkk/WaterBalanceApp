using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterBalance
{
    public class Month
    {
        public DateTime CurrentMonth { get; }
        readonly Day[] days;

        public Month() 
        {
            var date = DateTime.Now;
            CurrentMonth = date;
            days = new Day[DateTime.DaysInMonth(date.Year,date.Month)];
        }

        public bool IsDayEmpty(int day)
        {
            return days[day] == null;
        }

        public void CreateDay(int day)
        {
            days[day] = new Day();
        }

        public void SetGoalCompleted(int day)
        {
            days[day].GoalCompleted = true;
        }
        public void AddWaterToDay(int day, int waterAmount)
        {
            days[day].WaterDrank += waterAmount;
        }
    }
}

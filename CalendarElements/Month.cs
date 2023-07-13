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
        public readonly Day[] days;

        public Month() 
        {
            var date = DateTime.Now;
            CurrentMonth = date;
            days = new Day[DateTime.DaysInMonth(date.Year,date.Month)];
        }

        public void CreateDay(int day)
        {
            days[day] = new Day();
        }
    }
}

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
        public Month Month { get; set; }
        private int CurrentDay;
        private readonly DispatcherTimer NextDayTimer;

        public CalendarData()
        {
            Month = new Month();
            SetTodayDate();

            NextDayTimer = new DispatcherTimer();
            NextDayTimer.Tick += Timer_Tick;
            NextDayTimer.Start();
        }

        public void SetTodayDate()
        {
            CurrentDay = DateTime.Now.Day;
            if (Month.IsDayEmpty(CurrentDay))
                Month.CreateDay(CurrentDay);
        }

        public int GetTodayWaterAmount()
        {
            return Month.GetDayWater(CurrentDay);
        }

        public void AddWater(int waterAmount)
        {
            if (Month.IsDayEmpty(CurrentDay)) 
                Month.CreateDay(CurrentDay);

            Month.AddWaterToDay(CurrentDay, waterAmount);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsNewDay())
            {
                CheckTodayDate();
            }
        }

        void CheckTodayDate()
        {
            CurrentDay = DateTime.Now.Day;

            if (DateTime.Now.Month != Month.CurrentMonth.Month)
            {
                Month = new Month();
                Month.CreateDay(CurrentDay);
            }
        }

        bool IsNewDay()
        {
            return CurrentDay != DateTime.Now.Day;
        }
    }
}

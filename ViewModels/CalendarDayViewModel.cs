using System;
using System.Collections.Generic;
using System.Text;

namespace DynaRealm.ViewModels
{
    public class CalendarDayViewModel
    {
        public DateTime Date {  get; set; }

        public int DayNumber => Date.Day;

        public bool IsCurrentMonth {  get; set; }

        public bool IsToday => Date.Date == DateTime.Today;
    }
}

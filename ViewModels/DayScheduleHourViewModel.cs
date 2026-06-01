using System.Collections.Generic;

namespace DynaRealm.ViewModels
{
    public class DayScheduleHourViewModel
    {
        public int Hour { get; set; }

        public string HourText => $"{Hour:00}:00";

        public List<CalendarPageItemViewModel> Pages { get; set; } = new();
    }
}

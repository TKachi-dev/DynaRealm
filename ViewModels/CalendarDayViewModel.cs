using System;
using System.Collections.Generic;
using System.Linq;

namespace DynaRealm.ViewModels
{
    public class CalendarDayViewModel
    {
        public DateTime Date { get; set; }

        public int DayNumber => Date.Day;

        public bool IsCurrentMonth { get; set; }

        public bool IsToday => Date.Date == DateTime.Today;

        public string DayForeground => IsCurrentMonth ? "Black" : "Gray";

        public string BackgroundColor => "Transparent";

        public string ForegroundColor => DayForeground;

        public string DayNumberBackground =>
            IsToday ? "#FF6666" : "Transparent";

        public string DayNumberForeground =>
            IsToday ? "White" : DayForeground;

        public List<CalendarPageItemViewModel> Pages { get; set; } = new();

        public List<CalendarPageItemViewModel> VisiblePages =>
            Pages.Take(3).ToList();

        public int HiddenPageCount =>
            Pages.Count > 3 ? Pages.Count - 3 : 0;

        public bool HasHiddenPages => HiddenPageCount > 0;
    }
}

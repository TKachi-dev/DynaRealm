using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DynaRealm.ViewModels
{
    public class DayPageListViewModel
    {
        public DateTime Date { get; }
        public string DateText { get; }

        public List<CalendarPageItemViewModel> Pages { get; }

        public bool HasPages => Pages.Any();

        public bool HasNoPages => !Pages.Any();

        public DayPageListViewModel(CalendarDayViewModel day)
        {
            Date = day.Date;
            DateText = day.Date.ToString("yyyy年M月d日");
            Pages = day.Pages;
        }
    }
}

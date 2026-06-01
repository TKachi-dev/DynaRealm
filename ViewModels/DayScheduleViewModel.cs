using DynaRealm.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynaRealm.ViewModels
{
    public class DayScheduleViewModel : ViewModelBase
    {
        public DateTime Date { get; }

        public string DateText =>
            Date.ToString("yyyy年M月d日");

        public List<CalendarPageItemViewModel> AllDayPages { get; }

        public bool HasAllDayPages => AllDayPages.Any();

        public List<DayScheduleHourViewModel> Hours { get; }

        public DayScheduleViewModel(
            DateTime date,
            List<Page> pages,
            IEnumerable<TabViewModel> tabs)
        {
            Date = date;

            var tabDictionary = tabs.ToDictionary(t => t.Id);

            AllDayPages = pages
                .Where(p => !p.StartTime.HasValue)
                .Select(p => CreatePageItem(p, tabDictionary))
                .ToList();

            Hours = Enumerable.Range(0, 24)
                .Select(hour => new DayScheduleHourViewModel
                {
                    Hour = hour,
                    Pages = pages
                        .Where(p => p.StartTime.HasValue &&
                                    p.StartTime.Value.Hours == hour)
                        .Select(p => CreatePageItem(p, tabDictionary))
                        .ToList()
                })
                .ToList();
        }

        private CalendarPageItemViewModel CreatePageItem(
            Page page,
            Dictionary<Guid, TabViewModel> tabDictionary)
        {
            var tabName = tabDictionary.TryGetValue(page.TabId, out var tab)
                ? tab.Name
                : string.Empty;

            return new CalendarPageItemViewModel
            {
                PageId = page.Id,
                Title = page.Title,
                TabId = page.TabId,
                TabName = tabName,
                StartTime = page.StartTime,
                EndTime = page.EndTime,
            };
        }
    }
}

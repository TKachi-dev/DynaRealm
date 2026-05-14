using DynaRealm.Services;
using System.Collections.Generic;
using System.Linq;

namespace DynaRealm.ViewModels
{
    public class SearchResultViewModel : ViewModelBase
    {
        public string Keyword { get; }

        public List<CalendarPageItemViewModel> Results { get; }

        public bool HasResults => Results.Any();

        public bool HasNoResults => !Results.Any();

        public SearchResultViewModel(string keyword)
        {
            Keyword = keyword;

            var pageService = new PageService();

            Results = pageService.SearchPages(keyword)
                .Select(p => new CalendarPageItemViewModel
                {
                    PageId = p.Id,
                    Title = p.Title
                })
                .ToList();
        }
    }
}

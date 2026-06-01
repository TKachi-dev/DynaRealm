using DynaRealm.Services;
using System;

namespace DynaRealm.ViewModels
{
    public class PageDetailViewModel : ViewModelBase
    {
        private readonly Guid _pageId;
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public string StartTimeText { get; set; } = string.Empty;

        public string EndTimeText { get; set; } = string.Empty;

        public string DateText =>
            StartDate.ToString("yyyy年M月d日");

        public PageDetailViewModel(Guid pageId)
        {
            _pageId = pageId;

            var pageService = new PageService();

            var page = pageService.GetPageById(pageId);

            if (page != null)
            {
                Title = page.Title;
                Body = page.Body;
                StartDate = page.StartDate;

                StartTimeText = page.StartTime?.ToString(@"hh\:mm") ?? string.Empty;
                EndTimeText = page.EndTime?.ToString(@"hh\:mm") ?? string.Empty;
            }
        }

        public void Save()
        {
            TimeSpan? startTime = null;
            TimeSpan? endTime = null;

            if (TimeSpan.TryParse(StartTimeText, out var parsedStartTime))
            {
                startTime = parsedStartTime;
            }

            if (TimeSpan.TryParse(EndTimeText, out var parsedEndTime))
            {
                endTime = parsedEndTime;
            }

            var pageService = new PageService();

            pageService.UpdatePage(
                _pageId,
                Title,
                Body,
                startTime,
                endTime);
        }

        public void Delete()
        {
            var pageService = new PageService();

            pageService.DeletePage(_pageId);
        }
    }
}

using DynaRealm.Data;
using DynaRealm.Models;
using System;

namespace DynaRealm.ViewModels
{
    public class PageEditorViewModel : ViewModelBase
    {
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.Today;

        public string StartTimeText { get; set; } = string.Empty;

        public string EndTimeText { get; set; } = string.Empty;

        public Guid TabId { get; set; }

        public PageEditorViewModel()
        {
        }

        public PageEditorViewModel(DateTime date, Guid tabId)
        {
            StartDate = date;
            TabId = tabId;
        }

        public void Save()
        {
            using var db = new DynaRealmDbContext();

            var now = DateTime.Now;

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

            var page = new Page
            {
                Id = Guid.NewGuid(),
                Title = Title,
                StartDate = StartDate,
                EndDate = StartDate,

                StartTime = startTime,
                EndTime = endTime,

                TabId = TabId,
                Body = Body,
                CreatedAt = now,
                UpdatedAt = now,
            };

            db.Pages.Add(page);
            db.SaveChanges();
        }
    }
}

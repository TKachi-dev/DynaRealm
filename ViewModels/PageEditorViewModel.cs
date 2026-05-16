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

            var page = new Page
            {
                Id = Guid.NewGuid(),
                Title = Title,
                StartDate = StartDate,
                EndDate = StartDate,
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

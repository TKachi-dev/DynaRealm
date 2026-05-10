using DynaRealm.Data;
using DynaRealm.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DynaRealm.ViewModels
{
    public class PageEditorViewModel : ViewModelBase
    {
        public string Title { get; set; } = string.Empty;

        public string Body {  get; set; } = string.Empty;

        public void Save()
        {
            using var db = new DynaRealmDbContext();

            var tab = db.Tabs.First();

            var now = DateTime.Now;

            var page = new Page
            {
                Id = Guid.NewGuid(),
                Title = Title,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                TabId = tab.Id,
                Body = Body,
                CreatedAt = now,
                UpdatedAt = now,
            };

            db.Pages.Add(page);
            db.SaveChanges();
        }
    }
}

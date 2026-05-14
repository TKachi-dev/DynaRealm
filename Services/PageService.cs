using DynaRealm.Data;
using DynaRealm.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynaRealm.Services
{
    public class PageService
    {
        public List<Page> GetPagesByMonth(int year, int month, Guid tabId)
        {
            using var db = new DynaRealmDbContext();

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            return db.Pages
                .Where(p => p.StartDate >= startDate &&
                p.StartDate < endDate &&
                p.TabId == tabId)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }

        public Page? GetPageById(Guid pageId)
        {
            using var db = new DynaRealmDbContext();

            return db.Pages
                .FirstOrDefault(p => p.Id == pageId);
        }

        public void UpdatePage(Guid pageId, string title, string body)
        {
            using var db = new DynaRealmDbContext();

            var page = db.Pages.FirstOrDefault(p => p.Id == pageId);

            if (page == null)
            {
                return;
            }

            page.Title = title;
            page.Body = body;
            page.UpdatedAt = DateTime.Now;

            db.SaveChanges();
        }

        public void DeletePage(Guid pageId)
        {
            using var db = new DynaRealmDbContext();

            var page = db.Pages.FirstOrDefault(p => p.Id == pageId);

            if (page == null)
            {
                return;
            }

            db.Pages.Remove(page);
            db.SaveChanges();
        }
    }
}

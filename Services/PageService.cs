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

        public List<Page> GetPagesByDate(DateTime date)
        {
            using var db = new DynaRealmDbContext();

            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            return db.Pages
                .Where(p => p.StartDate >= startDate &&
                            p.StartDate < endDate)
                .ToList()
                .OrderBy(p => p.StartTime ?? TimeSpan.MaxValue)
                .ThenBy(p => p.CreatedAt)
                .ToList();
        }

        public Page? GetPageById(Guid pageId)
        {
            using var db = new DynaRealmDbContext();

            return db.Pages
                .FirstOrDefault(p => p.Id == pageId);
        }

        public void UpdatePage(
            Guid pageId,
            string title,
            string body,
            TimeSpan? startTime,
            TimeSpan? endTime)
        {
            using var db = new DynaRealmDbContext();

            var page = db.Pages.FirstOrDefault(p => p.Id == pageId);

            if (page == null)
            {
                return;
            }

            page.Title = title;
            page.Body = body;
            page.StartTime = startTime;
            page.EndTime = endTime;
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

        public List<Page> SearchPages(string keyword)
        {
            using var db = new DynaRealmDbContext();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new List<Page>();
            }

            return db.Pages
                .Where(p =>
                    p.Title.Contains(keyword) ||
                    p.Body.Contains(keyword))
                .OrderByDescending(p => p.UpdatedAt)
                .ToList();
        }
    }
}

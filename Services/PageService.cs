using DynaRealm.Data;
using DynaRealm.Models;
using Microsoft.EntityFrameworkCore;
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

        public bool IsLearningTab(Guid tabId)
        {
            using var db = new DynaRealmDbContext();

            var tab = db.Tabs.FirstOrDefault(t => t.Id == tabId);

            return tab?.Name == "学習";
        }

        public LearningLog? GetLearningLogByPageId(Guid pageId)
        {
            using var db = new DynaRealmDbContext();

            return db.LearningLogs
                .FirstOrDefault(ll => ll.PageId == pageId);
        }

        public List<LearningLog> GetTodayReviewLearningLogs(DateTime targetDate)
        {
            using var db = new DynaRealmDbContext();

            var today = targetDate.Date;

            return db.LearningLogs
                .Include(ll => ll.Page)
                .Where(ll =>
                    ll.ReviewRequired &&
                    ll.ReviewDate.HasValue &&
                    ll.ReviewDate.Value.Date <= today &&
                    !ll.ReviewDone)
                .OrderBy(ll => ll.ReviewDate)
                .ThenByDescending(ll => ll.UpdatedAt)
                .ToList();
        }

        public List<LearningLog> GetReviewLearningLogsByDateRange(
            DateTime startDate,
            DateTime endDateExclusive)
        {
            using var db = new DynaRealmDbContext();

            var rangeStart = startDate.Date;
            var rangeEnd = endDateExclusive.Date;

            return db.LearningLogs
                .Where(ll =>
                    ll.ReviewRequired &&
                    ll.ReviewDate.HasValue &&
                    ll.ReviewDate.Value >= rangeStart &&
                    ll.ReviewDate.Value < rangeEnd)
                .OrderBy(ll => ll.ReviewDate)
                .ThenByDescending(ll => ll.UpdatedAt)
                .ToList();
        }

        public void UpsertLearningLog(
            Guid pageId,
            string didToday,
            string learned,
            string stuck,
            string solved,
            string nextAction,
            bool reviewRequired,
            DateTime? reviewDate,
            bool reviewDone,
            string techTag)
        {
            using var db = new DynaRealmDbContext();

            var learningLog = db.LearningLogs
                .FirstOrDefault(ll => ll.PageId == pageId);

            if (learningLog == null)
            {
                learningLog = new LearningLog
                {
                    PageId = pageId,
                    CreatedAt = DateTime.Now
                };

                db.LearningLogs.Add(learningLog);
            }

            learningLog.DidToday = didToday;
            learningLog.Learned = learned;
            learningLog.Stuck = stuck;
            learningLog.Solved = solved;
            learningLog.NextAction = nextAction;
            learningLog.ReviewRequired = reviewRequired;
            learningLog.ReviewDate = reviewRequired ? reviewDate : null;
            learningLog.ReviewDone = reviewRequired && reviewDone;
            learningLog.TechTag = techTag;
            learningLog.UpdatedAt = DateTime.Now;

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

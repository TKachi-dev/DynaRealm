using DynaRealm.Services;
using System;
using System.Collections.Generic;

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

        public bool IsLearningPage { get; set; }

        public List<string> TechTags { get; } = new()
        {
            "C#",
            "Java",
            "Python",
            "JavaScript",
            "PowerShell",
            "Git",
            "Avalonia",
            "その他"
        };

        public string TechTag { get; set; } = "その他";

        public string DidToday { get; set; } = string.Empty;

        public string Learned { get; set; } = string.Empty;

        public string Stuck { get; set; } = string.Empty;

        public string Solved { get; set; } = string.Empty;

        public string NextAction { get; set; } = string.Empty;

        public bool ReviewRequired { get; set; }

        public string ReviewDateText { get; set; } = string.Empty;

        public bool ReviewDone { get; set; }

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

                IsLearningPage = pageService.IsLearningTab(page.TabId);
            }

            if (IsLearningPage)
            {
                var learningLog = pageService.GetLearningLogByPageId(pageId);

                if (learningLog != null)
                {
                    DidToday = learningLog.DidToday;
                    Learned = learningLog.Learned;
                    Stuck = learningLog.Stuck;
                    Solved = learningLog.Solved;
                    NextAction = learningLog.NextAction;
                    ReviewRequired = learningLog.ReviewRequired;
                    ReviewDateText = learningLog.ReviewDate?.ToString("yyyy/MM/dd") ?? string.Empty;
                    ReviewDone = learningLog.ReviewDone;
                    TechTag = string.IsNullOrWhiteSpace(learningLog.TechTag)
                        ? "その他"
                        : learningLog.TechTag;
                }
            }
        }

        public void Save()
        {
            TimeSpan? startTime = null;
            TimeSpan? endTime = null;
            DateTime? reviewDate = null;

            if (TimeSpan.TryParse(StartTimeText, out var parsedStartTime))
            {
                startTime = parsedStartTime;
            }

            if (TimeSpan.TryParse(EndTimeText, out var parsedEndTime))
            {
                endTime = parsedEndTime;
            }

            if (DateTime.TryParse(ReviewDateText, out var parsedReviewDate))
            {
                reviewDate = parsedReviewDate.Date;
            }

            var pageService = new PageService();

            pageService.UpdatePage(
                _pageId,
                Title,
                Body,
                startTime,
                endTime);

            if (IsLearningPage)
            {
                pageService.UpsertLearningLog(
                    _pageId,
                    DidToday,
                    Learned,
                    Stuck,
                    Solved,
                    NextAction,
                    ReviewRequired,
                    reviewDate,
                    ReviewDone,
                    TechTag);
            }
        }

        public void Delete()
        {
            var pageService = new PageService();

            pageService.DeletePage(_pageId);
        }
    }
}
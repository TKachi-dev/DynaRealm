using DynaRealm.Data;
using DynaRealm.Models;
using DynaRealm.Services;
using System;
using System.Collections.Generic;

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

        public string ErrorMessage { get; set; } = string.Empty;

        public bool HasErrorMessage =>
            !string.IsNullOrWhiteSpace(ErrorMessage);

        public PageEditorViewModel()
        {
        }

        public PageEditorViewModel(DateTime date, Guid tabId)
        {
            StartDate = date;
            TabId = tabId;

            var pageService = new PageService();
            IsLearningPage = pageService.IsLearningTab(TabId);
        }

        public bool Save()
        {
            ClearErrorMessage();

            using var db = new DynaRealmDbContext();

            var now = DateTime.Now;

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

            if (IsLearningPage && ReviewRequired)
            {
                if (string.IsNullOrWhiteSpace(ReviewDateText))
                {
                    SetErrorMessage("復習する場合は、復習日を入力してください。");
                    return false;
                }

                if (!DateTime.TryParse(ReviewDateText, out var parsedReviewDate))
                {
                    SetErrorMessage("復習日は日付として入力してください。例：2026/06/20");
                    return false;
                }

                reviewDate = parsedReviewDate.Date;
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

            if (IsLearningPage)
            {
                var pageService = new PageService();

                pageService.UpsertLearningLog(
                    page.Id,
                    DidToday,
                    Learned,
                    Stuck,
                    Solved,
                    NextAction,
                    ReviewRequired,
                    reviewDate,
                    false,
                    TechTag);
            }

            return true;
        }

        private void SetErrorMessage(string message)
        {
            ErrorMessage = message;

            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(HasErrorMessage));
        }

        private void ClearErrorMessage()
        {
            ErrorMessage = string.Empty;

            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(HasErrorMessage));
        }
    }
}
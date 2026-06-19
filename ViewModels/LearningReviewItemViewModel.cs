using System;

namespace DynaRealm.ViewModels
{
    public class LearningReviewItemViewModel : ViewModelBase
    {

        public Guid PageId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string TechTag { get; set; } = string.Empty;

        public DateTime? ReviewDate { get; set; }

        public string ReviewDateText =>
                ReviewDate?.ToString("yyyy/MM/dd") ?? string.Empty;

        public string NextAction { get; set; } = string.Empty;
    }
}

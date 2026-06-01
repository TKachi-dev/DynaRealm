using System;

namespace DynaRealm.Models
{
    public class Page
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public Guid TabId { get; set; }

        public string Body { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? LastOpenedAt { get; set; }
    }
}

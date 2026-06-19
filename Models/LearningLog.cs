using System;

namespace DynaRealm.Models
{
    public class LearningLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PageId { get; set; }

        public Page? Page { get; set; }

        public string DidToday { get; set; } = string.Empty;

        public string Learned { get; set; } = string.Empty;

        public string Stuck { get; set; } = string.Empty;

        public string Solved { get; set; } = string.Empty;

        public string NextAction { get; set; } = string.Empty;

        public bool ReviewRequired { get; set; }

        public DateTime? ReviewDate { get; set; }

        public bool ReviewDone { get; set; }

        public string TechTag { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DynaRealm.Models
{
    public class Page
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid TabId { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? LastOpenedAt { get; set; }
    }
}

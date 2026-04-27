using System;
using System.Collections.Generic;
using System.Text;

namespace DynaRealm.Models
{
    public class SubTag
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string NormalizedName { get; set; } = string.Empty;

        public DateTime CreatedAt {  get; set; }

        public DateTime LastUsedAt { get; set; }
    }
}

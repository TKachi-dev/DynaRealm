using System;
using System.Collections.Generic;
using System.Text;

namespace DynaRealm.Models
{
    public class Tab
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int SortOrder { get; set; }
    }
}

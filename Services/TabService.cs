using DynaRealm.Data;
using DynaRealm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynaRealm.Services
{
    public class TabService
    {
        public List<Tab> GetTabs()
        {
            using var db = new DynaRealmDbContext();

            return db.Tabs
                .OrderBy(t => t.SortOrder)
                .ToList();  
        }
    }
}

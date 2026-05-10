using DynaRealm.Data;
using DynaRealm.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynaRealm.Services
{
    public class PageService
    {
        public List<Page> GetPagesByMonth(int year, int month)
        {
            using var db = new DynaRealmDbContext();

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            return db.Pages
                .Where(p => p.StartDate >= startDate && p.StartDate < endDate)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }
    }
}

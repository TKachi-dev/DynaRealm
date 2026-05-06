using DynaRealm.Data;
using DynaRealm.Models;
using System;
using System.Linq;

namespace DynaRealm.Services
{
    public class AppStartupService
    {
        public static void Initialize()
        {
            using (var db = new DynaRealmDbContext())
            {
                // Tabsが存在するかチェック
                if (!db.Tabs.Any())
                {
                    db.Tabs.AddRange(
                        new Tab
                        {
                            Id = Guid.NewGuid(),
                            Name = "予定",
                            SortOrder = 1
                        },
                        new Tab
                        {
                            Id = Guid.NewGuid(),
                            Name = "学習",
                            SortOrder = 2
                        }
                    );

                    db.SaveChanges();
                }
            }
        }
    }
}
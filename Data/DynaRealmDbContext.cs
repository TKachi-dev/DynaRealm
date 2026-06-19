using DynaRealm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace DynaRealm.Data
{
    public class DynaRealmDbContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        public DbSet<LearningLog> LearningLogs { get; set; }

        public DbSet<Tab> Tabs { get; set; }

        public DbSet<SubTag> SubTags { get; set; }

        public DbSet<PageSubTag> PageSubTags { get; set; }

        public DbSet<AppSetting> AppSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(AppContext.BaseDirectory, "dynarealm.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageSubTag>()
                .HasKey(pst => new { pst.PageId, pst.SubTagId });

            modelBuilder.Entity<LearningLog>()
                .HasOne(ll => ll.Page)
                .WithOne()
                .HasForeignKey<LearningLog>(ll => ll.PageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubTag>()
                .HasIndex(st => st.NormalizedName)
                .IsUnique();

            modelBuilder.Entity<Tab>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<AppSetting>()
                .HasKey(a => a.Key);
        }
    }
}

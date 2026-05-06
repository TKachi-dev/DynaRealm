using DynaRealm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynaRealm.Data
{
    public class DynaRealmDbContext : DbContext
    {
        public DbSet<Page> Pages {  get; set; }

        public DbSet<Tab> Tabs { get; set; }

        public DbSet<SubTag> SubTags { get; set; }

        public DbSet<PageSubTag> PageSubTags { get; set; }

        public DbSet<AppSetting> AppSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=dynarealm.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageSubTag>()
                .HasKey(pst => new { pst.PageId, pst.SubTagId });

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

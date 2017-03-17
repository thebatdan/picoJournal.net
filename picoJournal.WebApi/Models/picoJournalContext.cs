using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace picoJournal.WebApi.Models
{
    public partial class PicoJournalContext : DbContext
    {
        public virtual DbSet<JournalEntry> JournalEntry { get; set; }
        public virtual DbSet<Question> Question { get; set; }

        public PicoJournalContext(DbContextOptions<PicoJournalContext> options)  : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JournalEntry>(entity =>
            {
                entity.Property(e => e.Answer).HasColumnType("varchar(140)");

                entity.Property(e => e.EntryDate).HasColumnType("date");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionText)
                    .HasColumnName("Question")
                    .HasColumnType("varchar(100)");
            });
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using picoJournal.WebApi.Models;

namespace picoJournal.WebApi.Migrations
{
    [DbContext(typeof(PicoJournalContext))]
    [Migration("20170317111304_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("picoJournal.WebApi.Models.JournalEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer")
                        .HasColumnType("varchar(140)");

                    b.Property<DateTime?>("EntryDate")
                        .HasColumnType("date");

                    b.Property<int?>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("JournalEntry");
                });

            modelBuilder.Entity("picoJournal.WebApi.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("QuestionText")
                        .HasColumnName("Question")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("picoJournal.WebApi.Models.JournalEntry", b =>
                {
                    b.HasOne("picoJournal.WebApi.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");
                });
        }
    }
}

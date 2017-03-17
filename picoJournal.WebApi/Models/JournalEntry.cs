using System;
using System.Collections.Generic;

namespace picoJournal.WebApi.Models
{
    public partial class JournalEntry
    {
        public int Id { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? QuestionId { get; set; }
        public string Answer { get; set; }

        public virtual Question Question { get; set; }
    }
}

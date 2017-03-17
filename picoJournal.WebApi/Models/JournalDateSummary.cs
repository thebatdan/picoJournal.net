using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace picoJournal.WebApi.Models
{
    public class JournalDateSummary
    {
        public DateTime? JournalDate { get; set; }
        public int EntryCount { get; set; }
    }
}

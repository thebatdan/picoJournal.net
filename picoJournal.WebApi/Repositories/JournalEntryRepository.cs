using picoJournal.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace picoJournal.WebApi.Repositories
{
    public class JournalEntryRepository
    {
        private readonly PicoJournalContext _context;

        public JournalEntryRepository(PicoJournalContext context)
        {
            _context = context;
        }

        public JournalEntry Get(int id)
        {
            return _context.Set<JournalEntry>()
                .Include(j => j.Question)
                .Where(j => j.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<JournalEntry> GetAll()
        {
            return _context.Set<JournalEntry>()
                .Include(j => j.Question)
                .ToList();
        }

        public IEnumerable<JournalEntry> GetForDay(DateTime entryDate)
        {
            return _context.Set<JournalEntry>()
                .Include(j => j.Question)
                .Where(j => j.EntryDate == entryDate.Date)
                .ToList();
        }

        public JournalEntry Create(JournalEntry journalEntry)
        {
            _context.Set<JournalEntry>().Add(journalEntry);
            _context.SaveChanges();
            return journalEntry;
        }

        public void Update(JournalEntry journalEntry)
        {
            _context.Set<JournalEntry>().Update(journalEntry);
            _context.SaveChanges();
        }

        public void Delete(JournalEntry journalEntry)
        {
            _context.Set<JournalEntry>().Remove(journalEntry);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Delete(Get(id));
        }

        public IEnumerable<JournalDateSummary> GetJournalDateSummary(DateTime dateFrom, DateTime dateTo)
        {
            return _context.Set<JournalEntry>()
                .Where(j => j.EntryDate >= dateFrom.Date && j.EntryDate <= dateTo.Date)
                .GroupBy(j => j.EntryDate)
                .Select(s => new JournalDateSummary { JournalDate = s.Key, EntryCount = s.Count() } )
                .OrderBy(s => s.JournalDate)
                .ToList();
        }
    }
}

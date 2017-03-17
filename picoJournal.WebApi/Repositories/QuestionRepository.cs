using picoJournal.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace picoJournal.WebApi.Repositories
{
    public class QuestionRepository
    {
        private readonly PicoJournalContext _context;

        public QuestionRepository(PicoJournalContext context)
        {
            _context = context;
        }

 
        public Question Get(int id)
        {
            return _context.Set<Question>().Find(id);
        }

        public IEnumerable<Question> GetAll()
        {
            return _context.Set<Question>().ToList();
        }

        public Question Create(Question question)
        {
            _context.Set<Question>().Add(question);
            _context.SaveChanges();
            return question;
        }

        public void Update(Question question)
        {
            _context.Set<Question>().Update(question);
            _context.SaveChanges();
        }

        public void Delete(Question question)
        {
            _context.Set<Question>().Remove(question);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Delete(Get(id));
        }

        public IEnumerable<Question> GetRandom(int questionCount, DateTime entryDate)
        {
            return _context.Set<Question>()
                .Where(q => !_context.Set<JournalEntry>()
                    .Any(j => j.QuestionId == q.Id && j.EntryDate == entryDate.Date))
                .OrderBy(q => Guid.NewGuid())
                .Take(questionCount);
        }
    }
}

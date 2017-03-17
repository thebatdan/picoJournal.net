using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using picoJournal.WebApi.Repositories;
using picoJournal.WebApi.Models;
using Microsoft.AspNetCore.Http;

namespace picoJournal.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class JournalEntryController : Controller
    {
        private readonly JournalEntryRepository _repository;

        internal string ControllerName
        {
            get
            {
                return ControllerContext.RouteData.Values["controller"].ToString();
            }
        }

        public JournalEntryController(JournalEntryRepository repository)
        {
            _repository = repository;
        }

        // GET api/journalentry
        [HttpGet]
        public IEnumerable<JournalEntry> Get()
        {
            return _repository.GetAll();
        }

        // GET api/journalentry/5
        [HttpGet("{id}", Name = "Get[controller]")]
        public IActionResult Get(int id)
        {
            return new ObjectResult(_repository.Get(id));
        }

        // POST api/journalentry
        [HttpPost]
        public IActionResult Post([FromBody]JournalEntry model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null)
            {
                return BadRequest();
            }
            try
            {
                model = _repository.Create(model);
                return CreatedAtRoute($"Get{ControllerName}", new { id = model.Id }, model);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/journalentry/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]JournalEntry model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null || !id.Equals(model.Id))
            {
                return BadRequest();
            }

            _repository.Update(model);
            return NoContent();
        }

        // DELETE api/journalentry/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        // GET api/[controller]/fordate/2017-03-17
        [HttpGet("fordate/{forDate}")]
        public virtual IActionResult GetForDay(DateTime forDate)
        {
            if (forDate == default(DateTime))
            {
                return BadRequest();
            }
            return new ObjectResult(_repository.GetForDay(forDate));
        }

        // GET api/[controller]/journaldatesummary
        [HttpGet("journaldatesummary")]
        public virtual IActionResult GetJournalDateSummary([FromQuery]DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            if (dateFrom == default(DateTime) || dateTo == default(DateTime))
            {
                return BadRequest();
            }
            return new ObjectResult(_repository.GetJournalDateSummary(dateFrom, dateTo));
        }
    }
}

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
    public class QuestionController : Controller
    {
        private readonly QuestionRepository _repository;

        internal string ControllerName
        {
            get
            {
                return ControllerContext.RouteData.Values["controller"].ToString();
            }
        }

        public QuestionController(QuestionRepository repository)
        {
            _repository = repository;
        }

        // GET api/question
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return _repository.GetAll();
        }

        // GET api/question/5
        [HttpGet("{id}", Name = "Get[controller]")]
        public IActionResult Get(int id)
        {
            return new ObjectResult(_repository.Get(id));
        }

        // POST api/question
        [HttpPost]
        public IActionResult Post([FromBody]Question model)
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

        // PUT api/question/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Question model)
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

        // DELETE api/question/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        // GET api/[controller]/random/
        [HttpGet("random")]
        public virtual IActionResult GetRandom([FromQuery]int questionCount, [FromQuery] DateTime forDate)
        {
            if (questionCount == default(int) || forDate == default(DateTime))
            {
                return BadRequest();
            }
            return new ObjectResult(_repository.GetRandom(questionCount, forDate));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qa_engine_api.Repositories;
using qa_engine_api.Services;
using qa_engine_api.ViewModels;

namespace qa_engine_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QaEngineContext _context;

        public QuestionController(QaEngineContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<QuestionVM> GetQuestionList()
        {
            return new QuestionRepo(_context).GetAll().ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetQuestion(long id) 
        {
            return new ObjectResult(new QuestionRepo(_context).GetById(id));
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody]Question newQuestion)
        {
            if (newQuestion.Description == "")
            {
                return BadRequest();
            }

            User user = new UserRepo(_context).Get(newQuestion.UserName);
            if (user == null)
            {
                return NotFound();
            }

            if (!new QuestionRepo(_context).Add(newQuestion))
            {
                return NoContent();
            }

            return Ok();
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Update([FromBody]Question updatedQuestion)
        {
            if (updatedQuestion.Description == "")
            {
                return BadRequest();
            }

            Question question = new QuestionRepo(_context).Get(updatedQuestion.Id);
            if (question.UserName != updatedQuestion.UserName)
            {
                return Unauthorized();
            }

            if(!new QuestionRepo(_context).Update(updatedQuestion))
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
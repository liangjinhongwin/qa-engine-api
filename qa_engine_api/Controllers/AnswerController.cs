using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qa_engine_api.Repositories;
using qa_engine_api.Services;

namespace qa_engine_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly QaEngineContext _context;

        public AnswerController(QaEngineContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody]Answer newAnswer)
        {
            if (newAnswer.Description == "")
            {
                return BadRequest();
            }

            User user = new UserRepo(_context).Get(newAnswer.UserName);
            if (user == null)
            {
                return NotFound();
            }

            if (!new AnswerRepo(_context).Add(newAnswer))
            {
                return NoContent();
            }

            return Ok();
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Update([FromBody]Answer updatedAnswer)
        {
            if (updatedAnswer.Description == "")
            {
                return BadRequest();
            }

            Answer answer = new AnswerRepo(_context).Get(updatedAnswer.Id);
            if (answer.UserName != updatedAnswer.UserName)
            {
                return Unauthorized();
            }

            if (!new AnswerRepo(_context).Update(updatedAnswer))
            {
                return NoContent();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody]Answer deletedAnswer)
        {
            Answer answer = new AnswerRepo(_context).Get(deletedAnswer.Id);
            if(answer.UserName != deletedAnswer.UserName)
            {
                return Unauthorized();
            }

            if(!new AnswerRepo(_context).Delete(deletedAnswer.Id))
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
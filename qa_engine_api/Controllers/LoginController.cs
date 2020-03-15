using Microsoft.AspNetCore.Mvc;
using qa_engine_api.Repositories;
using qa_engine_api.Services;
using qa_engine_api.ViewModels;
using System;
using System.Collections.Generic;

namespace qa_engine_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly QaEngineContext _context;

        public LoginController(QaEngineContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Profile")]
        public IActionResult GetUserProfile([FromBody]User user)
        {
            return new ObjectResult(new UserRepo(_context).GetProfile(user.UserName));
        }

        [HttpPost]
        public IActionResult SignIn([FromBody]User loginUser)
        {
            if (loginUser.UserName == "" || loginUser.Password == "")
            {
                return BadRequest();
            }

            User user = new UserRepo(_context).Get(loginUser.UserName);
            if (user == null)
            {
                return NotFound();
            }

            if (user != null && user.Password != loginUser.Password)
            {
                return Unauthorized();
            }

            return Ok();
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromBody]User newUser)
        {
            if (newUser.UserName == "" || newUser.Password == "")
            {
                return BadRequest();
            }

            UserRepo userRepo = new UserRepo(_context);
            if (!userRepo.Add(newUser))
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
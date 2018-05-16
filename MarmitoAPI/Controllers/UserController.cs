using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MarmitoAPI.Models;
using System.Linq;
using System;

namespace MarmitoAPI.Controllers
{
    [Route("api/[controller]")]

    public class UserController : Controller
    {
        private readonly MarmitoContext m_context;

        public UserController(MarmitoContext context)
        {
            m_context = context;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            if (!HttpContext.Request.Headers.ContainsKey("tokenValue") || !Auth.getAuth().isLogged(HttpContext.Request.Headers["tokenValue"]))
            {
                return BadRequest();
            }
            return new ObjectResult(m_context.Users.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult getUser(long Id)
        {
            if (!HttpContext.Request.Headers.ContainsKey("tokenValue") ||
                !Auth.getAuth().isLogged(HttpContext.Request.Headers["tokenValue"]) ||
                Auth.getAuth().getLoggedUser(HttpContext.Request.Headers["tokenValue"]).Id != Id)
            {
                return BadRequest();
            }
            return new ObjectResult(m_context.Users.FirstOrDefault(u => u.Id == Id));
        }

        [HttpPut]
        public IActionResult updateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (!HttpContext.Request.Headers.ContainsKey("tokenValue") || !Auth.getAuth().isLogged(HttpContext.Request.Headers["tokenValue"]))
            {
                return BadRequest();
            }

            user.Id = Auth.getAuth().getLoggedUser(HttpContext.Request.Headers["tokenValue"]).Id;

            if (user.Id != Auth.getAuth().getLoggedUser(HttpContext.Request.Headers["tokenValue"]).Id)
            {
                return BadRequest();
            }

            m_context.Users.Update(user);
            m_context.SaveChanges();

            return Ok();
        }
    }
}
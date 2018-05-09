using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MarmitoAPI.Models;
using System.Linq;

namespace MarmitoAPI.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly MarmitoContext m_context;

        public RegisterController(MarmitoContext context)
        {
            m_context = context;
        }

        [HttpGet]
        public IEnumerable<Mito> getAll()
        {
            return m_context.Mitos.ToList();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(long id)
        {
            var user = m_context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpPost]
        public IActionResult register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (m_context.Users.FirstOrDefault(u => u.Email == user.Email) != null)
            {
                return BadRequest();
            }

            m_context.Add(user);
            m_context.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }
    }
}
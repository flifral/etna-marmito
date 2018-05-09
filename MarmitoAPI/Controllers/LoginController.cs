using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MarmitoAPI.Models;
using System.Linq;
using System;

namespace MarmitoAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly MarmitoContext m_context;

        public LoginController(MarmitoContext context)
        {
            m_context = context;
        }

        [HttpPost]
        public IActionResult login([FromBody] Login login)
        {
            if (login == null)
            {
                return BadRequest();
            }

            var users = m_context.Users.Where(u => u.Email == login.Email && u.Password == login.Password);

            if (users.Count() == 0)
            {
                return BadRequest();
            }

            var user = users.First();

            string tokens = Auth.getAuth().logUser(user);
            Token token = new Token();
            token.TokenValue = tokens;

            return new ObjectResult(token);
        }
    }
}
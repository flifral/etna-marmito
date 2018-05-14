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
    }
}
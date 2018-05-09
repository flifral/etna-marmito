using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MarmitoAPI.Models;
using System.Linq;
using System;

namespace MarmitoAPI.Controllers
{
    [Route("api/[controller]")]
    public class MitoController : Controller
    {
        private readonly MarmitoContext m_context;

        public MitoController(MarmitoContext context)
        {
            m_context = context;
        }

        [HttpPost]
        public IActionResult mito([FromHeader] Token token, [FromBody] Mito mito)
        {
            Console.WriteLine(token.TokenValue);
            if (mito == null)
            {
                return BadRequest();
            }

            m_context.Add(mito);
            m_context.SaveChanges();

            return Ok();
        }
    }
}
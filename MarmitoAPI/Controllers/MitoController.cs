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

        [HttpGet]
        public IActionResult GetAll()
        {
            if (!HttpContext.Request.Headers.ContainsKey("tokenValue") || !Auth.getAuth().isLogged(HttpContext.Request.Headers["tokenValue"]))
            {
                return BadRequest();
            }
            return new ObjectResult(m_context.Mitos.ToList());
        }

        [HttpGet("{id}", Name = "GetMito")]
        public IActionResult GetById(long id)
        {
            var mito = m_context.Mitos.FirstOrDefault(u => u.Id == id);
            if (mito == null)
            {
                return NotFound();
            }

            if (!HttpContext.Request.Headers.ContainsKey("tokenValue") || !Auth.getAuth().isLogged(HttpContext.Request.Headers["tokenValue"]))
            {
                return BadRequest();
            }

            return new ObjectResult(mito);
        }
        [HttpPost]
        public IActionResult mito([FromBody] Mito mito)
        {
            if (mito == null)
            {
                return BadRequest();
            }

            if (m_context.Users.FirstOrDefault(u => u.Id == mito.AuthorId) == null)
            {
                return BadRequest();
            }

            m_context.Add(mito);
            m_context.SaveChanges();

            return CreatedAtRoute("GetMito", new {id = mito.Id}, mito);
        }
    }
}
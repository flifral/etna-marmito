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
        public IEnumerable<Mito> GetAll()
        {
            return m_context.Mitos.ToList();
        }

        [HttpGet("{id}", Name = "GetMito")]
        public IActionResult GetById(long id)
        {
            var mito = m_context.Mitos.FirstOrDefault(u => u.Id == id);
            if (mito == null)
            {
                return NotFound();
            }
            return new ObjectResult(mito);
        }
        [HttpPost]
        public IActionResult mito([FromHeader] Token token, [FromBody] Mito mito)
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
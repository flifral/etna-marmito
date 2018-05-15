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

        [HttpDelete("{id}", Name = "DeleteMito")]
        public IActionResult RemoveMito(long Id)
        {
            if (!HttpContext.Request.Headers.ContainsKey("tokenValue") || !Auth.getAuth().isLogged(HttpContext.Request.Headers["tokenValue"]))
            {
                return BadRequest();
            }

            Mito mito = m_context.Mitos.FirstOrDefault(m => m.Id == Id);
            if (mito != null)
            {
                m_context.Mitos.Remove(mito);
                m_context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{id}", Name = "GetMito")]
        public IActionResult GetById(long id)
        {
            if (!HttpContext.Request.Headers.ContainsKey("tokenValue") || !Auth.getAuth().isLogged(HttpContext.Request.Headers["tokenValue"]))
            {
                return BadRequest();
            }

            var mito = m_context.Mitos.FirstOrDefault(u => u.Id == id);
            if (mito == null)
            {
                return NotFound();
            }

            return new ObjectResult(mito);
        }

        [HttpPost]
        public IActionResult mito([FromBody] Mito mito)
        {
            if (!HttpContext.Request.Headers.ContainsKey("tokenValue") || !Auth.getAuth().isLogged(HttpContext.Request.Headers["tokenValue"]))
            {
                return BadRequest();
            }

            if (mito == null)
            {
                return BadRequest();
            }

            if (mito.AuthorId != Auth.getAuth().getLoggedUser(HttpContext.Request.Headers["tokenValue"]).Id)
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

        [HttpPut]
        public IActionResult update([FromBody] Mito mito)
        {
            if (!HttpContext.Request.Headers.ContainsKey("tokenValue") || !Auth.getAuth().isLogged(HttpContext.Request.Headers["tokenValue"]))
            {
                return BadRequest();
            }

            if (mito == null)
            {
                return BadRequest();
            }

            mito.AuthorId = Auth.getAuth().getLoggedUser(HttpContext.Request.Headers["tokenValue"]).Id;

            m_context.Mitos.Update(mito);
            m_context.SaveChanges();
            return Ok();
        }
    }
}
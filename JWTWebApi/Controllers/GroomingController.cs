using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWTWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroomingController : ControllerBase
    {
        private readonly SalonApiContext _context;

        public GroomingController(SalonApiContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<Grooming> GetAll()
        {
            return _context.Groomings;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _context.Groomings.SingleOrDefaultAsync(m => m.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Grooming model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Groomings.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAll", new { id = model.Id }, model);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Grooming model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroomingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _context.Groomings.SingleOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Groomings.Remove(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        private bool GroomingExists(int id)
        {
            return _context.Groomings.Any(e => e.Id == id);
        }
    }
}
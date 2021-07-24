using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Data;
using WebTruyen.API.Entities;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicInGenresController : ControllerBase
    {
        private readonly ComicDbContext _context;

        public ComicInGenresController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: api/ComicInGenres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComicInGenre>>> GetComicInGenres()
        {
            return await _context.ComicInGenres.ToListAsync();
        }

        // GET: api/ComicInGenres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComicInGenre>> GetComicInGenre(int id)
        {
            var comicInGenre = await _context.ComicInGenres.FindAsync(id);

            if (comicInGenre == null)
            {
                return NotFound();
            }

            return comicInGenre;
        }

        // PUT: api/ComicInGenres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComicInGenre(int id, ComicInGenre comicInGenre)
        {
            if (id != comicInGenre.IdGenre)
            {
                return BadRequest();
            }

            _context.Entry(comicInGenre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComicInGenreExists(id))
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

        // POST: api/ComicInGenres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComicInGenre>> PostComicInGenre(ComicInGenre comicInGenre)
        {
            _context.ComicInGenres.Add(comicInGenre);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ComicInGenreExists(comicInGenre.IdGenre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetComicInGenre", new { id = comicInGenre.IdGenre }, comicInGenre);
        }

        // DELETE: api/ComicInGenres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComicInGenre(int id)
        {
            var comicInGenre = await _context.ComicInGenres.FindAsync(id);
            if (comicInGenre == null)
            {
                return NotFound();
            }

            _context.ComicInGenres.Remove(comicInGenre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComicInGenreExists(int id)
        {
            return _context.ComicInGenres.Any(e => e.IdGenre == id);
        }
    }
}

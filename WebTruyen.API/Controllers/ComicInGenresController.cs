using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.ComicInGenre;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicInGenresController : ControllerBase
    {
        private readonly IComicInGenreService _comicInGenre;

        public ComicInGenresController(IComicInGenreService context)
        {
            _comicInGenre = context;
        }

        // GET: api/ComicInGenres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComicInGenreVM>>> GetComicInGenres()
        {
            return Ok(await _comicInGenre.GetComicInGenres());
        }

        // GET: api/ComicInGenres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComicInGenreVM>> GetComicInGenre(int id)
        {
            var comicInGenre = await _comicInGenre.GetComicInGenre(id);

            if (comicInGenre == null)
            {
                return NotFound();
            }

            return comicInGenre;
        }

        // PUT: api/ComicInGenres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComicInGenre(int id, ComicInGenreVM comicInGenre)
        {
            if (id != comicInGenre.IdGenre)
            {
                return BadRequest();
            }

            var result = await _comicInGenre.PutComicInGenre(id, comicInGenre);

            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        // POST: api/ComicInGenres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComicInGenreVM>> PostComicInGenre(ComicInGenreVM comicInGenre)
        {
            var result = await _comicInGenre.PostComicInGenre(comicInGenre);
            
            if (!result)
            {
                return Conflict();
            }

            return CreatedAtAction("GetComicInGenre", new { id = comicInGenre.IdGenre }, comicInGenre);
        }

        // DELETE: api/ComicInGenres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComicInGenre(int id)
        {
            var comicInGenre = await _comicInGenre.DeleteComicInGenre(id);
            if (!comicInGenre)
            {
                return NotFound();
            }

            return NoContent();
        }

       
    }
}

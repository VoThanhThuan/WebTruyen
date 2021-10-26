using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.Genre;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genre;

        public GenresController(IGenreService context)
        {
            _genre = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreAM>>> GetGenres()
        {
            return Ok(await _genre.GetGenres());
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreAM>> GetGenre(int id)
        {
            var result = await _genre.GetGenre(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, GenreAM genre)
        {
            if (id != genre.Id)
            {
                return BadRequest();
            }

            var result = await _genre.PutGenre(id, genre);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(GenreAM genre)
        {
            await _genre.PostGenre(genre);

            return NoContent();
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var result = await _genre.DeleteGenre(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.Comic;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicsController : ControllerBase
    {
        private readonly IComicService _comic;

        public ComicsController(IComicService context)
        {
            _comic = context;
        }

        // GET: api/Comics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComicVM>>> GetComics()
        {
            return Ok(await _comic.GetComics());
        }

        // GET: api/Comics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComicVM>> GetComic(Guid id)
        {
            var comic = await _comic.GetComic(id);

            if (comic == null)
            {
                return NotFound();
            }

            return comic;
        }

        // PUT: api/Comics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComic(Guid id, ComicVM request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var result = await _comic.PutComic(id, request);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Comics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComicVM>> PostComic(ComicVM request)
        {
            await _comic.PostComic(request);

            return CreatedAtAction("GetComic", new { id = request.Id }, request);
        }

        // DELETE: api/Comics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComic(Guid id)
        {
            var comic = await _comic.DeleteComic(id);
            if (comic == false)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}

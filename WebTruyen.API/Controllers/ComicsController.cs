using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.Comic;
using WebTruyen.API.Repository.ComicInGenre;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ComicsController : ControllerBase
    {
        private readonly IComicService _comic;
        private readonly IComicInGenreService _comicInGenre;
        private readonly IWebHostEnvironment _env;
        public ComicsController(IComicService context, IWebHostEnvironment env, IComicInGenreService comicInGenre)
        {
            _comic = context;
            _comicInGenre = comicInGenre;
            _env = env;
        }

        // GET: api/Comics
        [HttpGet]
        [Route("image/{name}")]
        public IActionResult GetImage(string name)
        {
            var filePath = Path.Combine(
                _env.ContentRootPath, "MyStaticFiles", name);

            return PhysicalFile(filePath, "image/jpeg");
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

        // GET: api/Comics/detail?nameAlias=Vo-Thanh-Thuan
        [HttpGet("detail")]
        public async Task<ActionResult<ComicVM>> GetComic([FromQuery]string nameAlias)
        {
            var comic = await _comic.GetComic(nameAlias);

            if (comic == null)
            {
                return NotFound();
            }

            return comic;
        }

        // PUT: api/Comics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComic(Guid id, [FromForm] ComicRequest request, [FromForm]List<int> genres)
        {
            if (id != request.Id)
                return BadRequest();

            var result = await _comic.PutComic(id, request);

            if (!result)
                return NotFound();

            var cig = genres.Select(genre => new ComicInGenreVM() {IdComic = id, IdGenre = genre}).ToList();

            result = await _comicInGenre.PutComicInGenres(id, cig);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/Comics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComicVM>> PostComic([FromForm] ComicRequest request, [FromForm] List<int> genres)
        {
            var comic = await _comic.PostComic(request);

            var cig = genres.Select(genre => new ComicInGenreVM() { IdComic = comic.Id, IdGenre = genre }).ToList();

            var result = await _comicInGenre.PutComicInGenres(comic.Id, cig);

            if (!result)
                return NotFound();

            return Ok(comic);
        }

        // DELETE: api/Comics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComic(Guid id)
        {
            var result = await _comic.DeleteComic(id);
            return result == 200 ? StatusCode(result) : NoContent();
        }


    }
}

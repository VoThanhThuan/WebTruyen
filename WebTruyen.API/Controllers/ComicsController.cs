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
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.API.Repository.ComicDI;
using WebTruyen.API.Repository.ComicInGenreDI;
using System.Security.Claims;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,Editer")]
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
        [AllowAnonymous]
        public async Task<ActionResult<ListComicAM>> GetComics(int skip = 0, int take = 10)
        {
            var comic = await _comic.GetComics(skip, take);
            return Ok(comic);
        }

        // GET: api/Comics
        [HttpGet("GetComicsOfUser")]
        public async Task<ActionResult<ListComicAM>> GetComicsOfUser(int skip = 0, int take = 10)
        {
            var userID = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            var role = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value;
            var comics = new ListComicAM();
            if(role == "Admin")
                comics = await _comic.GetComics(skip, take);
            else
                comics = await _comic.GetComicsOfUser(Guid.Parse(userID), skip, take);
            return Ok(comics);
        }

        // GET: api/Comics/SearchComics?contentSearch=
        [HttpGet("SearchComics")]
        [AllowAnonymous]
        public async Task<ActionResult<ListComicAM>> SearchComics(string contentSearch)
        {
            return Ok(await _comic.SearchComics(contentSearch));
        }

        // GET: api/Comics/xxx-xxx-xxx
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ComicAM>> GetComic([FromRoute]Guid id)
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
        [AllowAnonymous]
        public async Task<ActionResult<ComicAM>> GetComic([FromQuery]string nameAlias)
        {
            var comic = await _comic.GetComic(nameAlias);

            if (comic == null)
            {
                return NotFound();
            }

            return comic;
        }

        // GET: api/Comics/GetComicsInGenre?idGenre=1&skip=0&take=20
        [HttpGet("GetComicsInGenre")]
        [AllowAnonymous]
        public async Task<ActionResult<ListComicAM>> GetComicsInGenre(int idGenre, int skip = 0, int take = 20)
        {
            var comic = await _comic.GetComicsInGenre(idGenre, skip, take);

            if (comic == null) {
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

            var userID = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var result = await _comic.PutComic(id, request, Guid.Parse(userID));

            if (!result)
                return NotFound();

            var cig = genres.Select(genre => new ComicInGenreAM() {IdComic = id, IdGenre = genre}).ToList();

            result = await _comicInGenre.PutComicInGenres(id, cig);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/Comics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComicAM>> PostComic([FromForm] ComicRequest request, [FromForm] List<int> genres)
        {
            var userID = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            var nameUser = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.GivenName)?.Value;

            var comic = await _comic.PostComic(request, Guid.Parse(userID), nameUser);

            var cig = genres.Select(genre => new ComicInGenreAM() { IdComic = comic.Id, IdGenre = genre }).ToList();

            var result = await _comicInGenre.PostComicInGenres(comic.Id, cig);

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

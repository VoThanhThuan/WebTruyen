using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebTruyen.API.Repository.Chapter;
using WebTruyen.API.Repository.Page;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService _chapter;
        private readonly IPageService _page;

        public ChaptersController(IChapterService chapter, IPageService page)
        {
            _chapter = chapter;
            _page = page;
        }

        // GET: api/Chapters
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ChapterVM>>> GetChapters()
        {
            return Ok(await _chapter.GetChapters());
        }

        // GET: api/Chapters/comic?idComic=xxx-xxx-xxx-xxx
        [HttpGet("comic")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ChapterVM>>> GetChaptersInComic([FromQuery]Guid idComic)
        {
            return Ok(await _chapter.GetChaptersInComic(idComic));
        }

        // GET: api/Chapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterVM>> GetChapter(Guid id)
        {
            var result = await _chapter.GetChapter(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/Chapters/
        [HttpGet("lastChapter")]
        [AllowAnonymous]
        public async Task<ActionResult<ChapterVM>> GetLastChapter([FromQuery]Guid idComic)
        {
            var result = await _chapter.GetLastChapter(idComic);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/Chapters?idComic=xxxxx&amount=3
        [HttpGet("lastChapters")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ChapterVM>>> GetLastChapters([FromQuery] Guid idComic, [FromQuery] int amount)
        {
            var result = await _chapter.GetNewChapters(idComic, amount);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Chapters/5
        //Tăng kích thước update
        [RequestSizeLimit(100L * 1024L * 1024L)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100L * 1024L * 1024L)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapter(Guid id, [FromForm]ChapterRequest chapter, [FromForm]List<IFormFile> pages)
        {
            if (id != chapter.Id)
            {
                return BadRequest();
            }

            var result = await _chapter.PutChapter(id, chapter);

            if (result != StatusCodes.Status200OK)
                return StatusCode(result);
            if (!pages.Any()) return NoContent();
            result = await _page.PutPages(id, pages, chapter.IsLock);

            return result != StatusCodes.Status200OK ? StatusCode(result) : NoContent();
        }

        // POST: api/Chapters
        //Tăng kích thước update
        [RequestSizeLimit(100L * 1024L * 1024L)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100L * 1024L * 1024L)]
        [HttpPost]
        public async Task<ActionResult<ChapterVM>> PostChapter([FromForm]ChapterRequest chapter, [FromForm]List<IFormFile> pages)
        {
            if (pages.Count < 1)
                return BadRequest("Không có hình ảnh");

            var result = await _chapter.PostChapter(chapter);
            if (result is null)
                return BadRequest("Tạo chapter mới thất bại");

            await _page.PostPages(result.Id, pages);

            return Ok(result);

            //return CreatedAtAction("GetChapter", new { id = result.Id }, chapter);
        }

        // POST: api/Chapters/ContinuePostChapter/idChapter
        [HttpPost("ContinuePostChapter/{idChapter}")]
        public async Task<ActionResult<ChapterVM>> PostChapterContinue([FromRoute] Guid idChapter, [FromForm] List<IFormFile> pages)
        {
            if (pages.Count < 1)
                return BadRequest("Không có hình ảnh");

            var result = await _page.PostPages(idChapter, pages);

            return Ok(result);

            //return CreatedAtAction("GetChapter", new { id = result.Id }, chapter);
        }

        // DELETE: api/Chapters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(Guid id)
        {
            var result = await _chapter.DeleteChapter(id);
            if (result != StatusCodes.Status200OK)
            {
                return StatusCode(result);
            }

            return NoContent();
        }

    }
}

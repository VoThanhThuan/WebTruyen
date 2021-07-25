using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.API.Repository.Chapter;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService _chapter;

        public ChaptersController(IChapterService context)
        {
            _chapter = context;
        }

        // GET: api/Chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterVM>>> GetChapters()
        {
            return Ok(await _chapter.GetChapters());
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

        // PUT: api/Chapters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapter(Guid id, ChapterVM chapter)
        {
            if (id != chapter.Id)
            {
                return BadRequest();
            }

            var result = await _chapter.PutChapter(id, chapter);

            if (result == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Chapters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChapterVM>> PostChapter(ChapterVM chapter)
        {
            await _chapter.PostChapter(chapter);

            return CreatedAtAction("GetChapter", new { id = chapter.Id }, chapter);
        }

        // DELETE: api/Chapters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(Guid id)
        {
            var result = await _chapter.DeleteChapter(id);
            if (result == false)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}

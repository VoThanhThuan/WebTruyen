using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.Bookmark;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private readonly IBookmarkService _bookmark;

        public BookmarksController(IBookmarkService context)
        {
            _bookmark = context;
        }

        // GET: api/Bookmarks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookmarkAM>>> GetBookmarks()
        {
            return Ok(await _bookmark.GetBookmarks());
        }

        // GET: api/Bookmarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookmarkAM>> GetBookmark(Guid id)
        {
            var result = await _bookmark.GetBookmark(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/Bookmarks/GetBookmarkOfAccount?idComic=[]&idUser=[]
        [HttpGet("GetBookmarkOfAccount")]
        public async Task<ActionResult<BookmarkAM>> GetBookmarkOfAccount(Guid idComic, Guid idUser)
        {
            var result = await _bookmark.GetBookmarkOfAccount(idComic, idUser);

            if (result == null) {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Bookmarks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookmark(Guid id, BookmarkAM bookmark)
        {
            if (id != bookmark.IdUser)
            {
                return BadRequest();
            }

            var result = await _bookmark.PutBookmark(id, bookmark);

            if (result == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Bookmarks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookmarkAM>> PostBookmark(BookmarkAM bookmark)
        {
            var result = await _bookmark.PostBookmark(bookmark);
            if (result == false)
            {
                return NotFound();
            }

            //return CreatedAtAction("GetBookmark", new { id = bookmark.IdUser }, bookmark);
            return Ok(bookmark);
        }

        // DELETE: api/Bookmarks?idUser=[]&idComic=[]
        [HttpDelete()]
        public async Task<IActionResult> DeleteBookmark(Guid idUser, Guid idComic)
        {
            var result = await _bookmark.DeleteBookmark(idUser, idComic);
            if (result == false)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}

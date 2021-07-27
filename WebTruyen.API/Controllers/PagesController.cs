using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.Page;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPageService _page;

        public PagesController(IPageService context)
        {
            _page = context;
        }

        // GET: api/Pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PageVM>>> GetPages()
        {
            return Ok(_page.GetPages());
        }

        // GET: api/Pages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PageVM>> GetPage(Guid id)
        {
            var page = await _page.GetPage(id);
            if (page == null)
            {
                return NotFound();
            }

            return page;
        }

        // PUT: api/Pages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPage(Guid id, PageVM page)
        {
            if (id != page.Id)
            {
                return BadRequest();
            }

            var result = await _page.PutPage(id, page);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/Pages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Page>> PostPage(PageVM page)
        {
            await _page.PostPage(page);

            return CreatedAtAction("GetPage", new { id = page.Id }, page);
        }

        // DELETE: api/Pages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(Guid id)
        {
            var page = await _page.DeletePage(id);
            if (!page)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}

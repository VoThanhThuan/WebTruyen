using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebTruyen.API.Repository.Page;
using WebTruyen.API.Service;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPageService _page;
        private readonly IWebHostEnvironment _env;
        private readonly IStorageService _storage;

        public PagesController(IPageService context, IWebHostEnvironment env, IStorageService storage)
        {
            _page = context;
            _env = env;
            _storage = storage;
        }


        // GET: api/Pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PageAM>>> GetPages()
        {
            return Ok(await _page.GetPages());
        }

        // GET: api/Pages/image
        [HttpGet]
        [Route("image")]
        public IActionResult GetImage([FromQuery] string name, [FromHeader] string authorization)
        {
            //var principal = User as ClaimsPrincipal;
            var check = User.Identity.IsAuthenticated;
            
            var filePath = Path.Combine(
                _env.ContentRootPath, "MyStaticFiles", name);

            var folder = Path.GetDirectoryName(name);
            if (_storage.FileExists($@"{folder}/chapter.isLock", security: true))
            {
                if(check)
                    return PhysicalFile(filePath, "image/jpeg");
                return PhysicalFile(Path.Combine(_env.ContentRootPath, "MyStaticFiles", "Psyduck-image-lock.png"), "image/jpeg");
            }
            else
            {
                return PhysicalFile(filePath, "image/jpeg");
            }
        }

        // GET: api/Pages/chapter?idChapter=69
        [HttpGet("chapter")]
        public async Task<ActionResult<IEnumerable<PageAM>>> GetPagesInChapter([FromQuery]Guid idChapter)
        {
            return Ok(await _page.GetPagesInChapter(idChapter));
        }

        // GET: api/Pages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PageAM>> GetPage(Guid id)
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
        public async Task<IActionResult> PutPage(Guid idChapter, [FromForm]PageRequest requests)
        {
            if (idChapter != requests.Id)
                return BadRequest();


            var result = await _page.PutPage(idChapter, requests);
            if (result != StatusCodes.Status200OK)
                return StatusCode(result);

            return NoContent();
        }

        // POST: api/Pages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PageRequest>> PostPage(Guid idChapter, [FromForm] PageRequest requests)
        {
            if (idChapter != requests.IdChapter)
                return BadRequest();

            var result = await _page.PostPage(idChapter, requests);

            if (!result.isSuccess)
                return BadRequest(result.messages);

            return CreatedAtAction("GetPage", new { id = result.page.Id }, requests);
        }

        // DELETE: api/Pages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(Guid id)
        {
            var result = await _page.DeletePage(id);
            if (result != StatusCodes.Status200OK)
            {
                return StatusCode(result);
            }
            return NoContent();
        }

    }
}

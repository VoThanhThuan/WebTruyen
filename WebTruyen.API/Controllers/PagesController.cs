﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.Page;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.Request;
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
            return Ok(await _page.GetPages());
        }

        // GET: api/Pages/chapter?idChapter=69
        [HttpGet("chapter")]
        public async Task<ActionResult<IEnumerable<PageVM>>> GetPagesWithChapter([FromQuery]Guid idChapter)
        {
            return Ok(await _page.GetPagesWithChapter(idChapter));
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
        public async Task<IActionResult> PutPage(Guid idChapter, [FromForm]PageRequest requests)
        {
            if (idChapter != requests.Id)
                return BadRequest();


            var result = await _page.PutPage(idChapter, requests);
            if (!result)
                return NotFound();

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

            return CreatedAtAction("GetPage", new { id = result.Id }, requests);
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

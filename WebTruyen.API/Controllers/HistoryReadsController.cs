﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.HistoryRead;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryReadsController : ControllerBase
    {
        private readonly IHistoryReadService _historyRead;

        public HistoryReadsController(IHistoryReadService context)
        {
            _historyRead = context;
        }

        // GET: api/HistoryReads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryReadVM>>> GetHistoryReads()
        {
            return Ok(await _historyRead.GetHistoryReads());
        }

        // GET: api/HistoryReads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoryReadVM>> GetHistoryRead(Guid id)
        {
            var historyRead = await _historyRead.GetHistoryRead(id);

            if (historyRead == null)
            {
                return NotFound();
            }

            return historyRead;
        }

        // PUT: api/HistoryReads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryRead(Guid id, HistoryReadVM historyRead)
        {
            if (id != historyRead.IdUser)
            {
                return BadRequest();
            }

            var result = await _historyRead.PutHistoryRead(id, historyRead);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/HistoryReads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoryReadVM>> PostHistoryRead(HistoryReadVM historyRead)
        {
            var result = await _historyRead.PostHistoryRead(historyRead);
            if (!result)
                return Conflict();

            return CreatedAtAction("GetHistoryRead", new { id = historyRead.IdUser }, historyRead);
        }

        // DELETE: api/HistoryReads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryRead(Guid id)
        {
            var result = await _historyRead.DeleteHistoryRead(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

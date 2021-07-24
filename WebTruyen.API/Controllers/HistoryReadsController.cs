using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Data;
using WebTruyen.API.Entities;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryReadsController : ControllerBase
    {
        private readonly ComicDbContext _context;

        public HistoryReadsController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: api/HistoryReads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryRead>>> GetHistoryReads()
        {
            return await _context.HistoryReads.ToListAsync();
        }

        // GET: api/HistoryReads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoryRead>> GetHistoryRead(Guid id)
        {
            var historyRead = await _context.HistoryReads.FindAsync(id);

            if (historyRead == null)
            {
                return NotFound();
            }

            return historyRead;
        }

        // PUT: api/HistoryReads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryRead(Guid id, HistoryRead historyRead)
        {
            if (id != historyRead.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(historyRead).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryReadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HistoryReads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoryRead>> PostHistoryRead(HistoryRead historyRead)
        {
            _context.HistoryReads.Add(historyRead);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HistoryReadExists(historyRead.IdUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHistoryRead", new { id = historyRead.IdUser }, historyRead);
        }

        // DELETE: api/HistoryReads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryRead(Guid id)
        {
            var historyRead = await _context.HistoryReads.FindAsync(id);
            if (historyRead == null)
            {
                return NotFound();
            }

            _context.HistoryReads.Remove(historyRead);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoryReadExists(Guid id)
        {
            return _context.HistoryReads.Any(e => e.IdUser == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Data;
using WebTruyen.API.Entities;
using WebTruyen.API.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewComicAnnouncementsController : ControllerBase
    {
        private readonly ComicDbContext _context;

        public NewComicAnnouncementsController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: api/NewComicAnnouncements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewComicAnnouncementVM>>> GetNewComicAnnouncements()
        {
            return await _context.NewComicAnnouncements.Select(x => x.ToViewModel()).ToListAsync();
        }

        // GET: api/NewComicAnnouncements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewComicAnnouncementVM>> GetNewComicAnnouncement(Guid id)
        {
            var newComicAnnouncement = await _context.NewComicAnnouncements.FindAsync(id);

            if (newComicAnnouncement == null)
            {
                return NotFound();
            }

            return newComicAnnouncement.ToViewModel();
        }

        // PUT: api/NewComicAnnouncements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewComicAnnouncement(Guid id, NewComicAnnouncementVM newComicAnnouncement)
        {
            if (id != newComicAnnouncement.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(newComicAnnouncement.ToNewComicAnnouncement()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewComicAnnouncementExists(id))
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

        // POST: api/NewComicAnnouncements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewComicAnnouncement>> PostNewComicAnnouncement(NewComicAnnouncementVM newComicAnnouncement)
        {
            _context.NewComicAnnouncements.Add(newComicAnnouncement.ToNewComicAnnouncement());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NewComicAnnouncementExists(newComicAnnouncement.IdUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNewComicAnnouncement", new { id = newComicAnnouncement.IdUser }, newComicAnnouncement);
        }

        // DELETE: api/NewComicAnnouncements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewComicAnnouncement(Guid id)
        {
            var newComicAnnouncement = await _context.NewComicAnnouncements.FindAsync(id);
            if (newComicAnnouncement == null)
            {
                return NotFound();
            }

            _context.NewComicAnnouncements.Remove(newComicAnnouncement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewComicAnnouncementExists(Guid id)
        {
            return _context.NewComicAnnouncements.Any(e => e.IdUser == id);
        }
    }
}

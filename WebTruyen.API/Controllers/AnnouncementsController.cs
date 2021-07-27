using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.Announcement;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcement;

        public AnnouncementsController(IAnnouncementService context)
        {
            _announcement = context;
        }

        // GET: api/NewComicAnnouncements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementVM>>> GetAnnouncements()
        {
            return Ok(await _announcement.GetAnnouncements());
        }

        // GET: api/NewComicAnnouncements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementVM>> GetAnnouncement(Guid id)
        {
            var newComicAnnouncement = await _announcement.GetAnnouncement(id);

            if (newComicAnnouncement == null)
            {
                return NotFound();
            }

            return newComicAnnouncement;
        }

        // PUT: api/NewComicAnnouncements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement(Guid id, AnnouncementVM announcement)
        {
            if (id != announcement.IdUser)
            {
                return BadRequest();
            }

            var result = await _announcement.PutAnnouncement(id, announcement);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/NewComicAnnouncements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Announcement>> PostAnnouncement(AnnouncementVM announcement)
        {
            var result = await _announcement.PostAnnouncement(announcement);
            if (!result)
                return Conflict();


            return CreatedAtAction("GetAnnouncement", new { id = announcement.IdUser }, announcement);
        }

        // DELETE: api/NewComicAnnouncements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(Guid id)
        {
            var result = await _announcement.DeleteAnnouncement(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}

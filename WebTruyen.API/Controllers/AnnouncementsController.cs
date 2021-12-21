using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.AnnouncementDI;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcement;

        public AnnouncementsController(IAnnouncementService context)
        {
            _announcement = context;
        }

        // GET: api/Announcements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementAM>>> GetAnnouncements()
        {
            return Ok(await _announcement.GetAnnouncements());
        }

        // GET: api/Announcements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementAM>> GetAnnouncement(Guid id)
        {
            var newComicAnnouncement = await _announcement.GetAnnouncement(id);

            if (newComicAnnouncement == null)
            {
                return NotFound();
            }

            return newComicAnnouncement;
        }

        // GET: api/Announcements/GetAnnouncementOfUser
        [HttpGet("GetAnnouncementsOfUser")]
        public async Task<ActionResult<List<AnnouncementAM>>> GetAnnouncementsOfUser()
        {
            var userID = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var newComicAnnouncement = await _announcement.GetAnnouncementsOfUser(Guid.Parse(userID));

            if (newComicAnnouncement == null) {
                return NoContent();
            }

            return newComicAnnouncement;
        }

        // GET: api/Announcements/GetChapterOfAnnouncements
        [HttpGet("GetChapterOfAnnouncements")]
        public async Task<ActionResult<ListChapterAM>> GetChapterOfAnnouncements()
        {
            var userID = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var listChapter = await _announcement.GetChapterOfAnnouncements(Guid.Parse(userID));

            if (listChapter == null) {
                return NoContent();
            }

            return listChapter;
        }

        // PUT: api/Announcementss/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement(Guid id, AnnouncementAM announcement)
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
        public async Task<ActionResult<Announcement>> PostAnnouncement(AnnouncementAM announcement)
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

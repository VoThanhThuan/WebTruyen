using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Announcement
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly ComicDbContext _context;

        public AnnouncementService(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnnouncementVM>> GetAnnouncements()
        {
            return await _context.NewComicAnnouncements.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<AnnouncementVM> GetAnnouncement(Guid id)
        {
            var newComicAnnouncement = await _context.NewComicAnnouncements.FindAsync(id);

            return newComicAnnouncement?.ToViewModel();
        }

        public async Task<bool> PutAnnouncement(Guid id, AnnouncementVM request)
        {
            _context.Entry(request.ToNewComicAnnouncement()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewComicAnnouncementExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> PostAnnouncement(AnnouncementVM request)
        {
            _context.NewComicAnnouncements.Add(request.ToNewComicAnnouncement());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NewComicAnnouncementExists(request.IdUser))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeleteAnnouncement(Guid id)
        {
            var newComicAnnouncement = await _context.NewComicAnnouncements.FindAsync(id);
            if (newComicAnnouncement == null)
            {
                return false;
            }

            _context.NewComicAnnouncements.Remove(newComicAnnouncement);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool NewComicAnnouncementExists(Guid id)
        {
            return _context.NewComicAnnouncements.Any(e => e.IdUser == id);
        }
    }
}

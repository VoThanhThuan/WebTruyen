using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.AnnouncementDI
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly ComicDbContext _context;

        public AnnouncementService(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnnouncementAM>> GetAnnouncements()
        {
            return await _context.NewComicAnnouncements.Select(x => x.ToApiModel()).ToListAsync();
        }

        public async Task<AnnouncementAM> GetAnnouncement(Guid id)
        {
            var newComicAnnouncement = await _context.NewComicAnnouncements.FindAsync(id);

            return newComicAnnouncement?.ToApiModel();
        }

        public async Task<List<AnnouncementAM>> GetAnnouncementsOfUser(Guid idUser)
        {
            var announcements = await _context.NewComicAnnouncements.Where(x => x.IdUser == idUser)
                .OrderByDescending(x => x.TimeCreate)
                .Select(x => x.ToApiModel())
                .ToListAsync();
            return announcements;
        }

        public async Task<ListChapterAM> GetChapterOfAnnouncements(Guid idUser, int skip = 0, int take = 10)
        {
            var chapters = await _context.NewComicAnnouncements.Where(x => x.IdUser == idUser)
                .OrderByDescending(x => x.TimeCreate)
                .Skip(skip).Take(take)
                .Include(x => x.Chapter)
                .ThenInclude(x=>x.Comic)
                .Select(x => x.Chapter.ToApiModel())
                .ToListAsync();
            var listChapter = new ListChapterAM() {
                Total = chapters.Count,
                skip = skip,
                Take = take,
                Chapters = chapters
            };
            return listChapter;
        }

        public async Task<bool> PutAnnouncement(Guid id, AnnouncementAM request)
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

        public async Task<bool> PostAnnouncement(AnnouncementAM request)
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

        public async Task<bool> DeleteAnnouncement(Guid idUser, Guid idChapter)
        {
            
            var newComicAnnouncement = await _context.NewComicAnnouncements.FirstOrDefaultAsync(x => x.IdUser == idUser && x.IdChapter == idChapter);
            if (newComicAnnouncement == null)
                return false;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.Library.Enums;

namespace WebTruyen.API.Repository.Chapter
{
    public class ChapterService : IChapterService
    {
        private readonly ComicDbContext _context;

        public ChapterService(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChapterVM>> GetChapters()
        {
            return await _context.Chapters.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<ChapterVM> GetChapter(Guid id)
        {
            var chapter = await _context.Chapters.FindAsync(id);

            return chapter?.ToViewModel();
        }

        public async Task<bool> PutChapter(Guid id, ChapterVM chapter)
        {

            _context.Entry(chapter.ToChapter()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterExists(id))
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

        public async Task<bool> PostChapter(ChapterVM chapter)
        {
            _context.Chapters.Add(chapter.ToChapter());
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteChapter(Guid id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return false;
            }

            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ChapterExists(Guid id)
        {
            return _context.Chapters.Any(e => e.Id == id);
        }

    }
}

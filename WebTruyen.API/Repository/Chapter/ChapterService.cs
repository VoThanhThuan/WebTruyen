using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
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
            chapter.Views += 1;
            _context.Entry(chapter).State = EntityState.Modified;
            return chapter?.ToViewModel();
        }

        public async Task<bool> PutChapter(Guid id, ChapterRequest request)
        {

            var chapter = await _context.Chapters.FindAsync(id);

            chapter.Ordinal = request.Ordinal ?? chapter.Ordinal;
            chapter.Name = string.IsNullOrEmpty(request.Name) ? chapter.Name : request.Name;
            chapter.DateTimeUp = request.DateTimeUp ?? chapter.DateTimeUp;
            chapter.Views = request.Views ?? chapter.Views;

            _context.Entry(chapter).State = EntityState.Modified;

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

        public async Task<ChapterVM> PostChapter(ChapterRequest request)
        {
            var comic = await _context.Comics.FindAsync(request.IdComic);
            if (comic is null)
                return null;

            var chapter = new ChapterVM()
            {
                Id = Guid.NewGuid(),
                Ordinal = request.Ordinal??1,
                Name = request.Name,
                DateTimeUp = DateTime.Now,
                Views = 0,
                IdComic = request.IdComic
            };

            _context.Chapters.Add(chapter.ToChapter());
            await _context.SaveChangesAsync();

            return chapter;
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

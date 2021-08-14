using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Service;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.Library.Enums;

namespace WebTruyen.API.Repository.Chapter
{
    public class ChapterService : IChapterService
    {
        private readonly ComicDbContext _context;
        private readonly IStorageService _storage;

        public ChapterService(ComicDbContext context, IStorageService storageService)
        {
            _context = context;
            _storage = storageService;
        }
        public async Task<IEnumerable<ChapterVM>> GetChapters()
        {
            return await _context.Chapters.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<ChapterVM> GetChapter(Guid id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            //chapter.Views += 1;
            _context.Entry(chapter).State = EntityState.Modified;
            return chapter?.ToViewModel();
        }

        public async Task<List<ChapterVM>> GetChaptersInComic(Guid idComic)
        {
            var chapter = await _context.Chapters
                .Where(x => x.IdComic == idComic)
                .Select(x => x.ToViewModel())
                .ToListAsync();
            return chapter;
        }

        public async Task<int> PutChapter(Guid id, ChapterRequest request)
        {
            var chapter = await _context.Chapters.FindAsync(id);

            if (chapter is null)
                return StatusCodes.Status404NotFound;

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
                    return StatusCodes.Status500InternalServerError;
                }
                else
                {
                    throw;
                }
            }


            return StatusCodes.Status200OK;
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

            //Add chapter
            _context.Chapters.Add(chapter.ToChapter());
            await _context.SaveChangesAsync();

            //đường dẫn thư mục cho chap mới
            var path = $@"comic-collection/{comic.Id}/chapter{chapter.Ordinal}";
            //Tạo folder cho chapter
            _storage.CreateDirectory(path);

            return chapter;
        }

        public async Task<int> DeleteChapter(Guid id)
        {
            //xóa danh sách image =========================================================================================
            var listImage = _context.Pages.Where(x => x.IdChapter == id);
            if(!listImage.Any())
            {
                foreach (var item in listImage)
                {
                    await _storage.DeleteFileAsync(item.Image);
                    _context.Pages.Remove(item);

                }
                await _context.SaveChangesAsync();
            }
            //xóa report =========================================================================================
            var report = _context.Report.Where(x => x.IdChapter == id);
            if(!report.Any())
            {
                _context.Report.RemoveRange(report);
                await _context.SaveChangesAsync();
            }
            //xóa thông báo =========================================================================================
            var announcement = _context.NewComicAnnouncements.Where(x => x.IdChapter == id);
            if(!announcement.Any())
            {
                _context.NewComicAnnouncements.RemoveRange(announcement);
                await _context.SaveChangesAsync();
            }
            //Xóa chapter =========================================================================================
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return StatusCodes.Status404NotFound;
            }

            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool ChapterExists(Guid id)
        {
            return _context.Chapters.Any(e => e.Id == id);
        }

        public async Task<ChapterVM> GetLastChapter(Guid idComic)
        {
            return (await _context.Chapters.FirstOrDefaultAsync(x => x.IdComic == idComic))?.ToViewModel();
        }
    }
}

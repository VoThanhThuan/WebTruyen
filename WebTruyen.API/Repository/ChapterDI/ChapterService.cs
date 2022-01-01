using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Service;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Enums;
using WebTruyen.API.Repository.ChapterDI;
using WebTruyen.API.Repository.PageDI;
using WebTruyen.Library.Entities;

namespace WebTruyen.API.Repository.ChapterDI
{
    public class ChapterService : IChapterService
    {
        private readonly ComicDbContext _context;
        private readonly IPageService _page;
        private readonly IStorageService _storage;

        public ChapterService(ComicDbContext context, IStorageService storageService, IPageService page)
        {
            _context = context;
            _page = page;
            _storage = storageService;
        }
        public async Task<IEnumerable<ChapterAM>> GetChapters()
        {
            return await _context.Chapters.Select(x => x.ToApiModel()).ToListAsync();
        }

        public async Task<ChapterAM> GetChapter(Guid id)
        {
            var chapter = await _context.Chapters.Include(x => x.Comic).FirstOrDefaultAsync(x => x.Id == id);
            //var comic = await _context.Comics.FirstOrDefaultAsync(x => x.Id == chapter.IdComic);
            //if (comic != null)
            //    chapter.Name = comic.Name;
            //chapter.Views += 1;
            //_context.Entry(chapter).State = EntityState.Modified;
            var chapterAM = chapter?.ToApiModel();
            return chapterAM;
        }

        public async Task<ChapterAM> GetChapterOrder(string comicAliasName, float ordinal)
        {
            var comic = await _context.Comics.FirstOrDefaultAsync(x => x.NameAlias == comicAliasName);
            if (comic is null)
                return null;
            var chapter = await _context.Chapters.FirstOrDefaultAsync(x => x.IdComic == comic.Id && x.Ordinal.Equals(ordinal));
            //chapter.Views += 1;
            //_context.Entry(chapter).State = EntityState.Modified;
            return chapter?.ToApiModel();
        }

        public async Task<List<ChapterAM>> GetChaptersInComic(Guid idComic, int skip = 0, int take = 40)
        {
            var chapter = await _context.Chapters
                .Where(x => x.IdComic == idComic)
                .OrderByDescending(x => x.Ordinal)
                .Skip(skip).Take(take)
                .Select(x => x.ToApiModel())
                .ToListAsync();
            return chapter;
        }

        public async Task<int> PutChapter(Guid id, ChapterRequest request)
        {
            var comic = await _context.Comics.FindAsync(request.IdComic);
            if (comic is null)
                return StatusCodes.Status404NotFound;

            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter is null)
                return StatusCodes.Status404NotFound;

            //Đường dẫn thư mục chap truyện
            var path = $@"comic-collection/{comic.Id}/chapter{chapter.Ordinal}";

            if (request.Ordinal is not null) {
                if (!Equals(request.Ordinal, chapter.Ordinal)) {
                    var pathNew = $@"comic-collection/{comic.Id}/chapter{request.Ordinal}";
                    if (_storage.FolderExists(pathNew, security: true)) {
                        request.Ordinal += 0.1f;
                        pathNew = $@"comic-collection/{comic.Id}/chapter{request.Ordinal}";
                    }
                    await _page.MoveUrlPages(chapter.Id, $"chapter{chapter.Ordinal}", $"chapter{request.Ordinal}");
                    _storage.FolderMove(path, pathNew, security: true);
                    chapter.Ordinal = (float)request.Ordinal;
                    path = pathNew;
                }

            }

            chapter.Name = string.IsNullOrEmpty(request.Name) ? chapter.Name : request.Name;
            chapter.IsLock = request.IsLock ? true : false;

            //Tạo file xác định chapter khóa
            if (request.IsLock && !_storage.FileExists($@"{path}/chapter.isLock", security: true)) {
                //_storage.CreateFile($@"{path}/chapter.isLock", security: true);
                _storage.FileMove($@"{path}/chapter.isNotLock", $@"{path}/chapter.isLock", security: true);

            } else {
                //await _storage.DeleteFileAsync($@"{path}/chapter.isLock", security: true);
                _storage.FileMove($@"{path}/chapter.isLock", $@"{path}/chapter.isNotLock", security: true);
            }

            _context.Entry(chapter).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ChapterExists(id)) {
                    return StatusCodes.Status500InternalServerError;
                } else {
                    throw;
                }
            }


            return StatusCodes.Status200OK;
        }

        public async Task<ChapterAM> PostChapter(ChapterRequest request)
        {
            var comic = await _context.Comics.FindAsync(request.IdComic);
            if (comic is null)
                return null;

            var chapter = new ChapterAM() {
                Id = Guid.NewGuid(),
                Ordinal = request.Ordinal ?? 1,
                Name = request.Name,
                DateTimeUp = DateTime.Now,
                Views = 0,
                IsLock = request.IsLock,
                IdComic = request.IdComic
            };

            //Add chapter
            _context.Chapters.Add(chapter.ToChapter());

            comic.DateUpdate = DateTime.Now;

            await _context.SaveChangesAsync();


            //đường dẫn thư mục cho chap mới
            var path = $@"comic-collection/{comic.Id}/chapter{chapter.Ordinal}";
            //Tạo folder cho chapter
            _storage.CreateDirectory(path, security: true);
            //Tạo file xác định chapter khóa
            if (chapter.IsLock && !_storage.FileExists($@"{path}/chapter.isLock", security: true)) {
                _storage.CreateFile($@"{path}/chapter.isLock", security: true);
            } else {
                _storage.CreateFile($@"{path}/chapter.isNotLock", security: true);
            }

            await NotificationNewComic(comic.Id, chapter.Id);

            return chapter;
        }

        private async Task NotificationNewComic(Guid idComic, Guid idChapter)
        {
            var bookmarks = await _context.Bookmarks.Where(x => x.IdComic == idComic).ToListAsync();
            var nofitications = new List<Announcement>();
            foreach (var bookmark in bookmarks) {

                var nofitication = new Announcement() {
                    IsRead = false,
                    TimeCreate = DateTime.Now,
                    IdChapter = idChapter,
                    IdUser = bookmark.IdUser
                };
                nofitications.Add(nofitication);
            }
            await _context.NewComicAnnouncements.AddRangeAsync(nofitications);
            await _context.SaveChangesAsync();
        }

        private async Task DeleteForeignKey(Guid idChapter)
        {
            //xóa danh sách image =========================================================================================
            var listImage = _context.Pages.Where(x => x.IdChapter == idChapter);
            if (!listImage.Any()) {
                //foreach (var item in listImage)
                //{
                //    await _storage.DeleteFileAsync(item.Image, security: true);
                //    _context.Pages.Remove(item);

                //}
                _context.Pages.RemoveRange(listImage);
                await _context.SaveChangesAsync();
            }
            //xóa report =========================================================================================
            var report = _context.Report.Where(x => x.IdChapter == idChapter);
            if (!report.Any()) {
                _context.Report.RemoveRange(report);
                await _context.SaveChangesAsync();
            }
            //xóa thông báo =========================================================================================
            var announcement = _context.NewComicAnnouncements.Where(x => x.IdChapter == idChapter);
            if (!announcement.Any()) {
                _context.NewComicAnnouncements.RemoveRange(announcement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteChapterInComic(Guid idComic)
        {
            var chapters = await _context.Chapters.Where(x => x.IdComic == idComic).ToListAsync();
            //Xóa các bảng kết nối với chapter
            foreach (var chapter in chapters) {
                await DeleteForeignKey(chapter.Id);
            }

            //Xóa chapter =========================================================================================
            _context.Chapters.RemoveRange(chapters);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;

        }

        public async Task<int> DeleteChapter(Guid id)
        {
            //Tìm Chapter
            var chapter = await _context.Chapters.FindAsync(id);

            if (chapter == null) {
                return StatusCodes.Status404NotFound;
            }

            //đường dẫn thư mục cho chap 
            var path = $@"comic-collection/{chapter.IdComic}/chapter{chapter.Ordinal}";
            //Xóa folder chứa hình của chapter
            await _storage.DeleteFolderAsync(path, security: true);

            //Xóa các bảng kết nối với chapter
            await DeleteForeignKey(chapter.Id);

            //Xóa chapter =========================================================================================
            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool ChapterExists(Guid id)
        {
            return _context.Chapters.Any(e => e.Id == id);
        }

        public async Task<ChapterAM> GetLastChapter(Guid idComic)
        {
            return (_context.Chapters.Where(x => x.IdComic == idComic).OrderBy(x => x.Ordinal).Last())?.ToApiModel();
        }
        public async Task<List<ChapterAM>> GetNewChapters(Guid idComic, int amount)
        {
            return await _context.Chapters.Where(x => x.IdComic == idComic).OrderByDescending(x => x.DateTimeUp).Take(amount).Select(x => x.ToApiModel()).ToListAsync();
        }
    }
}

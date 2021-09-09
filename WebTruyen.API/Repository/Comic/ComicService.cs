using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Repository.Chapter;
using WebTruyen.API.Service;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Comic
{
    public class ComicService : IComicService
    {
        private readonly ComicDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IChapterService _chapterService;

        public ComicService(ComicDbContext context, IStorageService storageService, IChapterService chapterService)
        {
            _context = context;
            _storageService = storageService;
            _chapterService = chapterService;
        }
        public async Task<IEnumerable<ComicVM>> GetComics()
        {
            return await _context.Comics.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<ComicVM> GetComic(Guid id)
        {
            var comic = await _context.Comics.FindAsync(id);
            var comicInGenre = await _context.ComicInGenres.Where(x => x.IdComic == comic.Id).ToListAsync();
            var genres = new List<GenreVM>();
            foreach (var cig in comicInGenre)
            {
                var geren = await _context.Genres.FindAsync(cig.IdComic, cig.IdGenre);
                genres.Add(geren.ToViewModel());
            }
            var comicView = comic?.ToViewModel(genres);

            return comicView;
        }

        public async Task<ComicVM> GetComic(string nameAlias)
        {
            var comic = await _context.Comics.FirstOrDefaultAsync(x => x.NameAlias == nameAlias);

            var comicInGenre = await _context.ComicInGenres.Where(x => x.IdComic == comic.Id).ToListAsync();
            var genres = new List<GenreVM>();
            foreach (var cig in comicInGenre)
            {
                var geren = await _context.Genres.FindAsync(cig.IdGenre);
                genres.Add(geren.ToViewModel());
            }
            var comicView = comic?.ToViewModel(genres);
            return comicView;
        }

        public async Task<bool> PutComic(Guid id, ComicRequest request)
        {
            var text = new TextService();
            var comic = await _context.Comics.FindAsync(request.Id);
            if (comic == null)
                return false;
            comic.AnotherNameOfComic = string.IsNullOrEmpty(text.RemoveSpaces(request.AnotherNameOfComic)) == true ? comic.AnotherNameOfComic : request.AnotherNameOfComic;
            comic.Author = string.IsNullOrEmpty(text.RemoveSpaces(request.Author)) == true ? comic.Author : request.Author;
            comic.Status = request.Status??comic.Status;
            comic.Description = string.IsNullOrEmpty(text.RemoveSpaces(request.Description)) == true ? comic.Description : request.Description;

            if (!string.IsNullOrEmpty(request.Name))
            {
                comic.Name = request.Name;
                //Lưu name alias có dạnh như [ a-b-c ]
                comic.NameAlias = new TextService().ConvertToUnSign(request.Name).Replace(" ", "-");

            }

            var path = $"comic-collection/{comic.Id}";
            if (request.Thumbnail != null)
            {
                if(comic.Thumbnail != null)
                    await _storageService.DeleteFileAsync(comic.Thumbnail, security: true);
                comic.Thumbnail = $@"api/Pages/image?name={await _storageService.SaveFile(request.Thumbnail, path, security: true)}";
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComicExists(id))
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

        public async Task<ComicVM> PostComic(ComicRequest request)
        {
            var comic = request.ToComic();
            comic.Id = Guid.NewGuid();

            //Lưu name alias có dạnh như [ a-b-c ]
            comic.NameAlias = new TextService().ConvertToUnSign(request.Name).Replace(" ", "-");
            var path = $@"comic-collection/{comic.Id}";
            //Tạo thư mục truyện mới
            var folder = _storageService.CreateDirectory(path, security: true);
            //Lưu hình ảnh
            if (!folder.Exists)
                return null;

            comic.Thumbnail = $@"api/Pages/image?name={await _storageService.SaveFile(request.Thumbnail, path, security: true)}";

            _context.Comics.Add(comic);
            await _context.SaveChangesAsync();

            return comic.ToViewModel();

        }

        public async Task<int> DeleteComic(Guid id)
        {
            var comic = await _context.Comics.FindAsync(id);
            if (comic == null)
            {
                return StatusCodes.Status404NotFound;
            }
            

            // Xóa ComicInGenres
            var cig = await _context.ComicInGenres.Where(x => x.IdComic == id).ToListAsync();
            if (!cig.Any())
            {
                _context.ComicInGenres.RemoveRange(cig);
            }
            // Xóa Chapters
            var chapter = await _chapterService.DeleteChapterInComic(id);
            Console.WriteLine($">> Tình trạng xóa chapter >> {chapter}");

            // Xóa TranslationOfUsers
            var trans = await _context.TranslationOfUsers.Where(x => x.IdComic == id).ToListAsync();
            if (!trans.Any())
            {
                _context.TranslationOfUsers.RemoveRange(trans);
            }
            // Xóa Bookmarks
            var bookmarks = await _context.Bookmarks.Where(x => x.IdComic == id).ToListAsync();
            if (!bookmarks.Any())
            {
                _context.Bookmarks.RemoveRange(bookmarks);
            }

            // Xóa HistoryReads
            var history = await _context.HistoryReads.Where(x => x.IdComic == id).ToListAsync();
            if (!history.Any())
            {
                _context.HistoryReads.RemoveRange(history);
            }

            var path = $@"comic-collection/{comic.Id}";
            //Xóa Comic
            var resultRemove = await _storageService.DeleteFolderAsync(path, security: true);
            if (resultRemove == StatusCodes.Status500InternalServerError)
                return StatusCodes.Status500InternalServerError;

            _context.Comics.Remove(comic);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool ComicExists(Guid id)
        {
            return _context.Comics.Any(e => e.Id == id);
        }

        private async Task<int> DeleteFile(string fileName)
        {
            return await _storageService.DeleteFileAsync(fileName, security: true);
        }
    }
}

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
using WebTruyen.API.Repository.ComicDI;
using WebTruyen.API.Repository.ChapterDI;
using Microsoft.AspNetCore.Identity;
using WebTruyen.Library.Entities;

namespace WebTruyen.API.Repository.ComicDI
{
    public class ComicService : IComicService
    {
        private readonly ComicDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IChapterService _chapterService;
        private readonly UserManager<User> _userManager; //Thư viện quản lý user


        public ComicService(ComicDbContext context, IStorageService storageService, IChapterService chapterService, UserManager<User> userManager)
        {
            _context = context;
            _storageService = storageService;
            _chapterService = chapterService;
            _userManager = userManager;
        }
        public async Task<ListComicAM> GetComics(int skip = 0, int take = 20)
        {
            var comic = await _context.Comics.OrderByDescending(x => x.DateUpdate).Skip(skip).Take(take).Select(x => x.ToApiModel()).ToListAsync();
            var comicPage = new ListComicAM() {
                Skip = skip,
                Take = take,
                Total = comic.Count,
                Comic = comic
            };
            return comicPage;
        }

        public async Task<ListComicAM> GetComicsOfUser(Guid idUser, int skip = 0, int take = 20)
        {
            var comic = await _context.Comics.Where(x => x.IdPoster == idUser)
                .OrderByDescending(x => x.DateUpdate)
                .Skip(skip).Take(take)
                .Select(x => x.ToApiModel()).ToListAsync();
            var comicPage = new ListComicAM() {
                Skip = skip,
                Take = take,
                Total = comic.Count,
                Comic = comic
            };
            return comicPage;
        }

        public async Task<ListComicAM> SearchComics(string contenSearch, int skip = 0, int take = 5)
        {
            var comics = await _context.Comics.Where(x => x.Name.ToLower().Contains(contenSearch))
                .Skip(skip).Take(10)
                .Select(x => x.ToApiModel()).ToListAsync();
            var comicPage = new ListComicAM() {
                Skip = skip,
                Take = take,
                Total = comics.Count,
                Comic = comics
            };
            return comicPage;
        }

        public async Task<ListComicAM> GetComicsInGenre(int idGenre, int skip = 0, int take = 20)
        {
            var comics = await _context.ComicInGenres.Where(x => x.IdGenre == idGenre)
                .Include(x => x.Comic)
                .Skip(skip).Take(take)
                .Select(x => x.Comic.ToApiModel()).ToListAsync();
            var comicPage = new ListComicAM() {
                Skip = skip,
                Take = take,
                Total = comics.Count,
                Comic = comics
            };
            return comicPage;
        }

        public async Task<ComicAM> GetComic(Guid id)
        {
            var comic = await _context.Comics.FindAsync(id);
            var comicInGenre = await _context.ComicInGenres.Where(x => x.IdComic == comic.Id).ToListAsync();
            var genres = new List<GenreAM>();
            foreach (var cig in comicInGenre) {
                var geren = await _context.Genres.FindAsync(cig.IdGenre);
                genres.Add(geren.ToApiModel());
            }
            var comicView = comic?.ToApiModel(genres);

            return comicView;
        }


        public async Task<ComicAM> GetComic(string nameAlias)
        {
            var comic = await _context.Comics.FirstOrDefaultAsync(x => x.NameAlias == nameAlias);

            var comicInGenre = await _context.ComicInGenres.Where(x => x.IdComic == comic.Id).ToListAsync();
            var genres = new List<GenreAM>();
            foreach (var cig in comicInGenre) {
                var geren = await _context.Genres.FindAsync(cig.IdGenre);
                genres.Add(geren.ToApiModel());
            }
            var comicView = comic?.ToApiModel(genres);
            return comicView;
        }


        public async Task<bool> PutComic(Guid id, ComicRequest request, Guid idPoster)
        {
            var text = new TextService();
            var comic = await _context.Comics.FindAsync(request.Id);
            if (comic == null) {
                return false;
            }
            if (comic.IdPoster != idPoster) {
                var user = await (await _context.Users.FirstOrDefaultAsync(x => x.Id == comic.IdPoster)).ToApiModel(_userManager);
                if (user.RoleName != "Admin") {
                    return false;
                }
            }
            comic.AnotherNameOfComic = string.IsNullOrEmpty(text.RemoveSpaces(request.AnotherNameOfComic)) == true ? comic.AnotherNameOfComic : request.AnotherNameOfComic;
            comic.Author = string.IsNullOrEmpty(text.RemoveSpaces(request.Author)) == true ? comic.Author : request.Author;
            comic.Status = request.Status ?? comic.Status;
            comic.Description = string.IsNullOrEmpty(text.RemoveSpaces(request.Description)) == true ? comic.Description : request.Description;

            if (!string.IsNullOrEmpty(request.Name)) {
                comic.Name = request.Name;
                //Lưu name alias có dạnh như [ a-b-c ]
                comic.NameAlias = new TextService().ConvertToUnSign(request.Name).Replace(" ", "-");

            }

            var path = $"comic-collection/{comic.Id}";
            if (request.Thumbnail != null) {
                if (comic.Thumbnail != null && _storageService.ImageIsValid(request.Thumbnail)) {
                    await _storageService.DeleteFileAsync(comic.Thumbnail, security: true);
                    comic.Thumbnail = $@"api/Pages/image?name={await _storageService.SaveFile(request.Thumbnail, path, security: true)}";
                }
            }
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ComicExists(id)) {
                    return false;
                } else {
                    throw;
                }
            }

            return true;
        }

        public async Task<ComicAM> PostComic(ComicRequest request, Guid idPoster, string namePoster)
        {
            var comic = request.ToComic();
            comic.Id = Guid.NewGuid();
            comic.IdPoster = idPoster;
            comic.NamePoster = namePoster;
            //Lưu name alias có dạnh như [ a-b-c ]
            comic.NameAlias = new TextService().ConvertToUnSign(request.Name).Replace(" ", "-");
            var path = $@"comic-collection/{comic.Id}";
            //Tạo thư mục truyện mới
            var folder = _storageService.CreateDirectory(path, security: true);
            //Lưu hình ảnh
            if (!folder.Exists)
                return null;

            if (request.Thumbnail != null && _storageService.ImageIsValid(request.Thumbnail))
                comic.Thumbnail = $@"api/Pages/image?name={await _storageService.SaveFile(request.Thumbnail, path, security: true)}";

            _context.Comics.Add(comic);
            await _context.SaveChangesAsync();

            return comic.ToApiModel();

        }

        public async Task<int> DeleteComic(Guid id)
        {
            var comic = await _context.Comics.FindAsync(id);
            if (comic == null) {
                return StatusCodes.Status404NotFound;
            }


            // Xóa ComicInGenres
            var cig = await _context.ComicInGenres.Where(x => x.IdComic == id).ToListAsync();
            if (!cig.Any()) {
                _context.ComicInGenres.RemoveRange(cig);
            }
            // Xóa Chapters
            var chapter = await _chapterService.DeleteChapterInComic(id);
            Console.WriteLine($">> Tình trạng xóa chapter >> {chapter}");

            // Xóa TranslationOfUsers
            var trans = await _context.TranslationOfUsers.Where(x => x.IdComic == id).ToListAsync();
            if (!trans.Any()) {
                _context.TranslationOfUsers.RemoveRange(trans);
            }
            // Xóa Bookmarks
            var bookmarks = await _context.Bookmarks.Where(x => x.IdComic == id).ToListAsync();
            if (!bookmarks.Any()) {
                _context.Bookmarks.RemoveRange(bookmarks);
            }

            // Xóa HistoryReads
            var history = await _context.HistoryReads.Where(x => x.IdComic == id).ToListAsync();
            if (!history.Any()) {
                _context.HistoryReads.RemoveRange(history);
            }

            // Xóa comment
            var comment = await _context.Comments.Where(x => x.IdComic == id).ToListAsync();
            if (!comment.Any()) {
                _context.Comments.RemoveRange(comment);
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

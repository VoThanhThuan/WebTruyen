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
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Comic
{
    public class ComicService : IComicService
    {
        private readonly ComicDbContext _context;
        private readonly IStorageService _storageService;

        public ComicService(ComicDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<IEnumerable<ComicVM>> GetComics()
        {
            return await _context.Comics.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<ComicVM> GetComic(Guid id)
        {
            var comic = await _context.Comics.FindAsync(id);

            return comic?.ToViewModel();
        }

        public async Task<ComicVM> GetComic(string nameAlias)
        {
            var comic = await _context.Comics.FirstOrDefaultAsync(x => x.NameAlias == nameAlias);

            return comic?.ToViewModel();
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
                    await _storageService.DeleteFileAsync(comic.Thumbnail);
                comic.Thumbnail = await _storageService.SaveFile(request.Thumbnail, path);

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

        public async Task<bool> PostComic(ComicRequest request)
        {
            var comic = request.ToComic();

            //Lưu name alias có dạnh như [ a-b-c ]
            comic.NameAlias = new TextService().ConvertToUnSign(request.Name).Replace(" ", "-");
            var path = $@"comic-collection/{comic.Id}";
            //Tạo thư mục truyện mới
            var folder = _storageService.CreateDirectory(path);
            //Lưu hình ảnh
            if (!folder.Exists)
                return false;

            comic.Thumbnail = await _storageService.SaveFile(request.Thumbnail, path);

            _context.Comics.Add(comic);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> DeleteComic(Guid id)
        {
            var comic = await _context.Comics.FindAsync(id);
            if (comic == null)
            {
                return false;
            }

            _context.Comics.Remove(comic);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ComicExists(Guid id)
        {
            return _context.Comics.Any(e => e.Id == id);
        }

        private async Task<int> DeleteFile(string fileName)
        {
            return await _storageService.DeleteFileAsync(fileName);
        }
    }
}

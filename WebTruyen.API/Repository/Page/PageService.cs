using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebTruyen.API.Service;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Page
{
    public class PageService : IPageService
    {
        private readonly ComicDbContext _context;
        private readonly IStorageService _storage;
        public PageService(ComicDbContext context, IStorageService storage)
        {
            _context = context;
            _storage = storage;
        }

        public async Task<IEnumerable<PageVM>> GetPages()
        {
            return await _context.Pages.OrderBy(x => x.SortOrder).Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<PageVM> GetPage(Guid id)
        {
            var page = await _context.Pages.FindAsync(id);

            return page?.ToViewModel();
        }

        public async Task<IEnumerable<PageVM>> GetPagesWithChapter(Guid idChapter)
        {
            var pages = await _context.Pages
                .Where(x => x.IdChapter == idChapter)
                .OrderBy(x => x.SortOrder)
                .Select(x => x.ToViewModel())
                .ToListAsync();

            return pages;
        }

        public async Task<List<PageVM>> GetPageOfChapter(Guid idChapter)
        {
            var page = await _context.Pages
                .Where(x => x.IdChapter == idChapter)
                .Select(x => x.ToViewModel())
                .ToListAsync();

            return page;
        }


        public async Task<bool> PutPage(Guid id, PageRequest request)
        {

            var page = await _context.Pages.FindAsync(id);
            var path = Path.GetDirectoryName(page.Image);

            var lastOrder = _context.Pages
                .Where(x => x.Id == request.IdChapter)
                .OrderBy(x => x.SortOrder)
                .Last().SortOrder;

            page.Image = request.Image == null ? page.Image : await _storage.SaveFile(request.Image, path);
            page.SortOrder = request.SortOrder ?? lastOrder + 1;
            page.IdChapter = request.IdChapter;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageExists(id))
                    return false;
                throw;
            }

            return true;
        }

        public async Task<bool> PostPages(Guid idChapter, List<string> images)
        {
            var chapAndComic = (from chap in _context.Chapters
                                where chap.Id == idChapter
                                join com in _context.Comics
                                    on chap.IdComic equals com.Id
                                select new { chap, com }).First();

            //lấy đường dẫn
            var path = $@"comic/{chapAndComic.com.NameAlias}/chapter{chapAndComic.chap.Ordinal}";
            //Tạo folder cho chapter
            _storage.CreateDirectory(path);

            var pages = images.Select((t, i) => new Library.Entities.Page()
            {
                Id = Guid.NewGuid(),
                Image = t,
                SortOrder = i,
                IdChapter = idChapter
            }).ToList();

            _context.Pages.AddRange(pages);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PostPages(Guid idChapter, List<IFormFile> images)
        {
            var chapAndComic = (from chap in _context.Chapters
                                where chap.Id == idChapter
                                join com in _context.Comics
                                on chap.IdComic equals com.Id
                                select new { chap, com }).First();

            //lấy đường dẫn
            var path = $@"comic/{chapAndComic.com.NameAlias}/chapter{chapAndComic.chap.Ordinal}";
            //Tạo folder cho chapter
            _storage.CreateDirectory(path);
            images = images.OrderBy(x => x.FileName).ToList();

            var pages = images.Select((t, i) => new Library.Entities.Page()
            {
                Id = Guid.NewGuid(),
                Image = _storage.SaveFile(t, path).Result,
                SortOrder = i,
                IdChapter = idChapter
            }).ToList();

            _context.Pages.AddRange(pages);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PageVM> PostPage(Guid idChapter, PageRequest request)
        {
            var chapAndComic = (from chap in _context.Chapters
                                where chap.Id == idChapter
                                join com in _context.Comics
                                    on chap.IdComic equals com.Id
                                select new { chap, com }).First();

            //lấy đường dẫn
            var path = $@"comic/{chapAndComic.com.NameAlias}/chapter{chapAndComic.chap.Ordinal}";
            //Tạo folder cho chapter
            _storage.CreateDirectory(path);

            var lastOrder = _context.Pages.Where(x => x.IdChapter == idChapter).OrderBy(x => x.SortOrder).Last().SortOrder;

            var page = new Library.Entities.Page()
            {
                Id = request.Id ?? Guid.NewGuid(),
                Image = string.IsNullOrEmpty(request.Link) ? _storage.SaveFile(request.Image, path).Result : request.Link,
                SortOrder = request.SortOrder ?? lastOrder + 1,
                IdChapter = request.IdChapter
            };

            _context.Pages.Add(page);

            await _context.SaveChangesAsync();

            return page.ToViewModel();
        }

        public async Task<bool> DeletePage(Guid id)
        {
            var page = await _context.Pages.FindAsync(id);

            if (page == null)
            {
                return false;
            }
            await _storage.DeleteFileAsync(page.Image);
            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool PageExists(Guid id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }

    }
}

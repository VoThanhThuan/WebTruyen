using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            return await _context.Pages.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<PageVM> GetPage(Guid id)
        {
            var page = await _context.Pages.FindAsync(id);

            return page?.ToViewModel();
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
            //_context.Entry(request.ToPage()).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PageExists(id))
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return true;
        }

        public async Task<bool> PostPage(Guid idChapter, PageRequest request)
        {
            var chapter = await _context.Chapters.FindAsync(idChapter);
            //Tạo folder cho chapter
            Directory.CreateDirectory($"chapter{chapter.Ordinal}");
            //lấy đường dẫn
            var path = $@"comic/{chapter.Comic.NameAlias}/chapter{chapter.Ordinal}";



            var pages = request.Image.Select(t => new Library.Entities.Page()
                {
                    Id = Guid.NewGuid(),
                    Image = string.IsNullOrEmpty(request.Link) ? _storage.SaveFile(t, path).Result : request.Link,
                    SortOrder = request.SortOrder,
                    IdChapter = request.IdChapter
                })
                .ToArray();

            _context.Pages.AddRange(pages);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePage(Guid id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return false;
            }

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

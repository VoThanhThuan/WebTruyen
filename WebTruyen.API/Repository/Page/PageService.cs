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
using WebTruyen.Library.Entities.ApiModel;

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

        public async Task<IEnumerable<PageAM>> GetPages()
        {
            var pages = _context.Pages.Select(x => x).ToList();
            return await _context.Pages.OrderBy(x => x.SortOrder).Select(x => x.ToApiModel()).ToListAsync();
        }

        public async Task<PageAM> GetPage(Guid id)
        {
            var page = await _context.Pages.FindAsync(id);

            return page?.ToApiModel();
        }

        public async Task<IEnumerable<PageAM>> GetPagesInChapter(Guid idChapter)
        {
            var pages = await _context.Pages
                .Where(x => x.IdChapter == idChapter)
                .OrderBy(x => x.SortOrder)
                .Select(x => x.ToApiModel())
                .ToListAsync();

            return pages;
        }

        public async Task<int> PutPage(Guid id, PageRequest request)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page is null) return StatusCodes.Status404NotFound;

            var path = Path.GetDirectoryName(page.Image);

            //Xóa hình cũ
            await _storage.DeleteFileAsync(page.Image);

            var lastOrder = _context.Pages
                .Where(x => x.Id == request.IdChapter)
                .OrderBy(x => x.SortOrder)
                .Last().SortOrder;

            page.Image = request.Image == null ? page.Image : $"api/Pages/image?name={(await _storage.SaveFile(request.Image, path, security: true))}";
            page.SortOrder = request.SortOrder ?? lastOrder + 1;
            page.IdChapter = request.IdChapter;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageExists(id))
                    return StatusCodes.Status409Conflict;
                throw;
            }

            return StatusCodes.Status200OK;
        }

        public async Task<int> PutPages(Guid idChapter, List<IFormFile> images, bool isLock)
        {
            var chapAndComic = (from chap in _context.Chapters
                where chap.Id == idChapter
                join com in _context.Comics
                    on chap.IdComic equals com.Id
                select new { chap, com }).First();

            //đường dẫn thư mục của chapter 
            var path = $@"comic-collection/{chapAndComic.com.Id}/chapter{chapAndComic.chap.Ordinal}";
            if (_storage.FolderExists(path, security: true))
            {
                //Xóa các chap cũ
                //  <> Xóa Hình
                var resultRemove = await _storage.DeleteFolderAsync(path, security: true);

                if (resultRemove == StatusCodes.Status500InternalServerError)
                    return StatusCodes.Status500InternalServerError;
                _storage.CreateDirectory(path, security: true);
            }
            else
            {
                _storage.CreateDirectory(path, security: true);
            }
            

            // <> Xóa CSDL
            var pagesInDb = await _context.Pages.Where(x => x.IdChapter == idChapter).ToListAsync();
            _context.Pages.RemoveRange(pagesInDb);

            //Lưu những chap mới
            images = images.OrderBy(x => x.FileName).ToList();
            var pages = images.Select((t, i) => new Library.Entities.Page()
            {
                Id = Guid.NewGuid(),
                Image = $@"api/Pages/image?name={_storage.SaveFile(t, path, security: true).Result}",
                SortOrder = i,
                IdChapter = idChapter
            }).ToList();

            _context.Pages.AddRange(pages);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCodes.Status409Conflict;
            }

            return StatusCodes.Status200OK;
        }

        public async Task MoveUrlPages(Guid idChapter, string oldChapter, string newChapter)
        {
            var pages = await _context.Pages.Where(x => x.IdChapter == idChapter).ToListAsync();
            
            if (pages.Any())
            {
                foreach (var page in pages)
                {
                    page.Image = page.Image.Replace($@"/{oldChapter}/", $@"/{newChapter}/");
                }
                _context.Pages.AttachRange(pages);
                await _context.SaveChangesAsync();
            }
                
        }

        public async Task<PageAM> PostPage(Guid idChapter, PageRequest request)
        {
            var chapAndComic = (from chap in _context.Chapters
                where chap.Id == idChapter
                join com in _context.Comics
                    on chap.IdComic equals com.Id
                select new { chap, com }).First();

            //lấy đường dẫn
            var path = $@"comic-collection/{chapAndComic.com.Id}/chapter{chapAndComic.chap.Ordinal}";
            //Tạo folder cho chapter
            _storage.CreateDirectory(path, security: true);

            var lastOrder = _context.Pages.Where(x => x.IdChapter == idChapter).OrderBy(x => x.SortOrder).Last().SortOrder;

            var page = new Library.Entities.Page()
            {
                Id = request.Id ?? Guid.NewGuid(),
                Image = string.IsNullOrEmpty(request.Link) ? $@"api/Pages/image?name={_storage.SaveFile(request.Image, path, security: true).Result}" : request.Link,
                SortOrder = request.SortOrder ?? lastOrder + 1,
                IdChapter = request.IdChapter
            };

            _context.Pages.Add(page);

            await _context.SaveChangesAsync();

            return page.ToApiModel();
        }


        /// <summary>
        /// hình ảnh là đường link từ web khác
        /// </summary>
        /// <param name="idChapter"></param>
        /// <param name="images"></param>
        /// <returns></returns>
        public async Task<int> PostPages(Guid idChapter, List<string> images)
        {
            var chapAndComic = (from chap in _context.Chapters
                                where chap.Id == idChapter
                                join com in _context.Comics
                                    on chap.IdComic equals com.Id
                                select new { chap, com }).First();

            //lấy đường dẫn
            var path = $@"comic-collection//{chapAndComic.com.NameAlias}/chapter{chapAndComic.chap.Ordinal}";
            //Tạo folder cho chapter
            _storage.CreateDirectory(path, security: true);

            var pages = images.Select((t, i) => new Library.Entities.Page()
            {
                Id = Guid.NewGuid(),
                Image = t,
                SortOrder = i,
                IdChapter = idChapter
            }).ToList();

            _context.Pages.AddRange(pages);

            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        /// <summary>
        /// Lưu thông tin trang truyện và lưu hình vào thư mục server
        /// </summary>
        /// <param name="idChapter"></param>
        /// <param name="images">Danh sách hình ảnh</param>
        /// <returns></returns>
        public async Task<int> PostPages(Guid idChapter, List<IFormFile> images)
        {
            var chapAndComic = (from chap in _context.Chapters
                                where chap.Id == idChapter
                                join com in _context.Comics
                                on chap.IdComic equals com.Id
                                select new { chap, com }).First();

            //lấy đường dẫn
            var path = $@"comic-collection/{chapAndComic.com.Id}/chapter{chapAndComic.chap.Ordinal}";

            images = images.OrderBy(x => x.FileName).ToList();

            var pages = images.Select((t, i) => new Library.Entities.Page()
            {
                Id = Guid.NewGuid(),
                Image = $@"api/Pages/image?name={_storage.SaveFile(t, path, security: true).Result}",
                SortOrder = i,
                IdChapter = idChapter
            }).ToList();

            _context.Pages.AddRange(pages);

            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }


        public async Task<int> DeletePage(Guid id)
        {
            var page = await _context.Pages.FindAsync(id);

            if (page == null)
            {
                return StatusCodes.Status404NotFound;
            }
            await _storage.DeleteFileAsync(page.Image);
            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();

            return StatusCodes.Status404NotFound;
        }

        private bool PageExists(Guid id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }


    }
}

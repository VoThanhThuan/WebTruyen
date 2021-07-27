using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Page
{
    public class PageService : IPageService
    {
        private readonly ComicDbContext _context;

        public PageService(ComicDbContext context)
        {
            _context = context;
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

        public async Task<bool> PutPage(Guid id, PageVM request)
        {
            _context.Entry(request.ToPage()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageExists(id))
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

        public async Task<bool> PostPage(PageVM request)
        {
            _context.Pages.Add(request.ToPage());
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Comic
{
    public class ComicService : IComicService
    {
        private readonly ComicDbContext _context;

        public ComicService(ComicDbContext context)
        {
            _context = context;
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

        public async Task<bool> PutComic(Guid id, ComicVM request)
        {
            _context.Entry(request.ToComic()).State = EntityState.Modified;

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

        public async Task<bool> PostComic(ComicVM request)
        {
            _context.Comics.Add(request.ToComic());
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
    }
}

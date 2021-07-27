using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.ComicInGenre
{
    public class ComicInGenreService : IComicInGenreService
    {
        private readonly ComicDbContext _context;

        public ComicInGenreService(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComicInGenreVM>> GetComicInGenres()
        {
            return await _context.ComicInGenres.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<ComicInGenreVM> GetComicInGenre(int id)
        {
            var comicInGenre = await _context.ComicInGenres.FindAsync(id);

            return comicInGenre?.ToViewModel();
        }

        public async Task<bool> PutComicInGenre(int id, ComicInGenreVM request)
        {
            _context.Entry(request.ToComicInGenre()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComicInGenreExists(id))
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

        public async Task<bool> PostComicInGenre(ComicInGenreVM request)
        {
            _context.ComicInGenres.Add(request.ToComicInGenre());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ComicInGenreExists(request.IdGenre))
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

        public async Task<bool> DeleteComicInGenre(int id)
        {
            var comicInGenre = await _context.ComicInGenres.FindAsync(id);
            if (comicInGenre == null)
            {
                return false;
            }

            _context.ComicInGenres.Remove(comicInGenre);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ComicInGenreExists(int id)
        {
            return _context.ComicInGenres.Any(e => e.IdGenre == id);
        }
    }
}

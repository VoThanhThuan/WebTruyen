using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.API.Repository.ComicInGenreDI;

namespace WebTruyen.API.Repository.ComicInGenreDI
{
    public class ComicInGenreService : IComicInGenreService
    {
        private readonly ComicDbContext _context;

        public ComicInGenreService(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComicInGenreAM>> GetComicInGenres()
        {
            return await _context.ComicInGenres.Select(x => x.ToApiModel()).ToListAsync();
        }

        public async Task<ComicInGenreAM> GetComicInGenre(int id)
        {
            var comicInGenre = await _context.ComicInGenres.FindAsync(id);

            return comicInGenre?.ToApiModel();
        }

        public async Task<bool> PutComicInGenre(int id, ComicInGenreAM request)
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

        public async Task<bool> PutComicInGenres(Guid idComic, List<ComicInGenreAM> request)
        {
            if (!request.Any()) return false;
            var comic = await _context.ComicInGenres.FirstOrDefaultAsync(x => x.IdComic == idComic);
            if (comic != null)
            {
                _context.ComicInGenres.RemoveRange(comic);
                await _context.SaveChangesAsync();
            }

            var cig = request.Select(x => x.ToComicInGenre()).ToList();

            await _context.AddRangeAsync(cig);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PostComicInGenres(Guid idComic, List<ComicInGenreAM> request)
        {
            if (!request.Any()) return false;
            var comic = await _context.ComicInGenres.Where(x => x.IdComic == idComic).ToListAsync();
            if (comic == null) {
                return false;
            }

                var cig = request.Select(x => x.ToComicInGenre()).ToList();

            await _context.AddRangeAsync(cig);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PostComicInGenre(ComicInGenreAM request)
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

        public async Task<bool> DeleteComicInGenre(int idGenre)
        {
            var comicInGenre = await _context.ComicInGenres.Where(x => x.IdGenre == idGenre).ToListAsync();
            if (!comicInGenre.Any())
            {
                return false;
            }

            _context.ComicInGenres.RemoveRange(comicInGenre);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ComicInGenreExists(int id)
        {
            return _context.ComicInGenres.Any(e => e.IdGenre == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Genre
{
    public class GenreService : IGenreService
    {
        private readonly ComicDbContext _context;

        public GenreService(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenreVM>> GetGenres()
        {
            return await _context.Genres.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<GenreVM> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            return genre?.ToViewModel();
        }

        public async Task<bool> PutGenre(int id, GenreVM request)
        {
            _context.Entry(request.ToGenre()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        public async Task<bool> PostGenre(GenreVM request)
        {
            _context.Genres.Add(request.ToGenre());
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return false;
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}

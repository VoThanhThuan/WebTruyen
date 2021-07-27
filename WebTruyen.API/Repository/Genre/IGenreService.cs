using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Genre
{
    public interface IGenreService
    {
        public Task<IEnumerable<GenreVM>> GetGenres();
        public Task<GenreVM> GetGenre(int id);
        public Task<bool> PutGenre(int id, GenreVM request);
        public Task<bool> PostGenre(GenreVM request);
        public Task<bool> DeleteGenre(int id);

    }
}

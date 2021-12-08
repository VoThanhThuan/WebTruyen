using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.Genre
{
    public interface IGenreService
    {
        public Task<IEnumerable<GenreAM>> GetGenres();
        public Task<GenreAM> GetGenre(int id);
        public Task<bool> PutGenre(int id, GenreAM request);
        public Task<bool> PostGenre(GenreAM request);
        public Task<bool> DeleteGenre(int id);

    }
}

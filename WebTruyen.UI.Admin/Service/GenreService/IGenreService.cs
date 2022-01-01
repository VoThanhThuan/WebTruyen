using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Admin.Service.GenreService
{
    public interface IGenreService
    {
        public Task<List<GenreAM>> GetGenres();
        public Task<GenreAM> GetGenre(int id);
        public Task<int> PutGenre(int id, GenreAM request);
        public Task<int> PostGenre(GenreAM request);
        public Task<int> DeleteGenre(int id);
    }
}

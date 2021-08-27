using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Admin.Service.GenreService
{
    public interface IGenreService
    {
        public Task<List<GenreVM>> GetGenres();
        public Task<GenreVM> GetGenre(int id);
        public Task<int> PutGenre(int id, GenreVM request);
        public Task<int> PostGenre(GenreVM request);
        public Task<int> DeleteGenre(int id);
    }
}

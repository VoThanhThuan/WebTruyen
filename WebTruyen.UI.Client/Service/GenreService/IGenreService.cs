using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.GenreService
{
    public interface IGenreService
    {
        public Task<List<GenreVM>> GetGenres();
        public Task<GenreVM> GetGenre(int id);
    }
}

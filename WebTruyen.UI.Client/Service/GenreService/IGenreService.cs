using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.GenreService
{
    public interface IGenreService
    {
        public Task<List<GenreAM>> GetGenres();
        public Task<GenreAM> GetGenre(int id);
    }
}

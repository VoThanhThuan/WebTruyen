using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.ComicInGenreDI
{
    public interface IComicInGenreService
    {
        public Task<IEnumerable<ComicInGenreAM>> GetComicInGenres();
        public Task<ComicInGenreAM> GetComicInGenre(int id);
        public Task<bool> PutComicInGenre(int id, ComicInGenreAM request);
        public Task<bool> PutComicInGenres(Guid idComic, List<ComicInGenreAM> request);
        public Task<bool> PostComicInGenre(ComicInGenreAM request);
        public Task<bool> DeleteComicInGenre(int id);


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.ComicInGenre
{
    public interface IComicInGenreService
    {
        public Task<IEnumerable<ComicInGenreVM>> GetComicInGenres();
        public Task<ComicInGenreVM> GetComicInGenre(int id);
        public Task<bool> PutComicInGenre(int id, ComicInGenreVM request);
        public Task<bool> PutComicInGenres(Guid idComic, List<ComicInGenreVM> request);
        public Task<bool> PostComicInGenre(ComicInGenreVM request);
        public Task<bool> DeleteComicInGenre(int id);


    }
}

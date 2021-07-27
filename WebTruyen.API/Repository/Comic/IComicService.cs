using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Comic
{
    public interface IComicService
    {
        public Task<IEnumerable<ComicVM>> GetComics();
        public Task<ComicVM> GetComic(Guid id);
        public Task<bool> PutComic(Guid id, ComicVM request);
        public Task<bool> PostComic(ComicVM request);
        public Task<bool> DeleteComic(Guid id);

    }
}

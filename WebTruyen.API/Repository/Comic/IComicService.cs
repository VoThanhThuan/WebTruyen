using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Comic
{
    public interface IComicService
    {
        public Task<IEnumerable<ComicVM>> GetComics(int skip = 0, int take = 50);
        public Task<ComicVM> GetComic(Guid id);
        public Task<ComicVM> GetComic(string nameAlias);
        public Task<bool> PutComic(Guid id, ComicRequest request);
        public Task<ComicVM> PostComic(ComicRequest request);
        public Task<int> DeleteComic(Guid id);

    }
}

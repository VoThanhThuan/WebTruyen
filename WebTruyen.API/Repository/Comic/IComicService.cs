using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.Comic
{
    public interface IComicService
    {
        public Task<ListComicAM> GetComics(int skip = 0, int take = 50);
        public Task<ListComicAM> SearchComics(string contenSearch, int skip = 0, int take = 5);
        public Task<ListComicAM> GetComicsInGenre(int idGenre, int skip, int take);
        public Task<ComicAM> GetComic(Guid id);
        public Task<ComicAM> GetComic(string nameAlias);
        public Task<bool> PutComic(Guid id, ComicRequest request);
        public Task<ComicAM> PostComic(ComicRequest request);
        public Task<int> DeleteComic(Guid id);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.UI.Admin.RequestClient;

namespace WebTruyen.UI.Admin.Service.ComicService
{
    public interface IComicApiClient
    {
        public Task<List<ComicAM>> GetComics();
        public Task<string> GetImage(string url);
        public Task<ComicAM> GetComic(Guid id);
        public Task<ComicAM> GetComic(string nameAlias);
        public Task<int> PutComic(Guid id, ComicRequestClient request, List<GenreAM> genres);
        public Task<(HttpStatusCode StatusCode, ComicAM Content)> PostComic(ComicRequestClient request, List<GenreAM> genres);
        public Task<int> DeleteComic(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.UI.Admin.RequestClient;

namespace WebTruyen.UI.Admin.Service.ComicService
{
    public interface IComicApiClient
    {
        public Task<IEnumerable<ComicVM>> GetComics();
        public Task<string> GetImage(string url);
        public Task<ComicVM> GetComic(Guid id);
        public Task<ComicVM> GetComic(string nameAlias);
        public Task<int> PutComic(Guid id, ComicRequestClient request, List<GenreVM> genres);
        public Task<(HttpStatusCode StatusCode, ComicVM Content)> PostComic(ComicRequestClient request, List<GenreVM> genres);
        public Task<int> DeleteComic(Guid id);
    }
}

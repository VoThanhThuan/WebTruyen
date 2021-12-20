using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.ComicService
{
    public interface IComicApiClient
    {
        public Task<ListComicAM> GetComics(int skip = 0, int take = 40);
        public Task<ListComicAM> GetComicsInGenre(int idGenre, int skip = 0, int take = 40);
        public Task<ListComicAM> SearchComics(string contentSearch);
        public Task<ComicAM> GetComic(Guid id);
        public Task<ComicAM> GetComic(string nameAlias);
    }
}

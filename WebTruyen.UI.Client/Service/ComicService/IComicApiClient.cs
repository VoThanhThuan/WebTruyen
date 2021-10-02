using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.ComicService
{
    public interface IComicApiClient
    {
        public Task<List<ComicVM>> GetComics();
        public Task<ComicVM> GetComic(Guid id);
        public Task<ComicVM> GetComic(string nameAlias);
    }
}

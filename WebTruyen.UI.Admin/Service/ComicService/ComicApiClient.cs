using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Admin.Service.ComicService
{
    public class ComicApiClient : IComicApiClient
    {
        private readonly HttpClient _http;

        public ComicApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ComicVM>> GetComics()
        {   
            var result = await _http.GetFromJsonAsync<List<ComicVM>>("/api/Comics");
            var comic = result?.Select(x => { x.Thumbnail = $"{_http.BaseAddress}{x.Thumbnail}"; return x; }).ToList();
            return comic;
        }

        public async Task<ComicVM> GetComic(Guid id)
        {
            var result = await _http.GetFromJsonAsync<ComicVM>($"/api/Comics/{id}");
            if (result != null)
            {
                result.Thumbnail = $"{_http.BaseAddress}{result.Thumbnail}";
            }
            return result;
        }

        public async Task<ComicVM> GetComic(string nameAlias)
        {
            var result = await _http.GetFromJsonAsync<ComicVM>($"/api/Comics/detail?nameAlias={nameAlias}");
            if (result != null)
            {
                result.Thumbnail = $"{_http.BaseAddress}{result.Thumbnail}";
            }
            return result;
        }

        public Task<bool> PutComic(Guid id, ComicRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostComic(ComicRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComic(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

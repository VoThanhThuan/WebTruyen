using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.ComicService
{
    public class ComicApiClient : IComicApiClient
    {
        private HttpClient _http;
        ISessionStorageService _sessionStorage { get; set; }

        public ComicApiClient(HttpClient http, ISessionStorageService sessionStorage)
        {
            _http = http;
            _sessionStorage = sessionStorage;

        }

        private async Task GetSession()
        {
            var sessions = (await _sessionStorage.GetItemAsync<string>("Token"));
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        }

        public async Task<List<ComicAM>> GetComics()
        {
            var result = await _http.GetFromJsonAsync<List<ComicAM>>("/api/Comics");
            var comic = result?.Select(x => { x.Thumbnail = $"{_http.BaseAddress}{x.Thumbnail}"; return x; }).ToList();
            return comic;
        }

        public async Task<ComicAM> GetComic(Guid id)
        {
            var result = await _http.GetFromJsonAsync<ComicAM>($"/api/Comics/{id}");
            if (result != null)
            {
                result.Thumbnail = $"{_http.BaseAddress}{result.Thumbnail}";
            }
            return result;
        }

        public async Task<ComicAM> GetComic(string nameAlias)
        {
            var result = await _http.GetFromJsonAsync<ComicAM>($"/api/Comics/detail?nameAlias={nameAlias}");
            if (result != null)
            {
                result.Thumbnail = $"{_http.BaseAddress}{result.Thumbnail}";
            }
            return result;
        }

    }
}

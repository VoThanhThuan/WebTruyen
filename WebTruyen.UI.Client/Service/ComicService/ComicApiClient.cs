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
        private readonly HttpClient _http;
        private readonly ISessionStorageService _sessionStorage;

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

        public async Task<ListComicAM> GetComics(int skip = 0, int take = 20)
        {
            var result = await _http.GetFromJsonAsync<ListComicAM>($"/api/Comics?skip={skip}&take={take}");
            result.Comic = result?.Comic.Select(x => { x.Thumbnail = $"{_http.BaseAddress}{x.Thumbnail}"; return x; }).ToList();
            return result;
        }

        public async Task<ListComicAM> GetComicsInGenre(int idGenre, int skip = 0, int take = 20)
        {
            var result = await _http.GetFromJsonAsync<ListComicAM>($"api/Comics/GetComicsInGenre?idGenre={idGenre}&skip={skip}&take={take}");
            result.Comic = result?.Comic.Select(x => { x.Thumbnail = $"{_http.BaseAddress}{x.Thumbnail}"; return x; }).ToList();
            return result;
        }

        public async Task<ListComicAM> SearchComics(string contentSearch)
        {
            var result = await _http.GetFromJsonAsync<ListComicAM>($"api/Comics/SearchComics?contentSearch={contentSearch}");
            result.Comic = result?.Comic.Select(x => { x.Thumbnail = $"{_http.BaseAddress}{x.Thumbnail}"; return x; }).ToList();
            return result;
        }

        public async Task<ComicAM> GetComic(Guid id)
        {
            var result = await _http.GetFromJsonAsync<ComicAM>($"/api/Comics/{id}");
            if (result != null) {
                result.Thumbnail = $"{_http.BaseAddress}{result.Thumbnail}";
            }
            return result;
        }

        public async Task<ComicAM> GetComic(string nameAlias)
        {
            var result = await _http.GetFromJsonAsync<ComicAM>($"/api/Comics/detail?nameAlias={nameAlias}");
            if (result != null) {
                result.Thumbnail = $"{_http.BaseAddress}{result.Thumbnail}";
            }
            return result;
        }


    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.UI.Admin.RequestClient;
using WebTruyen.UI.Admin.Service.ImageService;

namespace WebTruyen.UI.Admin.Service.ComicService
{
    public class ComicApiClient : IComicApiClient
    {
        private HttpClient _http;
        private readonly IImageService _image;
        ProtectedLocalStorage _localStorageService { get; set; }

        public ComicApiClient(HttpClient http, ProtectedLocalStorage localStorageService, IImageService image)
        {
            _http = http;
            _localStorageService = localStorageService;
            _image = image;


        }

        private async Task GetSession()
        {
            var sessions = (await _localStorageService.GetAsync<string>("Token")).Value;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        }

        public async Task<IEnumerable<ComicAM>> GetComics()
        {
            await GetSession();

            var result = await _http.GetFromJsonAsync<List<ComicAM>>("/api/Comics");
            var comic = result?.Select(x => { x.Thumbnail = $"{_http.BaseAddress}{x.Thumbnail}"; return x; }).ToList();
            return comic;
        }

        public async Task<string> GetImage(string url)
        {
            await GetSession();

            var result = await _http.GetByteArrayAsync(url);
            return _image.ByteToString(result);
        }

        public async Task<ComicAM> GetComic(Guid id)
        {
            await GetSession();

            var result = await _http.GetFromJsonAsync<ComicAM>($"/api/Comics/{id}");
            if (result != null) {
                result.Thumbnail = $"{_http.BaseAddress}{result.Thumbnail}";
            }
            return result;
        }

        public async Task<ComicAM> GetComic(string nameAlias)
        {
            await GetSession();

            var result = await _http.GetFromJsonAsync<ComicAM>($"/api/Comics/detail?nameAlias={nameAlias}");
            if (result != null) {
                result.Thumbnail = $"{_http.BaseAddress}{result.Thumbnail}";
            }
            return result;
        }

        public async Task<int> PutComic(Guid id, ComicRequestClient request, List<GenreAM> genres)
        {
            await GetSession();

            var requestContent = new MultipartFormDataContent();

            if (request.Thumbnail != null) {

                var data = await _image.ImageToByte(request.Thumbnail);

                var bytes = new ByteArrayContent(data);
                bytes.Headers.ContentType = new MediaTypeHeaderValue(request.Thumbnail.ContentType);
                requestContent.Add(bytes, "Thumbnail", request.Thumbnail.Name);
            }
            //Comic
            requestContent.Add(new StringContent(request.Id.ToString()), "id");
            requestContent.Add(new StringContent(request.Name), "Name");
            requestContent.Add(new StringContent(request.NameAlias), "NameAlias");
            requestContent.Add(new StringContent(request.AnotherNameOfComic), "AnotherNameOfComic");
            requestContent.Add(new StringContent(request.Author), "Author");
            requestContent.Add(new StringContent(request.Status.ToString() ?? string.Empty), "Status");
            requestContent.Add(new StringContent(request.Description), "Description");

            //genre
            foreach (var genre in genres) {
                requestContent.Add(new StringContent(genre.Id.ToString()), "genres");
            }

            var response = await _http.PutAsync($"/api/Comics/{id}", requestContent);
            return (int)response.StatusCode;
        }

        public async Task<(HttpStatusCode StatusCode, ComicAM Content)> PostComic(ComicRequestClient request, List<GenreAM> genres)
        {
            await GetSession();

            var requestContent = new MultipartFormDataContent();

            if (request.Thumbnail != null) {
                var data = await _image.ImageToByte(request.Thumbnail);
                var bytes = new ByteArrayContent(data);
                bytes.Headers.ContentType = new MediaTypeHeaderValue(request.Thumbnail.ContentType);

                requestContent.Add(bytes, "Thumbnail", request.Thumbnail.Name);
            }

            requestContent.Add(new StringContent(request.Name), "Name");
            requestContent.Add(new StringContent(request.AnotherNameOfComic), "AnotherNameOfComic");
            requestContent.Add(new StringContent(request.Author), "Author");
            requestContent.Add(new StringContent(request.Status.ToString() ?? string.Empty), "Status");
            requestContent.Add(new StringContent(request.Description), "Description");

            //genre
            foreach (var genre in genres) {
                requestContent.Add(new StringContent(genre.Id.ToString()), "genres");
            }

            var response = await _http.PostAsync($"/api/Comics/", requestContent);
            return (response.StatusCode, await response.Content.ReadFromJsonAsync<ComicAM>());
        }

        public async Task<int> DeleteComic(Guid id)
        {
            await GetSession();

            var response = await _http.DeleteAsync($"/api/Comics/{id}");
            return (int)response.StatusCode;
        }
    }
}

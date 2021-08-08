using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.UI.Admin.RequestClient;

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

        public Task<int> PutComic(Guid id, ComicRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<int> PostComic(ComicRequestClient request)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.Thumbnail != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Thumbnail.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Thumbnail.OpenReadStream().Length);
                }

                var bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.Thumbnail.Name);
            }
            requestContent.Add(new StringContent(request.Name), "Name");
            requestContent.Add(new StringContent(request.NameAlias), "NameAlias");
            requestContent.Add(new StringContent(request.AnotherNameOfComic), "AnotherNameOfComic");
            requestContent.Add(new StringContent(request.Author), "Author");
            requestContent.Add(new StringContent(request.Status.ToString() ?? string.Empty), "Status");
            requestContent.Add(new StringContent(request.Description), "Description");

            var response = await _http.PostAsync($"/api/Comics/", requestContent);
            return (int)response.StatusCode;
        }

        public Task<int> DeleteComic(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

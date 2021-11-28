using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Enums;
using Microsoft.AspNetCore.Components.Forms;
using WebTruyen.UI.Admin.Service.ImageService;

namespace WebTruyen.UI.Admin.Service.ChapterService
{
    public class ChapterService : IChapterService
    {
        private readonly HttpClient _http;
        private readonly IImageService _image;

        ProtectedLocalStorage _localStorageService { get; set; }

        public ChapterService(HttpClient http, ProtectedLocalStorage localStorageService, IImageService image)
        {
            _http = http;
            _image = image;
            _localStorageService = localStorageService;
        }
        private async Task GetSession()
        {
            var sessions = (await _localStorageService.GetAsync<string>("Token")).Value;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        }

        public async Task<int> DeleteChapter(Guid id)
        {
            await GetSession();
            return (int)(await _http.DeleteAsync($"api/Chapters/{id}")).StatusCode;
        }

        public async Task<ChapterAM> GetChapter(Guid id)
        {
            await GetSession();
            return await _http.GetFromJsonAsync<ChapterAM>($"api/Chapters/{id}");
        }

        public Task<IEnumerable<ChapterAM>> GetChapters()
        {
            throw new NotImplementedException();
        }
        public async Task<List<ChapterAM>> GetChaptersInComic(Guid idComic)
        {
            await GetSession();

            return await _http.GetFromJsonAsync<List<ChapterAM>>($"api/Chapters/comic?idComic={idComic}");
        }

        public async Task<ChapterAM> GetLastChapter(Guid idComic)
        {
            await GetSession();

            var result = await _http.GetAsync($"api/Chapters/lastChapter?idComic={idComic}");
            if (result.IsSuccessStatusCode) {
                return await result.Content.ReadFromJsonAsync<ChapterAM>();
            }
            return null;
        }
        public async Task<int> PutChapter(Guid id, ChapterAM chapter, List<IBrowserFile> images)
        {
            await GetSession();

            var requestContent = new MultipartFormDataContent();

            if (images != null) {
                foreach (var image in images) {
                    var data = await _image.ImageToByte(image);
                    var bytes = new ByteArrayContent(data);
                    bytes.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);

                    requestContent.Add(bytes, "pages", image.Name);

                }
            }

            requestContent.Add(new StringContent(chapter.Id.ToString()), "Id");
            requestContent.Add(new StringContent(chapter.Name ?? ""), "Name");
            requestContent.Add(new StringContent(chapter.Ordinal.ToString(CultureInfo.InvariantCulture)), "Ordinal");
            requestContent.Add(new StringContent(chapter.IdComic.ToString()), "IdComic");
            requestContent.Add(new StringContent(chapter.IsLock.ToString()), "IsLock");

            var response = await _http.PutAsync($"/api/Chapters/{id}", requestContent);
            return (int)response.StatusCode;
        }


        public async Task<(HttpStatusCode StatusCode, ChapterAM Content)> PostChapter(ChapterAM chapter, List<IBrowserFile> images)
        {
            await GetSession();

            var requestContent = new MultipartFormDataContent();
            //requestContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            requestContent.Add(new StringContent(chapter.Id.ToString()), "Id");
            requestContent.Add(new StringContent(chapter.Name), "Name");
            requestContent.Add(new StringContent(chapter.Ordinal.ToString(CultureInfo.InvariantCulture)), "Ordinal");
            requestContent.Add(new StringContent(chapter.IdComic.ToString()), "IdComic");
            requestContent.Add(new StringContent(chapter.IsLock.ToString()), "IsLock");


            var response = new HttpResponseMessage();
            var resultPost = new ChapterAM();
            if (images == null) return (response.StatusCode, resultPost);
            if (images.Count > 40) {
                var i = 0;
                var j = 0;

                //Chuyển thành byte
                var dataFirst = await _image.ImageToByte(images[0]);

                var bytes = new ByteArrayContent(dataFirst);
                bytes.Headers.ContentType = new MediaTypeHeaderValue(images[0].ContentType);
                requestContent.Add(bytes, "pages", images[0].Name);

                response = await _http.PostAsync($"/api/Chapters", requestContent);

                if (!response.IsSuccessStatusCode) return (response.StatusCode, resultPost);
                resultPost = await response.Content.ReadFromJsonAsync<ChapterAM>();
                requestContent = new MultipartFormDataContent();

                for (i = 1; i < images.Count; i++) {
                    //Chuyển thành byte
                    var data = await _image.ImageToByte(images[i]);

                    bytes = new ByteArrayContent(data);
                    bytes.Headers.ContentType = new MediaTypeHeaderValue(images[i].ContentType);
                    requestContent.Add(bytes, "pages", images[i].Name);

                    ++j;

                    if (j < 40) continue;
                    var resultContinue = await _http.PostAsync($"/api/Chapters/ContinuePostChapter/{resultPost.Id}", requestContent);
                    j = 0;

                    requestContent = new MultipartFormDataContent();

                    if (!resultContinue.IsSuccessStatusCode)
                        return (resultContinue.StatusCode, null);
                }

            } else {
                foreach (var image in images) {

                    //Chuyển thành byte
                    var data = await _image.ImageToByte(image);


                    var bytes = new ByteArrayContent(data);
                    bytes.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);

                    requestContent.Add(bytes, "pages", image.Name);

                }
                response = await _http.PostAsync($"/api/Chapters", requestContent);
                resultPost = await response.Content.ReadFromJsonAsync<ChapterAM>();
            }

            return (response.StatusCode, resultPost);
        }
    }
}

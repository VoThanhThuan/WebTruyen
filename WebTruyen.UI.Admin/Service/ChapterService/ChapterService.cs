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
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.Library.Enums;

namespace WebTruyen.UI.Admin.Service.ChapterService
{
    public class ChapterService : IChapterService
    {
        private readonly HttpClient _http;
        ProtectedLocalStorage _localStorageService { get; set; }

        public ChapterService(HttpClient http, ProtectedLocalStorage localStorageService)
        {
            _http = http;
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

        public async Task<ChapterVM> GetChapter(Guid id)
        {
            await GetSession();
            return await _http.GetFromJsonAsync<ChapterVM>($"api/Chapters/{id}");
        }

        public Task<IEnumerable<ChapterVM>> GetChapters()
        {
            throw new NotImplementedException();
        }
        public async Task<List<ChapterVM>> GetChaptersInComic(Guid idComic)
        {
            await GetSession();

            return await _http.GetFromJsonAsync<List<ChapterVM>>($"api/Chapters/comic?idComic={idComic}");
        }

        public async Task<ChapterVM> GetLastChapter(Guid idComic)
        {
            await GetSession();

            var result = await _http.GetAsync($"api/Chapters/lastChapter?idComic={idComic}");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ChapterVM>();
            }
            return null;
        }
        public async Task<int> PutChapter(Guid id, ChapterVM chapter, List<(byte[] image, string nameFile)> images)
        {
            await GetSession();

            var requestContent = new MultipartFormDataContent();

            if (images != null)
            {
                foreach (var image in images)
                {
                    var bytes = new ByteArrayContent(image.image);

                    requestContent.Add(bytes, "pages", image.nameFile);

                }
            }

            requestContent.Add(new StringContent(chapter.Id.ToString()), "Id");
            requestContent.Add(new StringContent(chapter.Name??""), "Name");
            requestContent.Add(new StringContent(chapter.Ordinal.ToString(CultureInfo.InvariantCulture)), "Ordinal");
            requestContent.Add(new StringContent(chapter.IdComic.ToString()), "IdComic");
            requestContent.Add(new StringContent(chapter.IsLock.ToString()), "IsLock");

            var response = await _http.PutAsync($"/api/Chapters/{id}", requestContent);
            return (int)response.StatusCode;
        }


        public async Task<(HttpStatusCode StatusCode, ChapterVM Content)> PostChapter(ChapterVM chapter, List<(byte[] image, string nameFile)> images)
        {
            await GetSession();

            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(chapter.Id.ToString()), "Id");
            requestContent.Add(new StringContent(chapter.Name), "Name");
            requestContent.Add(new StringContent(chapter.Ordinal.ToString(CultureInfo.InvariantCulture)), "Ordinal");
            requestContent.Add(new StringContent(chapter.IdComic.ToString()), "IdComic");
            requestContent.Add(new StringContent(chapter.IsLock.ToString()), "IsLock");


            var response = new HttpResponseMessage();
            var resultPost = new ChapterVM();
            if (images != null)
            {
                if (images.Count > 40)
                {
                    var i = 0;
                    var j = 0;

                    var bytes = new ByteArrayContent(images[i].image);
                    requestContent.Add(bytes, "pages", images[i].nameFile);

                    response = await _http.PostAsync($"/api/Chapters", requestContent);

                    if (!response.IsSuccessStatusCode) return (response.StatusCode, resultPost);
                    requestContent = new MultipartFormDataContent();
                    resultPost = await response.Content.ReadFromJsonAsync<ChapterVM>();
                    for (i = 1; i < images.Count; i++)
                    {
                        bytes = new ByteArrayContent(images[i].image);
                        requestContent.Add(bytes, "pages", images[i].nameFile);

                        ++j;

                        if (j < 40) continue;
                        var resultContinue = await _http.PostAsync($"/api/Chapters/ContinuePostChapter/{resultPost.Id}", requestContent);
                        j = 0;

                        requestContent = new MultipartFormDataContent();

                        if (!resultContinue.IsSuccessStatusCode)
                            return (resultContinue.StatusCode, null);
                    }

                }
                else
                {
                    foreach (var image in images)
                    {
                        var bytes = new ByteArrayContent(image.image);

                        requestContent.Add(bytes, "pages", image.nameFile);

                    }
                    response = await _http.PostAsync($"/api/ContinuePostChapter", requestContent);
                    resultPost = await response.Content.ReadFromJsonAsync<ChapterVM>();
                }

            }

            return (response.StatusCode, resultPost);
        }
    }
}

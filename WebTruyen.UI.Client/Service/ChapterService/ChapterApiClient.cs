using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.Library.Enums;

namespace WebTruyen.UI.Client.Service.ChapterService
{
    public class ChapterApiClient : IChapterApiClient
    {

        private HttpClient _http;
        ISessionStorageService _sessionStorage { get; set; }

        public ChapterApiClient(HttpClient http, ISessionStorageService sessionStorage)
        {
            _http = http;
            _sessionStorage = sessionStorage;

        }
        private async Task GetSession()
        {
            var sessions = (await _sessionStorage.GetItemAsync<string>("Token"));
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        }

        public async Task<List<ChapterVM>> GetChapters()
        {
            return await _http.GetFromJsonAsync<List<ChapterVM>>("/api/Chapters");
        }

        public async Task<ChapterVM> GetChapter(Guid id)
        {
            var chapter = await _http.GetFromJsonAsync<ChapterVM>($"/api/Chapters/{id}");
            return chapter;
        }

        public async Task<List<ChapterVM>> GetChaptersInComic(Guid idComic)
        {
            // api/Chapters/comic?idComic=
            var chapter = await _http.GetFromJsonAsync<List<ChapterVM>>($"/api/Chapters/comic?idComic={idComic}");

            return chapter;
        }


        public async Task<ChapterVM> GetLastChapter(Guid idComic)
        {
            //api/Chapters/
            var chapter = await _http.GetFromJsonAsync<ChapterVM>($"/api/Chapters");

            return chapter;
        }
        public async Task<List<ChapterVM>> GetNewChapters(Guid idComic, int amount)
        {
            //api/Chapters?idComic=xxxxx&amount=3
            var chapter = await _http.GetFromJsonAsync<List<ChapterVM>>($"/api/Chapters?idComic={idComic}&amount={amount}");

            return chapter;

        }
    }
}

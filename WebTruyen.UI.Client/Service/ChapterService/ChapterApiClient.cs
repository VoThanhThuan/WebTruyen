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
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Enums;

namespace WebTruyen.UI.Client.Service.ChapterService
{
    public class ChapterApiClient : IChapterApiClient
    {

        private readonly HttpClient _http;
        private readonly ISessionStorageService _sessionStorage;

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

        public async Task<List<ChapterAM>> GetChapters()
        {
            return await _http.GetFromJsonAsync<List<ChapterAM>>("/api/Chapters");
        }

        public async Task<ChapterAM> GetChapter(Guid id)
        {
            var chapter = await _http.GetFromJsonAsync<ChapterAM>($"/api/Chapters/{id}");
            return chapter;
        }

        public async Task<ChapterAM> GetChapterOrder(string comicAliasName, string ordinal)
        {
            var chapter = await _http.GetFromJsonAsync<ChapterAM>($"/api/Chapters/GetChapterOrder/{comicAliasName}/{ordinal}");
            return chapter;
        }

        public async Task<List<ChapterAM>> GetChaptersInComic(Guid idComic, int skip = 0, int take = 40)
        {
            // api/Chapters/comic?idComic=
            var chapter = await _http.GetFromJsonAsync<List<ChapterAM>>($"/api/Chapters/comic?idComic={idComic}&skip={skip}&take={take}");

            return chapter;
        }


        public async Task<ChapterAM> GetLastChapter(Guid idComic)
        {
            //api/Chapters/
            var chapter = await _http.GetFromJsonAsync<ChapterAM>($"/api/Chapters");

            return chapter;
        }
        public async Task<List<ChapterAM>> GetNewChapters(Guid idComic, int amount)
        {
            //api/Chapters?idComic=xxxxx&amount=3
            var chapter = await _http.GetFromJsonAsync<List<ChapterAM>>($"/api/Chapters?idComic={idComic}&amount={amount}");

            return chapter;

        }
    }
}

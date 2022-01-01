using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.BookmarkService
{
    public class BookmarkApiClient : IBookmarkApiClient
    {
        private readonly HttpClient _http;
        private readonly ISessionStorageService _sessionStorage;

        public BookmarkApiClient(HttpClient http, ISessionStorageService sessionStorage)
        {
            _http = http;
            _sessionStorage = sessionStorage;

        }

        private async Task GetSession()
        {
            var sessions = (await _sessionStorage.GetItemAsync<string>("Token"));
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        }

        public async Task<int> AddBookmark(BookmarkAM bookmark)
        {
            var response = await _http.PostAsJsonAsync("api/Bookmarks", bookmark);
            return (int)response.StatusCode;
        }

        public async Task<BookmarkAM> GetBookmarkOfAccount(Guid idComic, Guid idUser)
        {
            var response = await _http.GetAsync($"api/Bookmarks/GetBookmarkOfAccount?idComic={idComic}&idUser={idUser}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<BookmarkAM>();
            }

            return null;
        }

        public async Task<int> RemoveBookmark(Guid idComic, Guid idUser)
        {
            var response = await _http.DeleteAsync($"api/Bookmarks?idUser={idUser}&idComic={idComic}");
            return (int)response.StatusCode;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _http;
        private readonly ISessionStorageService _sessionStorage;

        public CommentService(HttpClient http, ISessionStorageService sessionStorage)
        {
            _http = http;
            _sessionStorage = sessionStorage;

        }
        private async Task GetSession()
        {
            var sessions = (await _sessionStorage.GetItemAsync<string>("Token"));
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        }
        public async Task<IEnumerable<CommentVM>> GetComments()
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<List<CommentVM>>($"/api/Comments");
            return result;
        }

        public Task<CommentAM> GetComment(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CommentVM>> GetCommentInComic(Guid idComic, int skip = 0, int take = 10)
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<List<CommentVM>>($"/api/Comments/GetCommentInComic?idComic={idComic}&skip={skip}&take={take}");
            return result;
        }

        public async Task<List<CommentVM>> GetCommentInChapter(Guid idChapter, int skip = 0, int take = 10)
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<List<CommentVM>>($"/api/Comments/GetCommentInChapter?idChapter={idChapter}&skip={skip}&take={take}");
            return result;
        }

        public async Task<List<CommentVM>> GetCommentChildInComic(Guid idComic, Guid idCommentReply, int skip = 0, int take = 10)
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<List<CommentVM>>($"/api/Comments/GetCommentChildInComic?idComic={idComic}&idCommentReply={idCommentReply}&skip={skip}&take={take}");
            return result;
        }

        public async Task<List<CommentVM>> GetCommentChildInChapter(Guid idChapter, Guid idCommentReply, int skip = 0, int take = 10)
        {
            await GetSession();
            var result = await _http.GetFromJsonAsync<List<CommentVM>>($"/api/Comments/GetCommentChildInChapter?idChapter={idChapter}&idCommentReply={idCommentReply}&skip={skip}&take={take}");
            return result;
        }

        public Task<bool> PutComment(Guid id, CommentRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool isSuccess, string value)> PostComment(CommentRequest request)
        {
            await GetSession();
            var json = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(@"/api/Comments", httpContent);
            if (response.IsSuccessStatusCode) {
                return (true, await response.Content.ReadAsStringAsync());
            }

            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return (false, await response.Content.ReadAsStringAsync());
        }

        public Task<bool> DeleteComment(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

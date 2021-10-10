using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.PageService
{
    public class PageService : IPageService
    {

        private readonly HttpClient _http;

        public PageService(HttpClient http)
        {
            _http = http;
        }

        public Task<List<PageVM>> GetPages()
        {
            throw new NotImplementedException();
        }

        public Task<PageVM> GetPage(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PageVM>> GetPagesInChapter(Guid idChapter)
        {
            var result = await _http.GetFromJsonAsync<List<PageVM>>($"api/Pages/chapter?idChapter={idChapter}");
            var chapters = result?.Select(x => { x.Image = $"{_http.BaseAddress}{x.Image}"; return x; }).ToList();

            return chapters;
        }

        public async IAsyncEnumerable<PageVM> GetPagesInChapterYeild(Guid idChapter)
        {
            var result = await _http.GetFromJsonAsync<List<PageVM>>($"api/Pages/chapter?idChapter={idChapter}");

            foreach (var page in result)
            {
                page.Image = $"{_http.BaseAddress}{page.Image}";
                yield return page;
            }
        }

    }
}

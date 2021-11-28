using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Admin.Service.PageService
{
    public class PageService : IPageService
    {

        private readonly HttpClient _http;

        public PageService(HttpClient http)
        {
            _http = http;
        }

        public Task<List<PageAM>> GetPages()
        {
            //throw new NotImplementedException();
            return null;

        }

        public Task<PageAM> GetPage(Guid id)
        {
            //throw new NotImplementedException();

            return null;
        }

        public async Task<List<PageAM>> GetPagesInChapter(Guid idChapter)
        {
            var result = await _http.GetFromJsonAsync<List<PageAM>>($"api/Pages/chapter?idChapter={idChapter}");
            var chapters = result?.Select(x => { x.Image = $"{_http.BaseAddress}{x.Image}"; return x; }).ToList();

            return chapters;
        }

        public async IAsyncEnumerable<PageAM> GetPagesInChapterYeild(Guid idChapter)
        {
            var result = await _http.GetFromJsonAsync<List<PageAM>>($"api/Pages/chapter?idChapter={idChapter}");

            foreach (var page in result) {
                page.Image = $"{_http.BaseAddress}{page.Image}";
                yield return page;
            }
        }

        public Task<bool> PutPage(Guid id, PageRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostPages(Guid idChapter, List<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<PageAM> PostPage(Guid idChapter, PageRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePage(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

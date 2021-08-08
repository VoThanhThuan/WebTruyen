using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Admin.Service.PageService
{
    public class PageService : IPageService
    {

        private readonly HttpClient _http;

        public PageService(HttpClient http)
        {
            _http = http;
        }

        public Task<IEnumerable<PageVM>> GetPages()
        {
            throw new NotImplementedException();
        }

        public Task<PageVM> GetPage(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PageVM>> GetPagesWithChapter(Guid idChapter)
        {
            return _http.GetFromJsonAsync<IEnumerable<PageVM>>($"api/Pages/chapter?idChapter={idChapter}");
        }

        public Task<bool> PutPage(Guid id, PageRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostPages(Guid idChapter, List<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<PageVM> PostPage(Guid idChapter, PageRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePage(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Page
{
    public interface IPageService
    {
        public Task<IEnumerable<PageVM>> GetPages();
        public Task<PageVM> GetPage(Guid id);
        public Task<IEnumerable<PageVM>> GetPagesInChapter(Guid idChapter);

        public Task<bool> PutPage(Guid id, PageRequest request);
        public Task<bool> PostPages(Guid idChapter, List<string> request);
        public Task<bool> PostPages(Guid idChapter, List<IFormFile> request);
        public Task<PageVM> PostPage(Guid idChapter, PageRequest request);
        public Task<bool> DeletePage(Guid id);

    }
}

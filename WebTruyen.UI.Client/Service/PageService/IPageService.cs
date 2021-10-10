using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.PageService
{
    public interface IPageService
    {
        public Task<List<PageVM>> GetPages();
        public Task<PageVM> GetPage(Guid id);
        public Task<List<PageVM>> GetPagesInChapter(Guid idChapter);
        public IAsyncEnumerable<PageVM> GetPagesInChapterYeild(Guid idChapter);

    }
}

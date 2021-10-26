using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.PageService
{
    public interface IPageService
    {
        public Task<List<PageAM>> GetPages();
        public Task<PageAM> GetPage(Guid id);
        public Task<List<PageAM>> GetPagesInChapter(Guid idChapter);
        public IAsyncEnumerable<PageAM> GetPagesInChapterYeild(Guid idChapter);

    }
}

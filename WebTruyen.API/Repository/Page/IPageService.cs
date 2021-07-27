using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Page
{
    public interface IPageService
    {
        public Task<IEnumerable<PageVM>> GetPages();
        public Task<PageVM> GetPage(Guid id);
        public Task<bool> PutPage(Guid id, PageVM request);
        public Task<bool> PostPage(PageVM request);
        public Task<bool> DeletePage(Guid id);

    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.Page
{
    public interface IPageService
    {
        //Get
        public Task<IEnumerable<PageAM>> GetPages();
        public Task<PageAM> GetPage(Guid id);
        public Task<IEnumerable<PageAM>> GetPagesInChapter(Guid idChapter);
        //Put
        public Task<int> PutPage(Guid id, PageRequest request);
        public Task<int> PutPages(Guid idChapter, List<IFormFile> images, bool isLock);
        public Task MoveUrlPages(Guid idChapter, string oldChapter, string newChapter);

        //Post
        public Task<int> PostPages(Guid idChapter, List<string> request);
        public Task<int> PostPages(Guid idChapter, List<IFormFile> request);
        public Task<PageAM> PostPage(Guid idChapter, PageRequest request);
        //Delete
        public Task<int> DeletePage(Guid id);

    }
}

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
        //Get
        public Task<IEnumerable<PageVM>> GetPages();
        public Task<PageVM> GetPage(Guid id);
        public Task<IEnumerable<PageVM>> GetPagesInChapter(Guid idChapter);
        //Put
        public Task<int> PutPage(Guid id, PageRequest request);
        public Task<int> PutPages(Guid idChapter, List<IFormFile> images);
        public Task MoveUrlPages(Guid idChapter, string oldChapter, string newChapter);

        //Post
        public Task<int> PostPages(Guid idChapter, List<string> request);
        public Task<int> PostPages(Guid idChapter, List<IFormFile> request);
        public Task<PageVM> PostPage(Guid idChapter, PageRequest request);
        //Delete
        public Task<int> DeletePage(Guid id);

    }
}

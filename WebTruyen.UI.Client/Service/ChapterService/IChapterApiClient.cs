using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.ChapterService
{
    public interface IChapterApiClient
    {
        public Task<List<ChapterVM>> GetChapters();
        public Task<ChapterVM> GetChapter(Guid id);
        public Task<ChapterVM> GetLastChapter(Guid idComic);
        public Task<List<ChapterVM>> GetNewChapters(Guid idComic, int amount);

        public Task<List<ChapterVM>> GetChaptersInComic(Guid idComic);


    }
}

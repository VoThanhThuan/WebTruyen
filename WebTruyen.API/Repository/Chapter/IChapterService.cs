using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Chapter
{
    public interface IChapterService
    {
        public Task<IEnumerable<ChapterVM>> GetChapters();
        public Task<ChapterVM> GetChapter(Guid id);
        public Task<ChapterVM> GetLastChapter(Guid idComic);
        public Task<List<ChapterVM>> GetChaptersInComic(Guid idComic);
        public Task<int> PutChapter(Guid id, ChapterRequest chapter);
        public Task<ChapterVM> PostChapter(ChapterRequest chapter);
        public Task<int> DeleteChapter(Guid id);

    }
}

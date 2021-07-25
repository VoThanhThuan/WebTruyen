using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Chapter
{
    public interface IChapterService
    {
        public Task<IEnumerable<ChapterVM>> GetChapters();
        public Task<ChapterVM> GetChapter(Guid id);
        public Task<bool> PutChapter(Guid id, ChapterVM chapter);
        public Task<bool> PostChapter(ChapterVM chapter);
        public Task<bool> DeleteChapter(Guid id);

    }
}

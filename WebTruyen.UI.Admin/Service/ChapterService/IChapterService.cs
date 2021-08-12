using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Admin.Service.ChapterService
{
    public interface IChapterService
    {
        public Task<IEnumerable<ChapterVM>> GetChapters();
        public Task<ChapterVM> GetChapter(Guid id);
        public Task<List<ChapterVM>> GetChaptersInComic(Guid idComic);
        public Task<int> PutChapter(Guid id, ChapterVM chapter);
        public Task<int> PostChapter(ChapterVM chapter, IEnumerable<(byte[] image, string nameFile)> images);
        public Task<int> DeleteChapter(Guid id);

    }
}

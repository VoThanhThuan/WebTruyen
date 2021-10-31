using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Admin.Service.ChapterService
{
    public interface IChapterService
    {
        public Task<IEnumerable<ChapterAM>> GetChapters();
        public Task<ChapterAM> GetChapter(Guid id);
        public Task<ChapterAM> GetLastChapter(Guid idComic);
        public Task<List<ChapterAM>> GetChaptersInComic(Guid idComic);
        public Task<int> PutChapter(Guid id, ChapterAM chapter, List<(byte[] image, string nameFile, string contentType)> images);
        public Task<(HttpStatusCode StatusCode, ChapterAM Content)> PostChapter(ChapterAM chapter, List<(byte[] image, string nameFile, string contentType)> images);
        public Task<int> DeleteChapter(Guid id);

    }
}

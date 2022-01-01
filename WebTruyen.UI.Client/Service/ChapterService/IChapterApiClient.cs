using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.ChapterService
{
    public interface IChapterApiClient
    {
        public Task<List<ChapterAM>> GetChapters();
        public Task<ChapterAM> GetChapter(Guid id);
        public Task<ChapterAM> GetChapterOrder(string comicAliasName, string ordinal);

        public Task<ChapterAM> GetLastChapter(Guid idComic);
        public Task<List<ChapterAM>> GetNewChapters(Guid idComic, int amount);

        public Task<List<ChapterAM>> GetChaptersInComic(Guid idComic, int skip = 0, int take = 40);


    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.ChapterDI
{
    public interface IChapterService
    {
        public Task<IEnumerable<ChapterAM>> GetChapters();
        public Task<ChapterAM> GetChapter(Guid id);
        public Task<ChapterAM> GetChapterOrder(string comicAliasName, float ordinal);
        public Task<ChapterAM> GetLastChapter(Guid idComic);
        public Task<List<ChapterAM>> GetNewChapters(Guid idComic, int amount);

        public Task<List<ChapterAM>> GetChaptersInComic(Guid idComic, int skip = 0, int take = 40);

        public Task<int> PutChapter(Guid id, ChapterRequest chapter);
        public Task<ChapterAM> PostChapter(ChapterRequest chapter);

        public Task<int> DeleteChapter(Guid id);
        public Task<int> DeleteChapterInComic(Guid idComic);

    }
}

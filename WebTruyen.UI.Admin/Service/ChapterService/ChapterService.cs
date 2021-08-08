using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.Library.Enums;

namespace WebTruyen.UI.Admin.Service.ChapterService
{
    public class ChapterService : IChapterService
    {
        private readonly HttpClient _http;

        public ChapterService(HttpClient http)
        {
            _http = http;
        }

        public Task<bool> DeleteChapter(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ChapterVM> GetChapter(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChapterVM>> GetChapters()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<ChapterVM>> GetChaptersInComic(Guid idComic)
        {
            return _http.GetFromJsonAsync<IEnumerable<ChapterVM>>($"api/Chapters/comic?idComic={idComic}");
        }

        public Task<ChapterVM> PostChapter(ChapterRequest chapter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutChapter(Guid id, ChapterRequest chapter)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
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

        public Task<int> DeleteChapter(Guid id)
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
        public Task<List<ChapterVM>> GetChaptersInComic(Guid idComic)
        {
            return _http.GetFromJsonAsync<List<ChapterVM>>($"api/Chapters/comic?idComic={idComic}");
        }

        public async Task<int> PostChapter(ChapterVM chapter, IEnumerable<(byte[] image, string nameFile)> images)
        {
            var requestContent = new MultipartFormDataContent();

            if (images != null)
            {
                foreach (var image in images)
                {
                    var bytes = new ByteArrayContent(image.image);

                    requestContent.Add(bytes, "pages", image.nameFile);

                }
            }

            requestContent.Add(new StringContent(chapter.Id.ToString()), "Id");
            requestContent.Add(new StringContent(chapter.Name), "Name");
            requestContent.Add(new StringContent(chapter.Ordinal.ToString(CultureInfo.InvariantCulture)), "Ordinal");
            requestContent.Add(new StringContent(chapter.IdComic.ToString()), "IdComic");

            var response = await _http.PostAsync($"/api/Chapters", requestContent);
            return (int)response.StatusCode;
        }

        public Task<int> PutChapter(Guid id, ChapterVM chapter)
        {
            throw new NotImplementedException();
        }
    }
}

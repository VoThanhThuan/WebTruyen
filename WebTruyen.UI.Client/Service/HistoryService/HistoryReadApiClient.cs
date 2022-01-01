using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.HistoryService
{
    public class HistoryReadApiClient : IHistoryReadApiClient
    {
        private readonly HttpClient _http;
        private readonly ISessionStorageService _sessionStorage;

        public HistoryReadApiClient(HttpClient http, ISessionStorageService sessionStorage)
        {
            _http = http;
            _sessionStorage = sessionStorage;

        }
        public async Task<int> AddHistory(HistoryReadAM historyRead)
        {
            var response = await _http.PostAsJsonAsync("api/HistoryReads", historyRead);
            return (int)response.StatusCode;
        }

        public async Task<List<HistoryReadVM>> GetHistory(Guid idUser, int skip, int take)
        {
            var response = await _http.GetAsync($"api/HistoryReads/GetHistoryReadsOfAccount?idUser={idUser}&skip={skip}&take={take}");
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var result = await response.Content.ReadFromJsonAsync<List<HistoryReadVM>>();
            //Console.WriteLine($"HistoryReadApiClient >> GetHistory >> result: {JsonSerializer.Serialize(result)}");
            var histories = result?.Select(x => { x.Comic.Thumbnail = $"{_http.BaseAddress}{x.Comic.Thumbnail}"; return x; }).ToList();
            return histories;

        }
    }
}

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.AnnouncementService
{
    public class AnnouncementApiClient : IAnnouncementApiClient
    {
        private readonly HttpClient _http;
        public AnnouncementApiClient(HttpClient http)
        {
             _http = http;
        }

        public async Task<List<AnnouncementAM>> GetAnnouncements()
        {
            var response = await _http.GetAsync($"/api/Announcements/GetAnnouncementsOfUser");
            if(response.StatusCode == HttpStatusCode.OK) {
                 return await response.Content.ReadFromJsonAsync<List<AnnouncementAM>>();
            }
            return new List<AnnouncementAM>();
            
        }

        public async Task<ListChapterAM> GetChapterOfAnnouncements()
        {
            var response = await _http.GetAsync($"/api/Announcements/GetChapterOfAnnouncements");
            if (response.StatusCode == HttpStatusCode.OK) {
                return await response.Content.ReadFromJsonAsync<ListChapterAM>();
            }
            return new ListChapterAM();
        }
    }
}

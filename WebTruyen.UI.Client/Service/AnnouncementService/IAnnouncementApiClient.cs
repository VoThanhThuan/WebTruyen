using System.Collections.Generic;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.AnnouncementService
{
    public interface IAnnouncementApiClient
    {
        public Task<List<AnnouncementAM>> GetAnnouncements();
        public Task<ListChapterAM> GetChapterOfAnnouncements();
    }
}

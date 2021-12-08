using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.Announcement
{
    public interface IAnnouncementService
    {
        public Task<IEnumerable<AnnouncementAM>> GetAnnouncements();
        public Task<AnnouncementAM> GetAnnouncement(Guid id);
        public Task<bool> PutAnnouncement(Guid id, AnnouncementAM request);
        public Task<bool> PostAnnouncement(AnnouncementAM request);
        public Task<bool> DeleteAnnouncement(Guid id);

    }
}

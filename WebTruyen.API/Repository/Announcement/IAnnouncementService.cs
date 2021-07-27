using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Announcement
{
    public interface IAnnouncementService
    {
        public Task<IEnumerable<AnnouncementVM>> GetAnnouncements();
        public Task<AnnouncementVM> GetAnnouncement(Guid id);
        public Task<bool> PutAnnouncement(Guid id, AnnouncementVM request);
        public Task<bool> PostAnnouncement(AnnouncementVM request);
        public Task<bool> DeleteAnnouncement(Guid id);

    }
}

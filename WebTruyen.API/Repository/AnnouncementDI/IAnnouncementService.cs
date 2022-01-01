using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.AnnouncementDI
{
    public interface IAnnouncementService
    {
        public Task<IEnumerable<AnnouncementAM>> GetAnnouncements();
        public Task<AnnouncementAM> GetAnnouncement(Guid id);
        public Task<List<AnnouncementAM>> GetAnnouncementsOfUser(Guid idUser);
        public Task<ListChapterAM> GetChapterOfAnnouncements(Guid idUser, int skip = 0, int take = 10);
        public Task<bool> PutAnnouncement(Guid id, AnnouncementAM request);
        public Task<bool> PostAnnouncement(AnnouncementAM request);
        public Task<bool> DeleteAnnouncement(Guid idUser, Guid idChapter);

    }
}

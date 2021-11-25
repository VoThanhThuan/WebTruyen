using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.ApiModel
{
    public class AnnouncementAM
    {
        public Announcement ToNewComicAnnouncement()
        {
            return new Announcement()
            {
                IdUser = IdUser,
                IsRead = IsRead,
                IdChapter = IdChapter
            };
        }
        public bool IsRead { get; set; } = false;
        public Guid IdUser { get; set; }
        public Guid IdChapter { get; set; }

    }
}

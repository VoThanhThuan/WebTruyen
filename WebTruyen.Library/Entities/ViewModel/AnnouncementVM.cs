using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.ViewModel
{
    public class AnnouncementVM
    {
        public Announcement ToNewComicAnnouncement()
        {
            return new Announcement()
            {
                Id = Id,
                IdUser = IdUser,
                IdChapter = IdChapter
            };
        }
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdChapter { get; set; }

    }
}

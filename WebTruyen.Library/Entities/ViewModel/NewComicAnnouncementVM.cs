using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.ViewModel
{
    public class NewComicAnnouncementVM
    {
        public NewComicAnnouncement ToNewComicAnnouncement()
        {
            return new NewComicAnnouncement()
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.Library.Entities
{
    public class Announcement
    {
        public AnnouncementVM ToViewModel()
        {
            return new AnnouncementVM()
            {
                IsRead = IsRead,
                IdUser = IdUser,
                IdChapter = IdChapter
            };
        }

        public bool IsRead { get; set; } = false;

        public Guid IdUser { get; set; }
        public Guid IdChapter { get; set; }
        public virtual User User { get; set; }
        public virtual Chapter Chapter { get; set; }
    }
}

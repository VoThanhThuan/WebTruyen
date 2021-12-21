using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities
{
    public class Announcement
    {
        public AnnouncementAM ToApiModel()
        {
            return new AnnouncementAM()
            {
                IsRead = IsRead,
                IdUser = IdUser,
                IdChapter = IdChapter,
                TimeCreate = TimeCreate
            };
        }

        public bool IsRead { get; set; } = false;
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        public Guid IdUser { get; set; }
        public Guid IdChapter { get; set; }
        public virtual User User { get; set; }
        public virtual Chapter Chapter { get; set; }
    }
}

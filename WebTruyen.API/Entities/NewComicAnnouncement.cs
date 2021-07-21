using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities
{
    public class NewComicAnnouncement
    {
        public Guid Id { get; set; }
        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdChapter { get; set; }
        public Comic Comic { get; set; }
        public User User { get; set; }
        public Chapter Chapter { get; set; }
    }
}

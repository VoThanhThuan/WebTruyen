using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.Library.Entities
{
    public class Chapter
    {
        public ChapterVM ToViewModel()
        {
            return new ChapterVM()
            {
                Id = Id,
                DateTimeUp = DateTimeUp,
                Views = Views
            };
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime DateTimeUp { get; set; } = DateTime.Now;
        public int Views { get; set; } = 0;

        //Khóa ngoại
        public Guid IdComic { get; set; }
        public virtual Comic Comic { get; set; }
        public virtual List<Page> Pages { get; set; }
        public virtual List<Report> Reports { get; set; }
        public virtual List<Announcement> NewComicAnnouncements { get; set; }

    }
}

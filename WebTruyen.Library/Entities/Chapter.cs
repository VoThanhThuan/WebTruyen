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
                Ordinal = Ordinal,
                Name = Name,
                DateTimeUp = DateTimeUp,
                Views = Views,
                IdComic = IdComic
            };
        }

        [Key]
        public Guid Id { get; set; }
        public float Ordinal { get; set; } = 1.0f;
        public string Name { get; set; } = "";
        public DateTime DateTimeUp { get; set; } = DateTime.Now;
        public int Views { get; set; } = 0;
        public bool IsLock { get; set; } = false;
        public Guid IdComic { get; set; }

        //Khóa ngoại
        public virtual Comic Comic { get; set; }
        public virtual List<Page> Pages { get; set; }
        public virtual List<Report> Reports { get; set; }
        public virtual List<Announcement> NewComicAnnouncements { get; set; }

    }
}

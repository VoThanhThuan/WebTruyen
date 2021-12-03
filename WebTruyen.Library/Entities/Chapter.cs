using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities
{
    public class Chapter
    {
        public ChapterAM ToApiModel()
        {
            var chapter = new ChapterAM() {
                Id = Id,
                Ordinal = Ordinal,
                Name = Name,
                DateTimeUp = DateTimeUp,
                Views = Views,
                IsLock = IsLock,
                IdComic = IdComic
            };
            if (Comic != null)
                chapter.ComicAM = Comic.ToApiModel();
            return chapter;
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
        public virtual List<Comment> Comments { get; set; }

    }
}

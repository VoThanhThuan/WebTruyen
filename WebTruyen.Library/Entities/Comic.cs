using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Enums;

namespace WebTruyen.Library.Entities
{
    public class Comic
    {
        
        public ComicAM ToApiModel()
        {
            return new ComicAM()
            {
                Id = Id,
                Name = Name,
                NameAlias = NameAlias,
                AnotherNameOfComic = AnotherNameOfComic,
                Author = Author,
                Status = Status,
                Views = Views,
                Description = Description,
                Thumbnail = Thumbnail,
                DateUpdate = DateUpdate
            };
        }

        public ComicAM ToApiModel(List<GenreAM> genres)
        {
            var view = ToApiModel();
            view.Genres = genres;
            return view;
        }

        [Key]
        public Guid Id { get; set; }

        [Required] public string Name { get; set; } = "";

        public string NameAlias { get; set; } = "";
        public string AnotherNameOfComic { get; set; } = "";
        public string Author { get; set; } = "";
        public Status? Status { get; set; } = 0;
        public int? Views { get; set; } = 0;
        public string Description { get; set; } = "";
        public string Thumbnail { get; set; } = "";
        public DateTime DateUpdate { get; set; } = DateTime.Now;

        //Khoa ngoai
        public virtual List<ComicInGenre> ComicInGenres { get; set; }
        public virtual List<Chapter> Chapters { get; set; }
        public virtual List<TranslationOfUser> TranslationOfUsers { get; set; }
        public virtual List<Bookmark> Bookmarks { get; set; }
        public virtual List<Announcement> NewComicAnnouncements { get; set; }
        public virtual List<HistoryRead> HistoryReads { get; set; }
        public virtual List<Comment> Comments { get; set; }

    }
}

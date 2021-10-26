using System;

namespace WebTruyen.Library.Entities.ApiModel
{
    public class ComicInGenreAM
    {
        public ComicInGenre ToComicInGenre()
        {
            return new ComicInGenre()
            {
                IdComic = IdComic,
                IdGenre = IdGenre
            };
        }
        public Guid IdComic { get; set; }
        public int IdGenre { get; set; }

    }
}

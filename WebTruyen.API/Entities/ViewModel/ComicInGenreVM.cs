using System;

namespace WebTruyen.API.Entities.ViewModel
{
    public class ComicInGenreVM
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.API.Entities.ViewModel;

namespace WebTruyen.API.Entities
{
    public class ComicInGenre
    {
        public ComicInGenreVM ToViewModel()
        {
            return new ComicInGenreVM()
            {
                IdComic = IdComic,
                IdGenre = IdGenre
            };
        }

        public Guid IdComic { get; set; }
        public int IdGenre { get; set; }
        public virtual Comic Comic { get; set; }
        public virtual Genre Genre { get; set; }  
    }
}

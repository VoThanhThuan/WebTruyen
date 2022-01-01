using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities
{
    public class ComicInGenre
    {
        public ComicInGenreAM ToApiModel()
        {
            return new ComicInGenreAM()
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

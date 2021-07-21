using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities
{
    public class ComicInGenre
    {
        public Guid IdComic { get; set; }
        public int IdGenre { get; set; }
        public Comic Comic { get; set; }
        public Genre Genre { get; set; }  
    }
}

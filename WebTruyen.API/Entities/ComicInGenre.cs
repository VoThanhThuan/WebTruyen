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
        public virtual Comic Comic { get; set; }
        public virtual Genre Genre { get; set; }  
    }
}

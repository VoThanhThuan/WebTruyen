using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities
{
    public class Bookmark
    {
        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }

        public virtual Comic Comic { get; set; }
        public virtual User User { get; set; }
    }
}

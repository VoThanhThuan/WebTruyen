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
        public Comic Comic { get; set; }
        public User User { get; set; }
    }
}

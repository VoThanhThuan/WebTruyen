using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.ApiModel
{
    public class ListComicAM
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;
        public int Total { get; set; }

        public List<ComicAM> Comic { get; set; }
    }
}

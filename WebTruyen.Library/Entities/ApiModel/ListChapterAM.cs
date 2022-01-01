using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.ApiModel
{
    public class ListChapterAM
    {
        public int Total { get; set; } = 0;
        public int skip { get; set; } = 0;
        public int Take { get; set; } = 0;
        public List<ChapterAM> Chapters { get; set; }= new ();
    }
}

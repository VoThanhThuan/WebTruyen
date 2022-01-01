using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities.ViewModel
{
    public class HistoryReadVM
    {
        public Guid IdLastReadChapter { get; set; }
        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }

        public ComicAM Comic { get; set; } = new ComicAM();
        public ChapterAM Chapter { get; set; } = new ChapterAM();

        public DateTime TimeCreate { get; set; } = DateTime.Now;
    }
}

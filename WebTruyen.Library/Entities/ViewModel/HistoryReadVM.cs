using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.ViewModel
{
    public class HistoryReadVM
    {
        public HistoryRead ToHistoryRead()
        {
            return new HistoryRead()
            {
                IdUser = IdUser,
                IdComic = IdComic,
                LastReadChapter = LastReadChapter
            };
        }
        public Guid LastReadChapter { get; set; }

        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }
    }
}

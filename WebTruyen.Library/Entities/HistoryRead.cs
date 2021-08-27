using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.Library.Entities
{
    public class HistoryRead
    {
        public HistoryReadVM ToViewModel()
        {
            return new HistoryReadVM()
            {
                IdUser = IdUser,
                IdComic = IdComic,
                LastReadChapter = LastReadChapter
            };
        }

        public Guid LastReadChapter { get; set; }

        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }
        public virtual Comic Comic { get; set; }
        public virtual User User { get; set; }
    }
}

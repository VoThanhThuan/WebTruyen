using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities
{
    public class HistoryRead
    {
        public HistoryReadAM ToApiModel()
        {
            return new HistoryReadAM()
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

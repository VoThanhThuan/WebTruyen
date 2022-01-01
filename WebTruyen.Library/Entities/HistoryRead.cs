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
                LastReadChapter = LastReadChapter,
                TimeCreate = TimeCreate
            };
        }

        public Guid LastReadChapter { get; set; }

        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }

        public DateTime TimeCreate { get; set; } = DateTime.Now;
        public virtual Comic Comic { get; set; }
        public virtual User User { get; set; }
    }
}

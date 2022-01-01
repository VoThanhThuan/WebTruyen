using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.ApiModel
{
    public class HistoryReadAM
    {
        public HistoryRead ToHistoryRead()
        {
            return new HistoryRead()
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

    }
}

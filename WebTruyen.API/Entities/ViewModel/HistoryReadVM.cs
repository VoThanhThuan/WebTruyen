using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities.ViewModel
{
    public class HistoryReadVM
    {
        public HistoryRead ToHistoryRead()
        {
            return new HistoryRead()
            {
                IdUser = IdUser,
                IdComic = IdComic
            };
        }

        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }
    }
}

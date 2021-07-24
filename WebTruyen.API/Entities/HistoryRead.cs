﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.API.Entities.ViewModel;

namespace WebTruyen.API.Entities
{
    public class HistoryRead
    {
        public HistoryReadVM ToViewModel()
        {
            return new HistoryReadVM()
            {
                IdUser = IdUser,
                IdComic = IdComic
            };
        }

        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }
        public virtual Comic Comic { get; set; }
        public virtual User User { get; set; }
    }
}

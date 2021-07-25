﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.Library.Entities
{
    public class NewComicAnnouncement
    {
        public NewComicAnnouncementVM ToViewModel()
        {
            return new NewComicAnnouncementVM()
            {
                Id = Id,
                IdUser = IdUser,
                IdChapter = IdChapter
            };
        }

        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdChapter { get; set; }
        public virtual User User { get; set; }
        public virtual Chapter Chapter { get; set; }
    }
}
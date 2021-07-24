﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.API.Entities.ViewModel;

namespace WebTruyen.API.Entities
{
    public class Page
    {
        public PageVM ToViewModel()
        {
            return new PageVM()
            {
                Id = Id,
                Image = Image,
                IsLink = IsLink,
                SortOrder = SortOrder,
                IdChapter = IdChapter
            };
        }

        [Key]
        public Guid Id { get; set; }
        public string Image { get; set; }
        public bool IsLink { get; set; } = false;
        public int SortOrder { get; set; }

        public Guid IdChapter { get; set; }
        public virtual Chapter Chapter { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities
{
    public class Report
    {
        public ReportAM ToApiModel()
        {
            return new ReportAM()
            {
                Content = Content,
                IdChapter = IdChapter,
                IdUser = IdUser
            };
        }
        public string Content { get; set; } = "";
        public Guid IdChapter { get; set; }
        public Guid IdUser { get; set; }
        public virtual Chapter Chapter { get; set; }
        public virtual User User { get; set; }
    }
}

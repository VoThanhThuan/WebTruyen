using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities.ViewModel
{
    public class ReportVM
    {
        public Report ToReport()
        {
            return new Report()
            {
                Content = Content,
                IdChapter = IdChapter,
                IdUser = IdUser
            };
        }

        public string Content { get; set; }
        public Guid IdChapter { get; set; }
        public Guid IdUser { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities
{
    public class Report
    {
        public string Content { get; set; }
        public Guid IdChapter { get; set; }
        public Guid IdUser { get; set; }
        public Chapter Chapter { get; set; }
        public User User { get; set; }
    }
}

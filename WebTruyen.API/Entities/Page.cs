using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities
{
    public class Page
    {
        [Key]
        public Guid Id { get; set; }
        public string Image { get; set; }
        public bool IsLink { get; set; } = false;
        public int SortOrder { get; set; }

        public Guid IdChapter { get; set; }
        public Chapter Chapter { get; set; }
    }
}

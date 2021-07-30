using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.ViewModel
{
    public class PageVM
    {
        public Page ToPage()
        {
            return new Page()
            {
                Id = Id,
                Image = Image,
                SortOrder = SortOrder,
                IdChapter = IdChapter
            };
        }
        [Key]
        public Guid Id { get; set; }
        public string Image { get; set; }
        public int SortOrder { get; set; }
        public Guid IdChapter { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities.ViewModel
{
    public class PageVM
    {
        public Page ToPage()
        {
            return new Page()
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
    }
}

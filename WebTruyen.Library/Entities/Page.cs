using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities
{
    public class Page
    {
        public PageAM ToApiModel()
        {
            return new PageAM()
            {
                Id = Id,
                Image = Image,
                SortOrder = SortOrder,
                IdChapter = IdChapter
            };
        }

        [Key]
        public Guid Id { get; set; }
        public string Image { get; set; } = "";
        public int SortOrder { get; set; } = 0;

        public Guid IdChapter { get; set; }
        public virtual Chapter Chapter { get; set; }
    }
}

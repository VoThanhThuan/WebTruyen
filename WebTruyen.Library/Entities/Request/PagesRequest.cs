using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebTruyen.Library.Entities.Request
{
    public class PageRequest
    {
        public Guid? Id { get; set; }
        public IFormFile Image { get; set; }
        public string Link { get; set; }
        public int? SortOrder { get; set; } = 0;
        public Guid IdChapter { get; set; }
    }
}

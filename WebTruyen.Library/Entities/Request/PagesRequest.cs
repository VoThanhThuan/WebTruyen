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
        public Guid Id { get; set; }
        public int Ordinal { get; set; } = 1;
        public List<IFormFile> Image { get; set; }
        public string Link { get; set; }
        public int SortOrder { get; set; }
        public Guid IdChapter { get; set; }
    }
}

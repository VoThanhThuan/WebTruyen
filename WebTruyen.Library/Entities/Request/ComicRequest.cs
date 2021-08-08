using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTruyen.Library.Enums;

namespace WebTruyen.Library.Entities.Request
{
    public class ComicRequest
    {
        public Comic ToComic()
        {
            return new Comic()
            {
                Id = Id,
                Name = Name,
                AnotherNameOfComic = AnotherNameOfComic,
                Author = Author,
                Status = Status,
                Description = Description
            };
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string AnotherNameOfComic { get; set; }
        public string Author { get; set; }
        public Status? Status { get; set; } = 0;

        public string Description { get; set; }
        public IFormFile Thumbnail { get; set; }
    }
}

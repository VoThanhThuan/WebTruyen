using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.API.Enums;

namespace WebTruyen.API.Entities
{
    public class Comic
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string AnotherNameOfComic { get; set; }
        public string Author { get; set; }
        public Status Status { get; set; }
        public int Views { get; set; } = 0;
        public string Description { get; set; }
        public string Thumbnail { get; set; }

        //Khoa ngoai
        public List<ComicInGenre> ComicInGenres { get; set; }
    }
}

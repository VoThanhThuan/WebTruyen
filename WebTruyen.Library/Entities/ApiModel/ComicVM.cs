using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebTruyen.Library.Enums;

namespace WebTruyen.Library.Entities.ApiModel
{
    public class ComicAM
    {
        public Comic ToComic()
        {
            return new Comic()
            {
                Id = Id,
                Name = Name,
                NameAlias = NameAlias,
                AnotherNameOfComic = AnotherNameOfComic,
                Author = Author,
                Status = Status,
                Views = Views,
                Description = Description,
                Thumbnail = Thumbnail,
                DateUpdate = DateUpdate
            };
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public string NameAlias { get; set; } = "";
        public string AnotherNameOfComic { get; set; } = "";
        public string Author { get; set; } = "";
        [Required]
        public Status? Status { get; set; } = 0;
        public int? Views { get; set; } = 0;
        public string Description { get; set; } = "";
        public string Thumbnail { get; set; } = "";
        public DateTime DateUpdate { get; set; } = DateTime.Now;


        public List<GenreAM> Genres { get; set; } = new List<GenreAM>();

    }
}

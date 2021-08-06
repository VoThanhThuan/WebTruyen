using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using WebTruyen.Library.Enums;

namespace WebTruyen.Library.Entities.ViewModel
{
    public class ComicVM
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
                Thumbnail = Thumbnail
            };
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public string NameAlias { get; set; } = "";
        public string AnotherNameOfComic { get; set; } = "";
        public string Author { get; set; } = "";
        public Status? Status { get; set; } = 0;
        public int? Views { get; set; } = 0;
        public string Description { get; set; } = "";
        public string Thumbnail { get; set; } = "";

    }
}

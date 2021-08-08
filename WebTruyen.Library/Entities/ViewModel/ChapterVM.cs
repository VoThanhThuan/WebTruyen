using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebTruyen.Library.Entities.ViewModel
{
    public class ChapterVM
    {
        public Chapter ToChapter()
        {
            return new Chapter()
            {
                Id = Id,
                Ordinal = Ordinal,
                Name = Name,
                DateTimeUp = DateTimeUp,
                Views = Views,
                IdComic = IdComic
            };
        }

        public Guid Id { get; set; }
        public float Ordinal { get; set; } = 1.0f;
        public string Name { get; set; } = "";
        public DateTime DateTimeUp { get; set; } = DateTime.Now;
        public int Views { get; set; } = 0;

        public Guid IdComic { get; set; }

    }
}

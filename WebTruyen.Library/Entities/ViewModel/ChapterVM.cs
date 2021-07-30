using System;
using System.ComponentModel.DataAnnotations;

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
                Views = Views
            };
        }

        [Key]
        public Guid Id { get; set; }
        public int Ordinal { get; set; } = 1;
        public string Name { get; set; }
        public DateTime DateTimeUp { get; set; } = DateTime.Now;
        public int Views { get; set; } = 0;


    }
}

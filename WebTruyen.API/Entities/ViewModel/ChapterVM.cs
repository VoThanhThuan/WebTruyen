using System;
using System.ComponentModel.DataAnnotations;

namespace WebTruyen.API.Entities.ViewModel
{
    public class ChapterVM
    {
        public Chapter ToChapter()
        {
            return new Chapter()
            {
                Id = Id,
                DateTimeUp = DateTimeUp,
                Views = Views
            };
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime DateTimeUp { get; set; } = DateTime.Now;
        public int Views { get; set; } = 0;


    }
}

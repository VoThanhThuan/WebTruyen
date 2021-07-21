using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities
{
    public class Chapter
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateTimeUp { get; set; } = DateTime.Now;
        public int Views { get; set; } = 0;

        public Guid IdComic { get; set; }
        public Comic Comic { get; set; }
    }
}

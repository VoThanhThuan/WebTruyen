using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities
{
    public class Genre
    {
        public GenreAM ToApiModel()
        {
            return new GenreAM()
            {
                Id = Id,
                Name = Name,
                Description = Description
            };
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public virtual ComicInGenre ComicInGenre { get; set; }
    }
}

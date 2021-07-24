using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities.ViewModel
{
    public class GenreVM
    {
        public Genre ToGenre()
        {
            return new Genre()
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
        public string Name { get; set; }
        public string Description { get; set; }

    }
}

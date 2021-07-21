using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.API.Entities
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateTimeUp { get; set; } = DateTime.Now;
        public string Chapter { get; set; }

        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }
        public Comic Comic { get; set; }
        public User User { get; set; }

        public Guid IdCommentReply { get; set; }
        public Comment CommentReply { get; set; }
    }
}

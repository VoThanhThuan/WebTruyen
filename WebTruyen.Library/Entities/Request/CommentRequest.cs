using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.Request
{
    public class CommentRequest
    {
        public Comment ToComment()
        {
            return new Comment()
            {
                Id = Id,
                DateTimeUp = DateTime.Now,
                IdCommentReply = IdCommentReply,
                Level = Level,
                Content = Content,
                IdComic = IdComic,
                IdUser = IdUser,
                IdChapter = IdChapter
            };
        }

        [Key]
        public Guid Id { get; set; }
        [Range(0,3)]
        public int Level { get; set; } = 0;

        [Required]
        public string Content { get; set; } = "";

        public Guid? IdChapter { get; set; }
        public Guid? IdComic { get; set; }
        public Guid? IdCommentReply { get; set; }

        public Guid IdUser { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities
{
    public class Comment
    {
        public CommentAM ToApiModel()
        {
            return new CommentAM()
            {
                Id = Id,
                DateTimeUp = DateTimeUp,
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
        public DateTime DateTimeUp { get; set; } = DateTime.Now;

        [Range(0, 3)]
        public int Level { get; set; } = 0;
        public Guid? IdCommentReply { get; set; }

        public string Content { get; set; } = "";

        public Guid? IdComic { get; set; }
        public Guid? IdChapter { get; set; }
        public Guid IdUser { get; set; }

        public virtual List<Comment> CommentReply { get; set; } = null;

        public virtual Comic Comic { get; set; }
        public virtual Chapter Chapter { get; set; }
        public virtual User User { get; set; }
    }
}

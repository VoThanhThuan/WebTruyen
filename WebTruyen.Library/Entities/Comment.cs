using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.Library.Entities
{
    public class Comment
    {
        public CommentVM ToViewModel()
        {
            return new CommentVM()
            {
                Id = Id,
                DateTimeUp = DateTimeUp,
                Chapter = Chapter,
                IdComic = IdComic,
                IdUser = IdUser,
                IdCommentReply = IdCommentReply
            };
        }

        [Key]
        public Guid Id { get; set; }
        public DateTime DateTimeUp { get; set; } = DateTime.Now;
        public string Chapter { get; set; } = "";

        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }
        public Comic Comic { get; set; }
        public virtual User User { get; set; }

        public virtual Guid IdCommentReply { get; set; }
        public virtual Comment CommentReply { get; set; }
    }
}

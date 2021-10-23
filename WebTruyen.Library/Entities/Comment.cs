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
                IdCommentReply = IdCommentReply,
                Level = Level,

                IdComic = IdComic,
                IdUser = IdUser,
                IdChapter = IdChapter
            };
        }

        [Key]
        public Guid Id { get; set; }
        public DateTime DateTimeUp { get; set; } = DateTime.Now;
        public int Level { get; set; } = 0;
        public Guid? IdCommentReply { get; set; }

        public Guid? IdComic { get; set; }
        public Guid? IdChapter { get; set; }
        public Guid IdUser { get; set; }

        public virtual Comment CommentReply { get; set; }

        public virtual Comic Comic { get; set; }
        public virtual Chapter Chapter { get; set; }
        public virtual User User { get; set; }
    }
}

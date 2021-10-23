using System;
using System.ComponentModel.DataAnnotations;

namespace WebTruyen.Library.Entities.ViewModel
{
    public class CommentVM
    {
        public Comment ToComment()
        {
            return new Comment()
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


        public Guid? IdChapter { get; set; }
        public Guid? IdComic { get; set; }
        public Guid IdUser { get; set; }
        public Comment CommentReply { get; set; }

    }
}

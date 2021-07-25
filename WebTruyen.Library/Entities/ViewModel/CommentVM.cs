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
                Chapter = Chapter,
                IdComic = IdComic,
                IdUser = IdUser,
                IdCommentReply = IdCommentReply
            };
        }

        [Key]
        public Guid Id { get; set; }
        public DateTime DateTimeUp { get; set; } = DateTime.Now;
        public string Chapter { get; set; }

        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdCommentReply { get; set; }

    }
}

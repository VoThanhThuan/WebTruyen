using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebTruyen.Library.Entities.ApiModel
{
    public class CommentAM
    {
        public Comment ToComment()
        {
            return new Comment()
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

        [Required]
        [NotNull]
        public string Content { get; set; }

        public Guid? IdChapter { get; set; }
        public Guid? IdComic { get; set; }
        public Guid IdUser { get; set; }

        public int TotalCommentChild { get; set; } = 0;
    }
}

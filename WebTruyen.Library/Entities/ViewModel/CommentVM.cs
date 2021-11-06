using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities.ViewModel
{
    public class CommentVM
    {

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

        public UserAM User { get; set; } = new();//chỉ dùng trên client

        public List<CommentVM> CommentReply { get; set; } = new();

        public int TotalCommentChild { get; set; } = 0;
    }
}

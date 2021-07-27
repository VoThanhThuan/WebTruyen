using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Comment
{
    public interface ICommentService
    {
        public Task<IEnumerable<CommentVM>> GetComments();
        public Task<CommentVM> GetComment(Guid id);
        public Task<bool> PutComment(Guid id, CommentVM request);
        public Task<bool> PostComment(CommentVM request);
        public Task<bool> DeleteComment(Guid id);

    }
}

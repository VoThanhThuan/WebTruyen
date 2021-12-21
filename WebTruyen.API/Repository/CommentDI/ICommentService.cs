using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.CommentDI
{
    public interface ICommentService
    {
        public Task<IEnumerable<CommentAM>> GetComments();
        public Task<CommentAM> GetComment(Guid id);
        public Task<List<CommentAM>> GetCommentInComic(Guid idComic, int skip = 0, int take = 10);
        public Task<List<CommentAM>> GetCommentChildInComic(Guid idComic, Guid idCommentReply, int skip = 0, int take = 10);
        public Task<List<CommentAM>> GetCommentInChapter(Guid idChapter, int skip = 0, int take = 10);
        public Task<List<CommentAM>> GetCommentChildInChapter(Guid idChapter, Guid idCommentReply, int skip = 0, int take = 10);
        public Task<(bool isSuccess, string messages)> PutComment(Guid id, CommentAM request);
        public Task<(bool isSuccess, string messages)> PostComment(CommentRequest request);
        public Task<bool> DeleteComment(Guid id);

    }
}

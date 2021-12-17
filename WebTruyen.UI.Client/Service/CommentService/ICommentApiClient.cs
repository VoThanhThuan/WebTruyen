using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.CommentService
{
    public interface ICommentApiClient
    {
        public Task<IEnumerable<CommentVM>> GetComments();
        public Task<CommentAM> GetComment(Guid id);
        public Task<List<CommentVM>> GetCommentInComic(Guid idComic, int take = 10, int skip = 0);
        public Task<List<CommentVM>> GetCommentInChapter(Guid idChapter, int take = 10, int skip = 0);
        public Task<List<CommentVM>> GetCommentChildInComic(Guid idComic, Guid idCommentReply, int skip = 0, int take = 10);
        public Task<List<CommentVM>> GetCommentChildInChapter(Guid idChapter, Guid idCommentReply, int skip = 0, int take = 10);

        public Task<bool> PutComment(Guid id, CommentRequest request);
        public Task<(bool isSuccess, string value)> PostComment(CommentRequest request);
        public Task<bool> DeleteComment(Guid id);

    }
}

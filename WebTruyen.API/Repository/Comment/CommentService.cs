using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.Comment
{
    public class CommentService : ICommentService
    {
        private readonly ComicDbContext _context;

        public CommentService(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommentAM>> GetComments()
        {
            return await _context.Comments.OrderByDescending(x => x.DateTimeUp).Select(x => x.ToApiModel()).ToListAsync();
        }

        public async Task<CommentAM> GetComment(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);

            return comment?.ToApiModel();
        }

        public async Task<List<CommentAM>> GetCommentInComic(Guid idComic, int skip = 0, int take = 10)
        {
            var comments = await _context.Comments
                .Where(x => x.Level == 0 && x.IdComic == idComic)
                .OrderByDescending(x => x.DateTimeUp)
                .Skip(skip).Take(take)
                .Select(x => x.ToApiModel())
                .ToListAsync();

            return comments;
        }

        public async Task<List<CommentAM>> GetCommentChildInComic(Guid idComic, Guid idCommentReply, int skip = 0, int take = 10)
        {
            var comments =
                await _context.Comments.FirstOrDefaultAsync(x => x.Id == idCommentReply && x.IdComic == idComic);

            var cmtChilds = await _context.Comments
                .Where(x => x.IdCommentReply == comments.IdComic).OrderBy(x => x.DateTimeUp)
                .Select(x => x.ToApiModel()).ToListAsync();

            return cmtChilds;
        }

        public async Task<List<CommentAM>> GetCommentInChapter(Guid idChapter, int skip = 0, int take = 10)
        {
            var comments = await _context.Comments
                .Where(x => x.Level == 0 && x.IdChapter == idChapter)
                .OrderByDescending(x => x.DateTimeUp)
                .Skip(skip).Take(take)
                .Select(x => x.ToApiModel())
                .ToListAsync();
            return comments;
        }

        public async Task<List<CommentAM>> GetCommentChildInChapter(Guid idChapter, Guid idCommentReply, int skip = 0, int take = 10)
        {
            var comments =
                await _context.Comments.FirstOrDefaultAsync(x => x.Id == idCommentReply && x.IdChapter == idChapter);

            var cmtChilds = await _context.Comments
                .Where(x => x.IdCommentReply == comments.IdComic).OrderBy(x => x.DateTimeUp)
                .Skip(skip).Take(take)
                .Select(x => x.ToApiModel()).ToListAsync();

            return cmtChilds;
        }

        public async Task<(bool isSuccess, string messages)> PutComment(Guid id, CommentAM request)
        {
            _context.Entry(request.ToComment()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return (false, "Lỗi hệ thống khi sửa dữ liệu");
                }
                else
                {
                    throw;
                }
            }

            return (true, "Ok");
        }

        public async Task<(bool isSuccess, string messages)> PostComment(CommentRequest request)
        {
            if (request.IdCommentReply is not null)
            {
                var cmtReply = await _context.Comments.FirstOrDefaultAsync(x => x.Id == request.IdCommentReply);
                if (cmtReply is null)
                    return (false, "Comment trả lời không tồn tại");
            }

            if (request.IdComic != null && request.IdComic != Guid.Empty)
            {
                var comic = await _context.Comics.FirstOrDefaultAsync(x => x.Id == request.IdComic);
                if (comic is null)
                    return (false, "Comic không tồn tại");
            }

            if (request.IdChapter != null && request.IdChapter != Guid.Empty)
            {
                var chapter = await _context.Chapters.FirstOrDefaultAsync(x => x.Id == request.IdChapter);
                if (chapter is null)
                    return (false, "Chapter không tồn tại");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.IdUser);
            if (user is null)
                return (false, "Không tìm thấy user");

            _context.Comments.Add(request.ToComment());
            await _context.SaveChangesAsync();

            return (true, "Ok");
        }

        public async Task<bool> DeleteComment(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return false;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool CommentExists(Guid id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Comment
{
    public class CommentService : ICommentService
    {
        private readonly ComicDbContext _context;

        public CommentService(ComicDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CommentVM>> GetComments()
        {
            return await _context.Comments.OrderByDescending(x=> x.DateTimeUp).Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<CommentVM> GetComment(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);

            return comment?.ToViewModel();
        }

        public async Task<bool> PutComment(Guid id, CommentVM request)
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
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> PostComment(CommentVM comment)
        {
            _context.Comments.Add(comment.ToComment());
            await _context.SaveChangesAsync();

            return true;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Bookmark
{
    public interface IBookmarkService
    {
        public Task<IEnumerable<BookmarkVM>> GetBookmarks();
        public Task<BookmarkVM> GetBookmark(Guid id);
        public Task<bool> PutBookmark(Guid id, BookmarkVM bookmark);
        public Task<bool> PostBookmark(BookmarkVM bookmark);
        public Task<bool> DeleteBookmark(Guid id);

    }
}

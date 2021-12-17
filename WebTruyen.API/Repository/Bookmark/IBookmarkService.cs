using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.Bookmark
{
    public interface IBookmarkService
    {
        public Task<IEnumerable<BookmarkAM>> GetBookmarks();
        public Task<BookmarkAM> GetBookmark(Guid id);
        public Task<BookmarkAM> GetBookmarkOfAccount(Guid idComic, Guid idUser);
        public Task<bool> PutBookmark(Guid id, BookmarkAM bookmark);
        public Task<bool> PostBookmark(BookmarkAM bookmark);
        public Task<bool> DeleteBookmark(Guid idUser, Guid idComic);

    }
}

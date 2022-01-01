using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.BookmarkService
{
    public interface IBookmarkApiClient
    {
        public Task<BookmarkAM> GetBookmarkOfAccount(Guid idComic, Guid idUser);

        public Task<int> AddBookmark(BookmarkAM bookmark);
        public Task<int> RemoveBookmark(Guid idComic, Guid idUser);
    }
}

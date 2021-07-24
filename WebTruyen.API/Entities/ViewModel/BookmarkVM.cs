using System;

namespace WebTruyen.API.Entities.ViewModel
{
    public class BookmarkVM
    {
        public Bookmark ToBookmark()
        {
            return new Bookmark()
            {
                IdComic = IdComic,
                IdUser = IdUser
            };
        }
        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }

    }
}

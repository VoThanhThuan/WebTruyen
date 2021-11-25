using System;

namespace WebTruyen.Library.Entities.ApiModel
{
    public class BookmarkAM
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

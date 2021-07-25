using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.ViewModel
{
    public class TranslationOfUserVM
    {
        public TranslationOfUser ToTranslationOfUser()
        {
            return new TranslationOfUser()
            {
                IdUser = IdUser,
                IdComic = IdComic
            };
        }

        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }
    }
}

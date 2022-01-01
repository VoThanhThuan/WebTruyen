using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTruyen.Library.Entities.ApiModel
{
    public class TranslationOfUserAM
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

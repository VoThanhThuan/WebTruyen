using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.Library.Entities
{
    public class TranslationOfUser
    {
        public TranslationOfUserAM ToApiModel()
        {
            return new TranslationOfUserAM()
            {
                IdUser = IdUser,
                IdComic = IdComic
            };
        }

        public Guid IdComic { get; set; }
        public Guid IdUser { get; set; }
        public virtual Comic Comic { get; set; }
        public virtual User User { get; set; }
    }
}

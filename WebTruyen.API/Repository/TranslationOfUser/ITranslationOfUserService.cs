using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.TranslationOfUser
{
    public interface ITranslationOfUserService
    {
        public Task<IEnumerable<TranslationOfUserAM>> GetTranslationOfUsers();
        public Task<TranslationOfUserAM> GetTranslationOfUser(Guid id);
        public Task<bool> PutTranslationOfUser(Guid id, TranslationOfUserAM request);
        public Task<bool> PostTranslationOfUser(TranslationOfUserAM request);
        public Task<bool> DeleteTranslationOfUser(Guid id);

    }
}

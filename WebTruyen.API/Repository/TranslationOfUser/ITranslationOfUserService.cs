using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.TranslationOfUser
{
    public interface ITranslationOfUserService
    {
        public Task<IEnumerable<TranslationOfUserVM>> GetTranslationOfUsers();
        public Task<TranslationOfUserVM> GetTranslationOfUser(Guid id);
        public Task<bool> PutTranslationOfUser(Guid id, TranslationOfUserVM request);
        public Task<bool> PostTranslationOfUser(TranslationOfUserVM request);
        public Task<bool> DeleteTranslationOfUser(Guid id);

    }
}

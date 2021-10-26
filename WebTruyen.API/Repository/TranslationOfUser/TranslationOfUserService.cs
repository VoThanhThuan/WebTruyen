using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.TranslationOfUser
{
    public class TranslationOfUserService : ITranslationOfUserService
    {
        private readonly ComicDbContext _context;

        public TranslationOfUserService(ComicDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TranslationOfUserAM>> GetTranslationOfUsers()
        {
            return await _context.TranslationOfUsers.Select(x => x.ToApiModel()).ToListAsync();
        }

        public async Task<TranslationOfUserAM> GetTranslationOfUser(Guid id)
        {
            var translationOfUser = await _context.TranslationOfUsers.FindAsync(id);

            return translationOfUser?.ToApiModel();
        }

        public async Task<bool> PutTranslationOfUser(Guid id, TranslationOfUserAM request)
        {
            _context.Entry(request.ToTranslationOfUser()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslationOfUserExists(id))
                    return false;
                throw;
            }

            return true;
        }

        public async Task<bool> PostTranslationOfUser(TranslationOfUserAM request)
        {
            _context.TranslationOfUsers.Add(request.ToTranslationOfUser());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TranslationOfUserExists(request.IdUser))
                    return false;
                throw;
            }

            return true;

        }

        public async Task<bool> DeleteTranslationOfUser(Guid id)
        {
            var translationOfUser = await _context.TranslationOfUsers.FindAsync(id);
            if (translationOfUser == null)
            {
                return false;
            }

            _context.TranslationOfUsers.Remove(translationOfUser);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool TranslationOfUserExists(Guid id)
        {
            return _context.TranslationOfUsers.Any(e => e.IdUser == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.User
{
    public class UserService : IUserService
    {
        private readonly ComicDbContext _context;

        public UserService(ComicDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserVM>> GetUsers()
        {
            return await _context.Users.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<UserVM> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            return user?.ToViewModel();
        }

        public async Task<bool> PutUser(Guid id, UserVM request)
        {
            _context.Entry(request.ToUser()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return false;
                throw;
            }

            return true;
        }

        public async Task<bool> PostUser(UserVM request)
        {
            _context.Users.Add(request.ToUser());
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
